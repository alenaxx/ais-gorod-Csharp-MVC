using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiProcess.Models
{
    public class ListModel
    {   
        public string orgName { get; set; }
        public List<RollModel> RollOrgname;
        public List<OrgListModel> RollOrg;
        public List<ProcedureModel> ProcedureModel;
       
        public int result { get; set; }
        public string message { get; set; }
        public long id { get; set; }    
    }
    
    public class ProcedureModel
    {
        public string sqlProcedure { get; set; }
        public long Workid { get; set; }
    }
    public class RollModel
    { 
        public string sqlProcedure { get; set; }
        public string dataBaseName { get; set; }
        public string ipAdress { get; set; }
        public string trName { get; set; }
        public string WorkName { get; set; }        
    }

    public class OrgListModel
    {
        public string orgName { get; set; }
    }

    public class GetWorkModel
    {
        public string fullValue { get; set; }

        public List<GetTranscriptModel> getTranscript;
    }

    public class GetTranscriptModel
    {
        public string transcriptName { get; set; }
        public long transcriptValue { get; set; }

    }
}