using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MultiProcess.Models
{
    public class TrWorkModel
    {
        public List<TrWorkList> TrWorkList;
        public string trName { get; set; }
        public long workId { get; set; }
        public long workTypeId { get; set; }
        public string WorkName { get; set; }
        public int result { get; set; }
        public string message { get; set; }
    }

    public class TrWorkList
    {
        public string trName { get; set; }
        public string WorkName { get; set; }
        public long workId { get; set; }
    }
}


