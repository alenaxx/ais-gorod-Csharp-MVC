using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiProcess.Models;
namespace MultiProcess.Controllers
{
    public class DataBaseController : Controller
    {
        public ActionResult Base()
        {
           DataBaseModel dbase = new DataBaseModel();

            using (DataClassesDataContext DC = new DataClassesDataContext())
            {
                {
                  
                }
            }
            return View(dbase);
        }

        [HttpPost]
        public ActionResult Base(DataBaseModel model)
        {
            try
            {
                using (DataClassesDataContext DC = new DataClassesDataContext())
                {
                    {
                        var dataBaseCount = DC.DataBase.FirstOrDefault();
                        if (dataBaseCount == null)
                        {
                         
                        }
                        model.DataBaseList = DC.DataBase.Select(s => new DataBaseList {  }).ToList();
                        model.nameBD = "";
                        model.ipAdress = "";
                        model.message = "OK";
                        model.result = 0;
                    }
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
        }
}