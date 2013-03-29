using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;

namespace Microsoft.EIEC.Model.DAL.DataIssue
{
    public class EnrollmentDataIssue : CommonDataIssue
    {
        #region Public MethodsIncompleteEnrollment       
        public override IList<T> GetData<T>(string issueStatusCode = null)
        {
            return (IList<T>)GetIncompleteEnrollments(string.Empty, issueStatusCode);
        }

        public override IList<T> GetDetails<T>(string keyField)
        {
            return (IList<T>)GetAgreements(keyField);
        }

        #endregion

        #region Private Methods
        private IList<IncompleteEnrollment> GetIncompleteEnrollments(string agreementId, string issueStatusCode)
        {
            string message;

            var parameters = new Dictionary<string, string>
                                                        {
                                                            {"@AgreementId", agreementId},
                                                            {"@ErrorStatusCode", issueStatusCode}
                                                        };

            DataTable dtIncompleteEnrollments = GetDBData("REP.Get_IncompleteEnrollments", parameters, out message);

            return dtIncompleteEnrollments != null ? (from DataRow dr in dtIncompleteEnrollments.Rows select new IncompleteEnrollment(dr)).ToList() : null;
        }

        private IList<Agreement> GetAgreements(string keyField)
        {
            IList<Agreement> agreement = null;
            string message;
            try
            {
                var parameters = new Dictionary<string, string> { { "@AgreementId", keyField } };

                var dtAgreements = GetDBData("REP.Get_Agreements", parameters, out message);

                if (dtAgreements != null)
                {
                    agreement = (from DataRow dr in dtAgreements.Rows select Agreement.CreateAgreement(dr)).ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return agreement;
        }

        #endregion
    }
}
