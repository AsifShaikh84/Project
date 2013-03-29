using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable] 
    public class Report
    {
        [DataMember]
        public string ReportId{get;set;}

        [DataMember]
        public string ReportCode{get;set;}

        [DataMember]
        public string ReportName{get;set;}

        [DataMember]
        public string ReportDescription{get;set;}

        [DataMember]
        public DateTime  ModifiedOn{get;set;}

        [DataMember]
        public string ModifiedBy { get; set; }

    }

    [DataContract,Serializable]
    public class ReportRule
    {
        [DataMember]
        public int ReportId{get;set;}

        [DataMember]
        public int AlgorithmRuleId{get;set;}

        [DataMember]
        public string ReportedAs{get;set;}

        [DataMember]
        public string OutputFormat{get;set;}

        [DataMember]
        public int RowId{get;set;}
    }

    
}
