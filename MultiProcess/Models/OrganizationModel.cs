using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace MultiProcess.Models
{
    public class OrganizationModel
    {
        public List<OrganizationList> OrganizationList;
        public string orgName { get; set; }
        public int baseId { get; set; }
        public int orgId { get; set; }
        public int result { get; set; }
        public string message { get; set; }
        public string actionButton { get; set; }
        public string workId { get; set; }
    }
    public class OrganizationList
    {
        public string orgName { get; set; }
        public string baseName { get; set; }
        public string workId { get; set; }
    }
}