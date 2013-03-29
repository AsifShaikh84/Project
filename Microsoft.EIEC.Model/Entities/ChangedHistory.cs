using System.Runtime.Serialization;
using System.Data;
using System;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class ChangedHistory
    {
        [DataMember]
        public string ColumnName { get; set; }
        [DataMember]
        public string KeyField { get; set; }
        [DataMember]
        public string LastModified { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string RequestedBy { get; set; }
        [DataMember]
        public string RequestedOn { get; set; }
        [DataMember]
        public string ApprovalStatus { get; set; }
        [DataMember]
        public string LastModifiedBy { get; set; }
        [DataMember]
        public string MIMOS { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string RequestId { get; set; }
        [DataMember]
        public string RowId { get; set; }
        
        public ChangedHistory()
        {
        }

        public ChangedHistory(DataRow dr)
        {
            ColumnName = dr["ColumnName"].ToString();
            LastModified = dr["ModifiedOn"] == DBNull.Value ? string.Empty : Convert.ToDateTime(dr["ModifiedOn"]).ToShortDateString();
            Value = dr["Value"].ToString();
        

            RequestedBy = dr["RequestedBy"].ToString();
            RequestedOn = Convert.ToDateTime(dr["RequestedOn"]).ToShortDateString() ;
            ApprovalStatus = dr["ApprovalStatus"].ToString();
            LastModifiedBy = dr["LastModifiedBy"] == DBNull.Value ? string.Empty : dr["LastModifiedBy"].ToString();
            MIMOS = dr["MIMOS"].ToString();
            Comments = dr["Comments"].ToString();
            RequestId = dr["RequestId"].ToString();            
            RowId = dr["RowId"].ToString() ;
            
        }
    }
}
