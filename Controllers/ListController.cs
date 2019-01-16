using MultiProcess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiProcess.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Index()
        {

            ListModel dbase = new ListModel();

            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                {
                    dbase.RollOrg = DC.Organizations.Select(s => new OrgListModel { orgName = s.orgName }).ToList();
                }
            }
            return View(dbase);



        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetInfo(string orgName)
        {
            ListModel model = new ListModel();
            List<DataBaseList> listBase = new List<DataBaseList>();
            try
            {
                long orgid = 0;
                long baseId = 0;
                List<ProcedureModel> procedure = new List<ProcedureModel>();
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    orgid = DC.Organizations.Where(s => s.orgName == orgName).Select(s => s.id).FirstOrDefault();//получаем orgId
                    listBase = DC.OrganizationBases.Where(s => s.orgId == orgid).Join(DC.DataBase, e => e.orgId, o => o.id, (e, o) => new DataBaseList { ipAdress = o.iPadress, nameBD = o.dataBaseName, idOrgBase = e.id }).ToList();
                    var WorkId = DC.GetWork(orgid);//по orgid ищем workid
                    foreach (var item in WorkId)
                    {
                        var workid = item.id;
                        var period = DateTime.Now;
                        DC.FullProcessWorks(workid, period, period);
                        int? idd = DC.GetWorkId(workid, period, period);//заполняем ProcessWork  должен вернутся id ProcessWorks
                                                                        //ты этот id заполняешь в ProcesWorkBases в поле ProcessWorkId 
                                                                        // здесь ты еще ищешь все базы и заполняешь их в поле PrganizationBaseId , записей в ProcessWorkBases должно быть равно количеству баз организации
                                                                        //получается если у Организации= 4 базы. и 2 работы (WorkId) в в ProcessWorkBases кладется 2 раза по 4 записи = 8
                                                                        // }
                                                                        // baseId = DC.OrganizationBases.Where(s => s.orgId == orgid).Select(s => s.baseId).FirstOrDefault();//по orgid ищем базу  
                                                                        //    foreach (var it in WorkId)
                                                                        //  {
                        var countOrgBases = DC.OrganizationBases.Where(s => s.orgId == orgid).Select(s => s.id).ToList();
                        foreach (var count in countOrgBases)
                        {
                            DC.FullProcessWorkBases(idd, count, period);//заполняем ProcessWorkBases
                        }
                        string pr;
                        pr = DC.SqlProcedureView.Where(s => s.id == item.id).Select(s => s.sqlProcedure).FirstOrDefault();//ищем процедуру по workid
                        procedure.Add(new ProcedureModel { sqlProcedure = pr, Workid = item.id });

                        foreach (var itemBase in listBase)
                        {
                            string connectionString = String.Format("Data Source={0};Initial Catalog={1};User ID={2};Password={3}",
                                                                                                       itemBase.ipAdress,
                                                                                                       itemBase.nameBD,
                                                                                                      "dev", "f,shdfku");

                            string sqlExpression = "[dbo].[GetProcedure]";
                            using (SqlConnection conn = new SqlConnection(connectionString))
                            {
                                conn.Open();
                                SqlCommand cmd = new SqlCommand(sqlExpression, conn);
                                string[] result = { null, null };
                                var Results = new DataSet();
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.CommandType = CommandType.Text;
                                cmd.CommandText = @"exec [dbo].[GetProcedure] @Period";
                                cmd.Parameters.Add("@Period", SqlDbType.DateTime);
                                cmd.Parameters["@Period"].Value = DateTime.Now;
                                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                                adapter.Fill(Results);
                                int i = 0;
                                var getWorkList = new GetWorkModel();
                                getWorkList.getTranscript = new List<GetTranscriptModel>();
                                foreach (DataTable table in Results.Tables)
                                {

                                    foreach (DataRow row in table.Rows)
                                    {

                                        if (Results.Tables[0].Rows.Count >= 0 && Results.Tables[0].Columns.Count >= 1)
                                        {
                                            if (getWorkList.fullValue == null)
                                            getWorkList.fullValue = Results.Tables[0].Rows[0][0].ToString();

                                            if (Results.Tables[1].Rows.Count > 0 && Results.Tables[1].Columns.Count > 1 && Results.Tables[1].Rows.Count > i)
                                            {
                                                try
                                                {
                                                    result[1] = Results.Tables[1].Rows[i][1].ToString();
                                                    getWorkList.getTranscript.Add(new GetTranscriptModel { transcriptName = Results.Tables[1].Rows[i][0].ToString(), transcriptValue = Int64.Parse(result[1]) });
                                                }
                                                catch (Exception) { }
                                                i++;

                                            }
                                        }

                                        //msg += string.Format("{0} - {1}\n", table.TableName, row[0].ToString());

                                    }
                                }

                               // long idOrgBasesPWB = DC.ProcessWorkBases.Where(s => s.organizationBaseId == && s.created == period).Select(s => s.id).FirstOrDefault();                            
                                var idPWB = DC.SaveValueToProcessWorkBases(idd, Int64.Parse(getWorkList.fullValue), itemBase.idOrgBase);
                                if(getWorkList.getTranscript.Count > 0)
                                {
                                    foreach(var item2 in getWorkList.getTranscript)
                                    {
                                        var s =  DC.SaveValueToProcessWorkBaseDetails(idPWB, item2.transcriptName, item2.transcriptValue, period);
                                    }
                                }


                            }

                        }
                    }
                    //var full = DC.FullView.Select();


                }
            }
            catch (Exception ex)
            {
                model.message = "ERROR";
                model.result = -1;
                return View();
            }


            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}

