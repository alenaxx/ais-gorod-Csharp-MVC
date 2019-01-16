using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiProcess.Models;
using System.Reflection;

namespace MultiProcess.Controllers
{
    public class OrganizationController : Controller
    {

        public ActionResult Org1()
        {
            OrganizationModel torg = new OrganizationModel();

            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                torg.OrganizationList = DC.OrganizationsView.Select(s => new OrganizationList
                {
                    orgName = s.orgName,
                    
                }).ToList();
            }
            return View(torg);
        }
        public ActionResult Org()
        {
            OrganizationModel torg = new OrganizationModel();

            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
               
            }
            return View(torg);
        }

        [HttpPost]
        public ActionResult Org(OrganizationModel model)
        {
            try
            {
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    var organizationCount = DC.Organizations.FirstOrDefault();
                    if (organizationCount == null)
                    {
                        DC.GetOrganization(model.orgName);
                    }
                    model.OrganizationList = DC.OrganizationsView.Select(s => new OrganizationList
                    {
                        
                    }).ToList();
                    model.orgName = "";
                    model.message = "OK";
                    model.result = 0;
                }
            }
            catch (Exception ex)
            {
                model.message = "ERROR";
                model.result = -1;
                return View();
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult OrgBases(OrganizationModel model)
        {
            try
            {
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    var organizationCount = DC.OrganizationBases.FirstOrDefault();
                    if (organizationCount == null)
                    {
                        DC.GetOrganizationBases(model.orgId, model.baseId);
                    }
                    model.OrganizationList = DC.OrganizationsView.Select(s => new OrganizationList
                    {
                    }).ToList();
                    model.message = "OK";
                    model.result = 0;
                }
            }
            catch (Exception ex)
            {
                model.message = "ERROR";
                model.result = -1;
                return View();
            }
            return View("Org", model);
        }





        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetInfo(string orgId)
        {
            OrganizationModel model = new OrganizationModel();
            try
            {

                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    //var organizationCount = DC.WorkOrgView.Where(s => s.orgId == orgId).FirstOrDefault();
                    //if (organizationCount != null)
                    //{
                    //    model.OrganizationList = DC.WorkOrgView.Where(s => s.orgId == orgId).Select(s => new OrganizationList
                    //    {
                    //        orgName = s.orgName,
                    //        baseName = s.dataBaseName + "(" + s.iPadress + ")",
                    //        workId=s.workId

                    //    }
                    //).ToList();
                    //    model.orgName = "";
                    //    model.message = "OK";
                    //    model.result = 0;
                    //}
                    
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







        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getBase()
        {
            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                var s1 = DC.DataBase.Select(p => new { id = p.id, name = p.dataBaseName + "(" + p.iPadress + ")" }).ToList();
                if (s1.Count > 0)
                    return Json(s1, JsonRequestBehavior.AllowGet);
                else
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }


    

        
    }
}