using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiProcess.Models
{
    public class WorkModel
    {
        public List<WorkList> WorkList;
        public long orgId { get; set; }
        public long workTypeId { get; set; }
        public string name { get; set; }
        public string sqlProcedure { get; set; }
        public int result { get; set; }
        public string message { get; set; }
    }
    public class WorkList
    {
        public string orgName { get; set; }
        public string name { get; set; }
        public string dataBaseName { get; set; }
        public string sqlProcedure { get; set; }
    }
}



