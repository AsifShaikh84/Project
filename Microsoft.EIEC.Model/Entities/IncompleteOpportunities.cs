using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class IncompleteOpportunities
    {
        [DataMember]
        public string ProblemDescription { get; set; }
        [DataMember]
        public int RowId { get; set; }
        [DataMember]
        public string GlobalCRMId { get; set; }
        [DataMember]
        public string IncentiveRequestId { get; set; }
        [DataMember]
        public string AgreementNumber { get; set; }
        [DataMember]
        public string PartnerPCN { get; set; }
        [DataMember]
        public string PartnerName { get; set; }
        [DataMember]
        public string Month { get; set; }
        [DataMember]
        public string OperationCenter { get; set; }

        public IncompleteOpportunities()
        {
        }

        public IncompleteOpportunities(DataRow dr)
        {
            ProblemDescription = dr["ProblemDescription"].ToString();
            RowId = Convert.ToInt32(dr["RowId"]);
            GlobalCRMId = dr["GlobalCRMId"].ToString();
            IncentiveRequestId = dr["IncentiveRequestId"].ToString();
            AgreementNumber = dr["AgreementNumber"].ToString();
            PartnerPCN = dr["PartnerPCN"].ToString();
            PartnerName = dr["PartnerName"].ToString();
            Month = dr["Month"].ToString();
            OperationCenter = dr["OperationCenter"].ToString();
        }
    }
}
