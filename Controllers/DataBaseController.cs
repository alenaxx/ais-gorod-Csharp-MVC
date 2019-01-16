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
               //     dbase.DataBaseList = DC.DataBase.Select(s => new DataBaseList { ipAdress = s.iPadress, nameBD = s.dataBaseName }).ToList();
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
                        var dataBaseCount = DC.DataBase.Where(s => s.iPadress == model.ipAdress && s.dataBaseName == model.nameBD).FirstOrDefault();
                        if (dataBaseCount == null)
                        {
                            DC.GetDataBase(model.ipAdress, model.nameBD);
                        }
                        model.DataBaseList = DC.DataBase.Select(s => new DataBaseList { ipAdress = s.iPadress, nameBD = s.dataBaseName }).ToList();
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