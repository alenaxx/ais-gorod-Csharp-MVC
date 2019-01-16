using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiProcess.Models;
namespace MultiProcess.Controllers
{
    public class WorkController : Controller
    {
        public ActionResult Work()
        {
            WorkModel wbase = new WorkModel();

            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                {
                    
                }
            }
            return View(wbase);
        }
        [HttpPost]
        public ActionResult Work (WorkModel model)
        {
            try
            {
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    var organizationCount = DC.WorkType.Where(s => s.name == model.name && s.sqlProcedure == model.sqlProcedure).FirstOrDefault();
                    if (organizationCount == null)
                    {
                        DC.GetWorkProc(model.name, model.sqlProcedure);
                    }
                    model.WorkList = DC.WorksView.Select(s => new WorkList
                    {
                        orgName = s.orgName,
                        name = s.name,
                        dataBaseName = s.dataBaseName,
                        sqlProcedure = s.sqlProcedure
                    }).ToList();
                    model.name = "";
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
        public ActionResult WorkOrg(WorkModel model)
        {
            try
            {
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    var organizationCount = DC.Works.Where(s => s.workTypeId == model.workTypeId && s.orgId == model.orgId).FirstOrDefault();
                    if (organizationCount == null)
                    {
                        DC.GetWorkOrg(model.workTypeId,model.orgId);
                    }
                    model.WorkList = DC.WorksView.Select(s => new WorkList
                    {
                        orgName = s.orgName,
                        name = s.name,
                        dataBaseName = s.dataBaseName,
                        sqlProcedure = s.sqlProcedure
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
            return View("Work", model);
        }




        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getOrg()
        {
            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                var s2 = DC.Organizations.Select(p => new { id = p.id, name = p.orgName }).ToList();
                if (s2.Count > 0)
                    return Json(s2, JsonRequestBehavior.AllowGet);
                else
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getWork()
        {
            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                var s1 = DC.WorkType.Select(p => new { id = p.id, name = p.name }).ToList();
                if (s1.Count > 0)
                    return Json(s1, JsonRequestBehavior.AllowGet);
                else
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}