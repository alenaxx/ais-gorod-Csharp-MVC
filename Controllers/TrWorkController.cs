using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiProcess.Models;

namespace MultiProcess.Controllers
{
    public class TrWorkController : Controller
    {
        public ActionResult TrWork()
        {
            TrWorkModel tworg = new TrWorkModel();

            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                {
                    tworg.TrWorkList = DC.TrWorkView.Select(s => new TrWorkList
                    { WorkName = s.name, trName = s.trName }).OrderBy(s =>s.WorkName).ToList();
                  
                }

            }
            return View(tworg);
        }
        [HttpPost]
        public ActionResult TrWork(TrWorkModel model)
        {
            try
            {
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    var TranscriptCoount = DC.Transcript.Where(s => s.trName == model.trName && s.workId== model.workId).FirstOrDefault();
                    if (TranscriptCoount == null)
                    {   

                        DC.GetTranscript(model.trName,model.workId);

                    }
                    model.TrWorkList = DC.TrWorkView . Select(s => new TrWorkList
                    {

                        WorkName = s.name,
                        trName = s.trName



                    }).Distinct().ToList();
                    model.WorkName = "";
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


        //[HttpPost]
        //public ActionResult TrWork(TrWorkModel model)
        //{
        //    try
        //    {
        //        using (DataClassesDataContext DC = new DataClassesDataContext())
        //        {
        //            var workCount = DC.WorkType.Where(s => s.name == model.WorkName ).FirstOrDefault();
        //            if (workCount == null)
        //            {
        //                DC.GetWorkTr(model.WorkName);
        //            }
        //            model.TrWorkList = DC.TrWorkView.Select(s => new TrWorkList
        //            { 
        //               WorkName = s.name,
        //                trName = s.trName
        //            }).ToList();
        //            model.WorkName = "";
        //            model.message = "OK";
        //            model.result = 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        model.message = "ERROR";
        //        model.result = -1;
        //        return View();
        //    }
        //    return View(model);
        //}




        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getWork()
        {
            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                var s2 = DC.WorkType.Select(p => new { id = p.id, name = p.name }).ToList();
                if (s2.Count > 0)
                    return Json(s2, JsonRequestBehavior.AllowGet);
                else
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult getTrWork()
        {
            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                // var s1 = DC.WorkType.Select(p => new { id = p.id,name=p.name}).ToList();
                var s1 = DC.Works.Join(DC.WorkType,
                    e => e.workTypeId, o => o.id, (e, o) => new { id = e.id, name = o.name }).ToList();

                if (s1.Count > 0)
                    return Json(s1, JsonRequestBehavior.AllowGet);
                else
                    return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}