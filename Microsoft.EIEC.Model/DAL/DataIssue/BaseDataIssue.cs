using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.XPath;

namespace Microsoft.EIEC.Model.DAL.DataIssue
{
    public abstract class BaseDataIssue
    {
        public abstract IList<T> GetData<T>(string issueStatusCode = null);

        public abstract IList<T> GetDetails<T>(string keyField);

        public virtual IList<ChangedHistory> GetChangeHistory(string keyField, string fieldName, int TemplateId, int requestTypeId)
        {
            IList<ChangedHistory> changedHistories = null;

            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@TableId", SqlDbType.VarChar, TemplateId);
                dbl.AddParam("@Id", SqlDbType.VarChar, keyField);
                dbl.AddParam("@RequestTypeID", SqlDbType.Int, requestTypeId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_ChangedHistory");
            }

            if (dtData != null
                && dtData.Rows != null)
                changedHistories = (from DataRow dr in dtData.Rows select new ChangedHistory(dr)).ToList();

            return changedHistories;
        }

        protected static DataTable GetDBData(string spName, Dictionary<string, string> spParameters, out string outMessage)
        {
            outMessage = string.Empty;

            using (DatabaseLayer dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                foreach (KeyValuePair<string, string> parameter in spParameters)
                {
                    switch (parameter.Key)
                    {
                        case "@AgreementId":
                            if (!string.IsNullOrEmpty(parameter.Value))
                                dbl.AddParam(parameter.Key, SqlDbType.VarChar, parameter.Value);
                            break;

                        case "@ErrorStatusCode":
                            if (!string.IsNullOrEmpty(parameter.Value))
                                dbl.AddParam(parameter.Key, SqlDbType.VarChar, parameter.Value);
                            break;

                        case "@InvoiceDocumentNumber":
                            if (!string.IsNullOrEmpty(parameter.Value))
                                dbl.AddParam(parameter.Key, SqlDbType.VarChar, parameter.Value);
                            break;

                        case "@OpportunityGlobalCRMId":
                            if (!string.IsNullOrEmpty(parameter.Value))
                                dbl.AddParam(parameter.Key, SqlDbType.VarChar, parameter.Value);
                            break;

                        case "@InvoiceId":
                            if (!string.IsNullOrEmpty(parameter.Value))
                                dbl.AddParam(parameter.Key, SqlDbType.VarChar, parameter.Value);
                            break;

                        case "@InvoiceInternalId":
                            if (!string.IsNullOrEmpty(parameter.Value))
                                dbl.AddParam(parameter.Key, SqlDbType.BigInt, Convert.ToInt32(parameter.Value));
                            break;
                    }

                }

                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);

                var dtData = dbl.ExecuteStoredProcedure(spName);

                if (spUserMessage.Value != null 
                    && spUserMessage.Value != DBNull.Value)
                {
                    outMessage = spUserMessage.Value.ToString();
                }

                return dtData;
            }

        }

        public IssueStatistics GetIssuesStats()
        {
            IssueStatistics issueStats = null;

            DataSet dsIssueDetails;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dsIssueDetails = dbl.ExecuteStoredProcedure_DS("REP.Get_IssueStats");
            }

            DataTable dtDetails;
            if (dsIssueDetails != null && dsIssueDetails.Tables.Count > 0)
            {
                issueStats=new IssueStatistics();
                if (dsIssueDetails.Tables[0] != null)
                {
                    dtDetails = dsIssueDetails.Tables[0];
                    issueStats.CurrentIssues = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
                }
                if (dsIssueDetails.Tables[1] != null)
                {
                    dtDetails = dsIssueDetails.Tables[1];
                    issueStats.PendingRequests = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
                }

                if (dsIssueDetails.Tables.Count > 2)
                {

                    if (dsIssueDetails.Tables[2] != null)
                    {
                        dtDetails = dsIssueDetails.Tables[2];
                        issueStats.RequestTypeSummary = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
                    }
                }
            }
            return issueStats;
        }

        public DataTable GetIssuesData(string Id, int TemplateId, int RequestTypeId, out string userMessage)
        {
            userMessage = string.Empty;
            DataTable dtData;
            try
            {
                using (DatabaseLayer dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@Id", SqlDbType.VarChar, Id);
                    dbl.AddParam("@TableId", SqlDbType.Int, TemplateId);
                    dbl.AddParam("@RequestTypeId", SqlDbType.Int, RequestTypeId);
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                    dtData = dbl.ExecuteStoredProcedure("REP.Get_IssuesData");
                    if (dtData != null && dtData.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                    {
                        userMessage = spUserMessage.Value.ToString();
                    }
                    dtData.TableName = "IssueTable";
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dtData;
        }


        public string SaveOperationOnEntity(IList<string> keyfields, int TableId, int RequestTypeId)
        {
            string userMsg = string.Empty;
            using (SaveHelper helper = new SaveHelper())
            {
                using (DatabaseLayer dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    XPathNavigator nav = xmlDoc.CreateNavigator();
                    using (XmlWriter writer = nav.AppendChild())
                    {
                        XmlSerializer ser = new XmlSerializer(typeof(List<string>),
                        new XmlRootAttribute("ExcluEntitiesList"));
                        ser.Serialize(writer, keyfields);
                    }

                    dbl.AddParam("@DataFromSheet", SqlDbType.Xml, xmlDoc.InnerXml);
                    dbl.AddParam("@TableId", SqlDbType.Int, TableId);
                    dbl.AddParam("@RequestTypeId", SqlDbType.Int, RequestTypeId);
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);

                    SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                    dbl.ExecuteNonQueryStoredProcedure("[REP].[Save_OperationOnEntity]");
                    if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                    {
                        userMsg = spUserMessage.Value.ToString();
                    }
                }
            }

            return userMsg;
        }
    }
}
