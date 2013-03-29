using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using System.Configuration;
using Microsoft.EIEC.Model.Helper;
using System.Xml;

namespace Microsoft.EIEC.Model.DAL
{
    public class EnrollmentDataIssueContext : IDataIssueContext
    {

        public string KeyFieldName
        {
            get { return "Enrollment"; }
        }

        public IEnumerable<object> GetData(string issueStatusCode = null)
        {
            return GetIncompleteEnrollments(string.Empty, issueStatusCode);
        }

        public IEnumerable<object> GetDetails(string keyfield)
        {
            Agreement agreement = new Agreement();
            DataTable dtAgreements = new DataTable();
            DataTable dtAgreementColumns = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                if (!string.IsNullOrEmpty(keyfield))
                    dbl.AddParam("@AgreementId", SqlDbType.VarChar, keyfield);
                dtAgreements = dbl.ExecuteStoredProcedure("Get_Agreements");
            }

            foreach (DataRow dr in dtAgreements.Rows)
            {
                agreement = Agreement.CreateAgreement(dr);
            }

            return agreement.TransposeToFieldValue();
            //return (IEnumerable<object>)agreement;
        }

        public IEnumerable<object> GetChangeHistory(string keyfield, string fieldName)
        {
            List<ChangedHistory> ChangedHistories = new List<ChangedHistory>();

            DataTable dtData = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                dbl.AddParam("@TableName", SqlDbType.VarChar, "dbo.Agreement");
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

        private IEnumerable<IncompleteEnrollment> GetIncompleteEnrollments(string agreementId, string issueStatusCode)
        {
            List<IncompleteEnrollment> incompleteEnrollments = new List<IncompleteEnrollment>();
            DataTable dtIncompleteEnrollments = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                if (!string.IsNullOrEmpty(agreementId))
                    dbl.AddParam("@AgreementId", SqlDbType.VarChar, agreementId);

                if (!string.IsNullOrEmpty(issueStatusCode))
                    dbl.AddParam("@ErrorStatusCode", SqlDbType.VarChar, issueStatusCode);

                dtIncompleteEnrollments = dbl.ExecuteStoredProcedure("Get_IncompleteEnrollments");
            }

            foreach (DataRow dr in dtIncompleteEnrollments.Rows)
            {
                incompleteEnrollments.Add(new IncompleteEnrollment(dr));
            }

            return incompleteEnrollments;
        }

        public string SaveChangeRequest(ChangeRequest cr)
        {
            cr.TableName = "Agreement";
            List<ChangeRequest> crList = new List<ChangeRequest>() { cr };
            return SaveHelper.SaveChanges(crList, "Save_CreateChangeRequest", "");
        }
    }
}
