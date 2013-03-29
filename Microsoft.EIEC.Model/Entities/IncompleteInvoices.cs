using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class IncompleteInvoices
    {
        [DataMember]
        public string ProblemDescription { get; set; }       
        [DataMember]
        public string AgreementNumber { get; set; }
        [DataMember]
        public string PartnerPCN { get; set; }
        [DataMember]
        public int RowId { get; set; }
        [DataMember]
        public string InvoiceDocumentNumber { get; set; }
        [DataMember]
        public DateTime InvoiceCreatedDate { get; set; }
        [DataMember]
        public double MSSalesRevenue { get; set; }
        [DataMember]
        public double LIRRevenue { get; set; }
        [DataMember]
        public string OperationCenter { get; set; }

        public IncompleteInvoices()
        {
        }

        public IncompleteInvoices(DataRow dr)
        {
            ProblemDescription = dr["ProblemDescription"].ToString();
            AgreementNumber = dr["AgreementNumber"].ToString();
            PartnerPCN = dr["PartnerPCN"].ToString();
            RowId = Convert.ToInt32(dr["RowId"]);
            InvoiceDocumentNumber = dr["InvoiceDocumentNumber"].ToString();
            InvoiceCreatedDate = Convert.ToDateTime(dr["InvoiceCreatedDate"]);
            MSSalesRevenue = Convert.ToDouble(dr["MSSalesRevenue"]);
            LIRRevenue = Convert.ToDouble(dr["LIRRevenue"]);
            OperationCenter = dr["OperationCenter"].ToString();
        }
    }
}
