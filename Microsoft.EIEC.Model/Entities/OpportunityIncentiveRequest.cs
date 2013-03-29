using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class OpportunityIncentiveRequest : ITranspose
    {

        [DataMember]
        public int RowId { get; set; }
        [DataMember]
        public string OpportunityGlobalCRMId { get; set; }
        [DataMember]
        public int OpportunityId { get; set; }
        [DataMember]
        public string OpportunityName { get; set; }
        [DataMember]
        public string OpportunityCustomerName { get; set; }
        [DataMember]
        public string IncentiveRequestTypeName { get; set; }
        [DataMember]
        public string SalesStage {get;set;}
        [DataMember]
        public string AgreementNumber { get; set; }
        [DataMember]
        public DateTime IncentiveRequestCreatedDate {get ; set;}
        [DataMember]
        public string PurchaseOrderNumber {get ; set;}
        [DataMember]
        public DateTime PurchaseOrderCreatedDate {get ; set;}
        [DataMember]
        public string PAMApprovalStatus { get; set; }
        [DataMember]
        public bool RequestedOnTime { get; set; }


        
        public OpportunityIncentiveRequest()
        {
        }

        public static OpportunityIncentiveRequest CreateOpportunityIncentiveRequest(DataRow dr)
        {
            OpportunityIncentiveRequest p = new OpportunityIncentiveRequest
                                                {
                                                    RowId = Convert.ToInt32(dr["RowId"]),
                                                    OpportunityGlobalCRMId = dr["OpportunityGlobalCRMId"].ToString(),
                                                    OpportunityName = dr["OpportunityName"].ToString(),
                                                    OpportunityCustomerName = dr["OpportunityCustomerName"].ToString(),
                                                    IncentiveRequestTypeName = dr["IncentiveRequestTypeName"].ToString(),
                                                    SalesStage = dr["SalesStageName"].ToString(),
                                                    AgreementNumber = dr["AgreementId"].ToString(),
                                                    IncentiveRequestCreatedDate = Convert.ToDateTime(dr["IncentiveRequestCreatedDate"]),
                                                    PAMApprovalStatus = dr["PAMApprovalStatus"].ToString(),
                                                    RequestedOnTime = Convert.ToBoolean(dr["RequestedOnTime"] ?? 0)
                                                };

            if(dr["PurchaseOrderNumber"] != DBNull.Value)
                p.PurchaseOrderNumber = dr["PurchaseOrderNumber"].ToString();
            
            if(dr["PurchaseOrderCreatedDate"] != DBNull.Value)
                p.PurchaseOrderCreatedDate = Convert.ToDateTime(dr["PurchaseOrderCreatedDate"]);
            
            return p;
        }

        public IList<FieldValueTranspose> TransposeToFieldValue()
        {
            return (from info in GetType().GetProperties() where info.CanRead let o = info.GetValue(this, null) select new FieldValueTranspose(info.Name, o, true)).ToList();
        }
    }
}
