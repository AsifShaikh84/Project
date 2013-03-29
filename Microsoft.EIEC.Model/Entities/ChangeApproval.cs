using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
     [Serializable]
    [DataContract]
    public class ChangeApproval
    {
        [DataMember]
        public int RequestId { get; set; }
        [DataMember]
        public string DisplayRequestId { get; set; }
        [DataMember]
        public string RequestedBy { get; set; }
        [DataMember]
        public DateTime RequestedOn { get; set; }
        [DataMember]
        public string RequestType { get; set; }
        [DataMember]
        public string ExternalKey { get; set; }
        [DataMember]
        public string ChangeValue { get; set; }
        [DataMember]
        public string MIMOS { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public string RequestStatus { get; set; }
        [DataMember]
        public int RequiredApprovalCount { get; set; }
        [DataMember]
        public string AssignedTo { get; set; }
        [DataMember]
        public string ROC { get; set; }
        [DataMember]
        public string RowId { get; set; }
         


        public ChangeApproval()
        {
        }

        public ChangeApproval(DataRow dr)
        {
            RequestId = Convert.ToInt32(dr["RequestId"]);
            RequestStatus = dr["StatusCode"].ToString();
            DisplayRequestId = dr["DisplayRequestId"].ToString();
            RequestedBy = dr["RequestedBy"].ToString();
            RequestedOn = Convert.ToDateTime(dr["RequestedOn"]);
            RequestType = dr["RequestType"].ToString();
            ExternalKey = dr["ExternalKey"].ToString();
            ChangeValue = dr["DisplayValue"].ToString();
            MIMOS = dr["MIMOS"].ToString();
            Comments = dr["Comments"].ToString();
            RequiredApprovalCount = Convert.ToInt32(dr["RequiredApprovalCount"]);
            AssignedTo = dr["AssignedTo"].ToString();
        }

    }
}
