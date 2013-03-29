using System;
using System.Collections.Generic;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using System.Configuration;

namespace Microsoft.EIEC.Model.DAL
{
    public class IncentiveRequestDataIssueContext : IDataIssueContext
    {
        public string KeyFieldName
        {
           // get { return "OpportunityGlobalCRMId"; }
            get { return "GlobalCRMId"; }
        }

        public IEnumerable<object> GetData(string issueStatusCode = null)
        {
            return GetIncompleteOpportunities(string.Empty, string.Empty, issueStatusCode);
        }

        public IEnumerable<object> GetDetails(string keyfield)
        {
            OpportunityIncentiveRequest opportunityIncentiveRequest = new OpportunityIncentiveRequest();
            DataTable dtOpportunityIncentiveRequest = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                if (!string.IsNullOrEmpty(keyfield))
                    dbl.AddParam("@OpportunityGlobalCRMId", SqlDbType.VarChar, keyfield);
                dtOpportunityIncentiveRequest = dbl.ExecuteStoredProcedure("Get_OpportunityIncentiveRequests");
            }

            foreach (DataRow dr in dtOpportunityIncentiveRequest.Rows)
            {
                opportunityIncentiveRequest = OpportunityIncentiveRequest.CreateOpportunityIncentiveRequest(dr);
            }

            return opportunityIncentiveRequest.TransposeToFieldValue();   
        }

        private IEnumerable<IncompleteOpportunities> GetIncompleteOpportunities(string opportunityCRMId, string incentiveRequestId, string issueStatusCode)
        {
            List<IncompleteOpportunities> IncompleteOpportunities = new List<IncompleteOpportunities>();
            DataTable dtIncompleteOpportunities = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                if (!string.IsNullOrEmpty(opportunityCRMId))
                    dbl.AddParam("@OpportunityGlobalCRMId", SqlDbType.VarChar, opportunityCRMId);

                if (!string.IsNullOrEmpty(incentiveRequestId))
                    dbl.AddParam("@incentiveRequestId", SqlDbType.BigInt, Convert.ToInt32(incentiveRequestId));

                dtIncompleteOpportunities = dbl.ExecuteStoredProcedure("Get_IncompleteOpportunities");
            }

            foreach (DataRow dr in dtIncompleteOpportunities.Rows)
            {
                IncompleteOpportunities.Add(new IncompleteOpportunities(dr));
            }

            return IncompleteOpportunities;
        }

        public IEnumerable<object> GetChangeHistory(string keyfield, string fieldName)
        {
            List<ChangedHistory> ChangedHistories = new List<ChangedHistory>();

            DataTable dtData = new DataTable();
            using (DatabaseLayer dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                dbl.AddParam("@TableName", SqlDbType.VarChar, "OpportunityTransaction");
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
            cr.TableName = "OpportunityTransaction";
            List<ChangeRequest> crList = new List<ChangeRequest>() { cr };
            //return SaveHelper.SaveChanges(crList, "Save_CreateChangeRequest", "");
            return null;

        }
    }
}
