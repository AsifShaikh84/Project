using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;

namespace Microsoft.EIEC.Model.DAL.DataIssue
{
    public class InvoiceDataIssue : CommonDataIssue
    {
        #region Public Methods
        public override IList<T> GetData<T>(string issueStatusCode = null)
        {
            return (IList<T>)GetIncompleteInvoices(string.Empty);
        }

        public override IList<T> GetDetails<T>(string keyField)
        {
            return (IList<T>)GetInvoices(keyField);
        }

        #endregion
        #region Private Methods
        private IList<IncompleteInvoices> GetIncompleteInvoices(string invoiceDocumentNumber)
        {
            string message;
            var parameters = new Dictionary<string, string> { { "@InvoiceDocumentNumber", invoiceDocumentNumber } };

            DataTable dtIncompleteInvoices = GetDBData("REP.Get_IncompleteInvoices", parameters, out message);

            return dtIncompleteInvoices != null ? (from DataRow dr in dtIncompleteInvoices.Rows select new IncompleteInvoices(dr)).ToList() : null;
        }

        private IList<Invoice> GetInvoices(string keyField)
        {
            string message;

            IList<Invoice> invoiceData = null;

            var parameters = new Dictionary<string, string> { { "@InvoiceDocumentNumber", keyField } };

            var dtInvoices = GetDBData("REP.Get_Invoices", parameters, out message);

            if (dtInvoices != null)
            {
                invoiceData = (from DataRow dr in dtInvoices.Rows select Invoice.CreateInvoice(dr)).ToList();
            }

            return invoiceData;
        }
        #endregion
    }
}
