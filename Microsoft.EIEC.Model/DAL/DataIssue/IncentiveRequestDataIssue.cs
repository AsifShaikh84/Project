using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;

namespace Microsoft.EIEC.Model.DAL.DataIssue
{
    public class IncentiveRequestDataIssue : CommonDataIssue
    {
        #region Public Methods
        public override IList<T> GetData<T>(string issueStatusCode = null)
        {
            return (IList<T>)GetIncompleteOpportunities(string.Empty, string.Empty);
        }

        public override IList<T> GetDetails<T>(string keyField)
        {
            return (IList<T>)GetOpportunityIncentiveRequestData(keyField);
        }
        #endregion

        #region Private Methods

        private IList<IncompleteOpportunities> GetIncompleteOpportunities(string opportunityCRMId, string incentiveRequestId)
        {
            string message;
            var parameters = new Dictionary<string, string>
                                                        {
                                                            {"@OpportunityGlobalCRMId", opportunityCRMId},
                                                            {"@incentiveRequestId", incentiveRequestId}
                                                        };

            var dtIncompleteOpportunities = GetDBData("REP.Get_IncompleteOpportunities", parameters, out message);

            return (from DataRow dr in dtIncompleteOpportunities.Rows select new IncompleteOpportunities(dr)).ToList();
        }

        private IList<OpportunityIncentiveRequest> GetOpportunityIncentiveRequestData(string keyField)
        {
            string message;
            var parameters = new Dictionary<string, string> { { "@OpportunityGlobalCRMId", keyField } };

            var dtOpportunityIncentiveRequest = GetDBData("REP.Get_OpportunityIncentiveRequests", parameters, out message);

            return (from DataRow dr in dtOpportunityIncentiveRequest.Rows select OpportunityIncentiveRequest.CreateOpportunityIncentiveRequest(dr)).ToList();
        }
        #endregion
    }
}
