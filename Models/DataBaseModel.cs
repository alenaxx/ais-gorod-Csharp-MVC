using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiProcess.Models
{
    public class DataBaseModel
    {
        public List<DataBaseList> DataBaseList;

        public string ipAdress { get; set; }

        public string nameBD { get; set; }

        public int result { get; set; }

        public string message { get; set; }

    }
    public class DataBaseList
    {

        public long idOrgBase { get; set; }
        public string ipAdress { get; set; }

        public string nameBD { get; set; }
    }

}