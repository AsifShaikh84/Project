using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using System.Configuration;

namespace Microsoft.EIEC.Model.DAL
{
    public class InvoiceDataIssueContext : IDataIssueContext
    {

        public string KeyFieldName
        {
            get { return "RowId"; }
        }

        public IEnumerable<object> GetData(string issueStatusCode = null)
        {
            return GetIncompleteEnrollments(string.Empty, issueStatusCode);
        }

        public IEnumerable<object> GetDetails(string keyfield)
        {
            Invoice invoice = new Invoice();
            DataTable dtInvoices = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                if (!string.IsNullOrEmpty(keyfield))
                    dbl.AddParam("@InvoiceInternalId", SqlDbType.BigInt, Convert.ToInt32(keyfield));
                dtInvoices = dbl.ExecuteStoredProcedure("Get_Invoices");
            }

            foreach (DataRow dr in dtInvoices.Rows)
            {
                invoice = Invoice.CreateInvoice(dr);
            }

            return invoice.TransposeToFieldValue();
        }

        public IEnumerable<object> GetChangeHistory(string keyfield, string fieldName)
        {
            List<ChangedHistory> ChangedHistories = new List<ChangedHistory>();

            DataTable dtData = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                dbl.AddParam("@TableName", SqlDbType.VarChar, "Invoice");
                dbl.AddParam("@FieldName", SqlDbType.VarChar, fieldName);
                dbl.AddParam("@Id", SqlDbType.VarChar, keyfield);
                dtData = dbl.ExecuteStoredProcedure("Get_ChangedHistory");
            }

            foreach (DataRow dr in dtData.Rows)
            {
                ChangedHistories.Add(new ChangedHistory(dr));
            }

            return ChangedHistories;
        }

        public string SaveChangeRequest(ChangeRequest cr)
        {
            cr.TableName = "Invoice";
            List<ChangeRequest> crList = new List<ChangeRequest>() { cr };
            //return SaveHelper.SaveChanges(crList, "Save_CreateChangeRequest", "");
            return null;

        }

        private IEnumerable<IncompleteInvoices> GetIncompleteEnrollments(string invoiceDocumentNumber, string issueStatusCode)
        {
            List<IncompleteInvoices> incompleteInvoices = new List<IncompleteInvoices>();
            DataTable dtIncompleteInvoices = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                if (!string.IsNullOrEmpty(invoiceDocumentNumber))
                    dbl.AddParam("@InvoiceDocumentNumber", SqlDbType.VarChar, invoiceDocumentNumber);
                dtIncompleteInvoices = dbl.ExecuteStoredProcedure("Get_IncompleteInvoices");
            }

            foreach (DataRow dr in dtIncompleteInvoices.Rows)
            {
                incompleteInvoices.Add(new IncompleteInvoices(dr));
            }

            return incompleteInvoices;
        }

    }
}
