using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class IncompleteEnrollment
    {
        [DataMember]
        public int RowId { get; set; }
        
        [DataMember]
        public string Enrollment { get; set; }
        [DataMember]
        public string Program { get; set; }
       
        [DataMember]
        public string PartnerName { get; set; }
        [DataMember]
        public string Month { get; set; }
        [DataMember]
        public string OperationCenter { get; set; }

        public IncompleteEnrollment()
        {
        }

        public IncompleteEnrollment(DataRow dr)
        {
            RowId = Convert.ToInt32(dr["RowId"]);
            
            Enrollment = dr["Enrollment"].ToString();
            Program = dr["Program"].ToString();
            
            PartnerName = dr["PartnerName"].ToString();
            Month = dr["Month"].ToString();
            OperationCenter = dr["OperationCenter"].ToString();
        }
    }
}
