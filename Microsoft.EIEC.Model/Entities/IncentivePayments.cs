using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class IncentivePayments
    {
        [DataMember]
        public int IncentivePayableId { get; set; }
        [DataMember]
        public string PartnerPCN { get; set; }
        [DataMember]
        public string PartnerName  { get; set; }
        [DataMember]
        public string PartnerBillToNumber { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string PaymentMethod { get; set; }
        [DataMember]
        public decimal Amount { get; set; }
        [DataMember]
        public decimal LastTransmittedAmount { get; set; }
        [DataMember]
        public decimal Difference { get; set; }
        [DataMember]
        public bool PartnerPreview { get; set; }
        [DataMember]
        public bool IsTransmitted { get; set; }
        [DataMember]
        public string LastTransmittedOn { get; set; }
        [DataMember]
        public string FiscalMonth { get; set; }
        [DataMember]
        public string IncentiveProgramId { get; set; }
        [DataMember]
        public string PartnerInternalId { get; set; }
        [DataMember]
        public int IsApproved { get; set; }
        [DataMember]
        public string PartnerPCNBill { get; set; }
        [DataMember]
        public string PaymentMethodIncentivePayableId { get; set; }
        [DataMember]
        public string IncentiveProgramName { get; set; }
        [DataMember]
        public string ROC { get; set; }
        [DataMember]
        public bool CHIPUploadStatus { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
                

        public IncentivePayments()
        {
        }

        public static IncentivePayments CreateIncentivePayment(DataRow dr)
        {
            //string appendPartnerBillIfNotEmpty = string.IsNullOrEmpty(dr["PartnerBillToNumber"].ToString()) ? "]" : "] [" + dr["PartnerBillToNumber"].ToString() + "]";
            IncentivePayments p = new IncentivePayments
                                      {
                                          IncentivePayableId = Convert.ToInt32(dr["IncentivePayableId"]),
                                          PartnerPCN = dr["PartnerPCN"].ToString(),
                                          PartnerName = dr["PartnerName"].ToString(),
                                          PartnerBillToNumber = dr["PartnerBillToNumber"].ToString(),
                                          Currency = dr["Currency"].ToString(),
                                          PaymentMethod = dr["PaymentMethod"].ToString(),
                                          Amount =Convert.ToDecimal(dr["Amount"]),
                                          LastTransmittedAmount = Convert.ToDecimal(dr["LastTransmittedAmount"]),
                                          Difference = Convert.ToDecimal(dr["Difference"]),
                                          PartnerPreview = Convert.ToBoolean(dr["PartnerPreview"]),
                                          IsTransmitted = Convert.ToBoolean(dr["IsTransmitted"]),
                                          LastTransmittedOn = dr["LastTransmittedOn"].ToString(),
                                          FiscalMonth = dr["FiscalMonth"].ToString(),
                                          PartnerInternalId = dr["PartnerInternalId"].ToString(),
                                          IncentiveProgramId = dr["IncentiveProgramId"].ToString(),
                                          IncentiveProgramName = dr["IncentiveProgramName"].ToString(),
                                          IsApproved = int.Parse( dr["IsApproved"].ToString()),
                                          PartnerPCNBill = "[" + dr["PartnerName"].ToString() + "] [" + dr["PartnerPCN"].ToString() + (string.IsNullOrEmpty(dr["PartnerBillToNumber"].ToString()) ? "]" : "] [" + dr["PartnerBillToNumber"].ToString() + "]"),
                                          PaymentMethodIncentivePayableId = dr["PaymentMethod"].ToString() + "." + dr["IncentivePayableId"].ToString()  ,          
                                          ROC =  dr["ROC"].ToString(),
                                          CHIPUploadStatus = Convert.ToBoolean(dr["CHIPUploadStatus"]),
                                          ErrorMessage = dr["ErrorMessage"].ToString()
                                      };

            
            return p;
        }
    }
}
