using System;
using System.Data;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    [DataContract]
    public class PartnerComplianceScore
    {

        [DataMember]
        public int ComplianceTypeId { get; set; }

        [DataMember]
        public string ComplianceTypeName { get; set; }
        [DataMember]
        public string PreviousMonth { get; set; }

        [DataMember]
        public string CurrentMonth { get; set; }

        [DataMember]
        public int PartnerinternalId { get; set; }

        [DataMember]
        public string Override_AssessmentValue { get; set; }

        [DataMember]
        public string Override_MimosNumber { get; set; }

        [DataMember]
        public string Override_Comments { get; set; }

        [DataMember]
        public string Override_AssignedTo { get; set; }

        [DataMember]
        public int RowId { get; set; }

        [DataMember]
        public int RequestId { get; set; }

        [DataMember]
        public Int16 RequestTypeId { get; set; }

        [DataMember]
        public string RequestStatusCode { get; set; }

        [DataMember]
        public string PartnerName { get; set; }

        [DataMember]
        public int RequiredApprovalCount { get; set; }

        [DataMember]
        public string ROCCode { get; set; }

        [DataMember]
        public string PartnerPCN { get; set; }

        [DataMember]
        public string ComplianceDataType { get; set; }

        public static PartnerComplianceScore CreatePartnerComplianceScore(DataRow dr)
        {
            PartnerComplianceScore score = new PartnerComplianceScore();
            score.ComplianceTypeId = Convert.ToInt32(dr["ComplianceTypeId"]);

            score.RowId = Convert.ToInt32(dr["RowId"]);

            if (!DBNull.Value.Equals(dr["RequestId"]))
            {
                score.RequestId = Convert.ToInt32(dr["RequestId"]);
                score.RequestStatusCode = dr["RequestStatusCode"].ToString();
            }
            else
            {
                score.RequestId = -1;
                score.RequestStatusCode = string.Empty;
            }

            score.ComplianceTypeName = dr["ComplianceTypeName"].ToString();
            score.PartnerName = dr["PartnerName"].ToString();
            score.ROCCode = dr["ROCCode"].ToString();
            score.RequestTypeId = Convert.ToInt16(dr["RequestTypeId"]);
            score.PreviousMonth = dr["PreviousMonth"].ToString();
            score.CurrentMonth = dr["CurrentMonth"].ToString();

            if (!DBNull.Value.Equals(dr["PartnerPCN"]))
            {
                score.PartnerPCN = dr["PartnerPCN"].ToString();
            }
            if (!DBNull.Value.Equals(dr["ComplianceDataType"]))
            {
                score.ComplianceDataType = dr["ComplianceDataType"].ToString();
            }
            return score;
        }
    }
}
