using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class Invoice : ITranspose
    {

        [DataMember]
        public int RowId { get; set; }
        [DataMember]
        public string InvoiceDocumentNumber { get; set; }
        [DataMember]
        public string AgreementID { get; set; }
        [DataMember]
        public string PurchaseOrderNumber { get; set; }
        [DataMember]
        public string SubsidiaryName { get; set; }
        [DataMember]
        public string CurrencyName { get; set; }
        [DataMember]
        public DateTime InvoiceCreatedDate { get; set; }
        [DataMember]
        public bool IsApproved { get; set; }
        [DataMember]
        public bool IsActive { get; set; }

        public Invoice()
        {
        }

        public static Invoice CreateInvoice(DataRow dr)
        {

            Invoice p = new Invoice
                            {
                                RowId = Convert.ToInt32(dr["RowId"]),
                                InvoiceDocumentNumber = dr["InvoiceDocumentNumber"].ToString(),
                                AgreementID = dr["AgreementID"].ToString(),
                                PurchaseOrderNumber = dr["PurchaseOrderNumber"].ToString(),
                                SubsidiaryName = dr["SubsidiaryName"].ToString(),
                                CurrencyName = dr["CurrencyName"].ToString(),
                                InvoiceCreatedDate = Convert.ToDateTime(dr["InvoiceCreatedDate"]),
                                IsApproved = Convert.ToBoolean(dr["IsApproved"]),
                                IsActive = Convert.ToBoolean(dr["IsActive"])
                            };

            return p;
        }

        public IList<FieldValueTranspose> TransposeToFieldValue()
        {
            return (from info in GetType().GetProperties() where info.CanRead let o = info.GetValue(this, null) select new FieldValueTranspose(info.Name, o, true)).ToList();
        }
    }
}
