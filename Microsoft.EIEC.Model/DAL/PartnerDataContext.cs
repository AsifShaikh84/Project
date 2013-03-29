using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using Microsoft.EIEC.DataLayer;
using System.Data;
using System.Data.Linq;
using System.Collections.ObjectModel;
using Microsoft.EIEC.Model.Helper;
using System.Threading;

namespace Microsoft.EIEC.Model.DAL
{
    public class PartnerDataContext
    {
        public IList<Partner> GetPartners()
        {
            IList<Partner> partnerList = null;
            DataTable dtUserAccess;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dtUserAccess = dbl.ExecuteStoredProcedure("REP.Get_Partner");
            }

            if (dtUserAccess != null)
            {
                partnerList = (from DataRow dr in dtUserAccess.Rows select Partner.CreatePartner(dr)).ToList();
            }
            return partnerList;
        }

        public string SavePartnerDetails(Partner partner)
        {
            List<Partner> partnerList = new List<Partner>() { partner };
            return SaveHelper.SaveChanges(partnerList, "REP.Save_PartnerIncentiveProgram", "",null);

        }

        public ICollection<PartnerComplianceScore> GetPartnerComplianceScoreReport(string ComplianceTypes, string Period, string PartnerName,string partnerPCN,out string userMessage)
        {
            Collection<PartnerComplianceScore> scoreCollection = null;
            userMessage = string.Empty;
            DataTable scoreTable;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ComplianceType", SqlDbType.NVarChar, ComplianceTypes);
                dbl.AddParam("@Period", SqlDbType.NVarChar, Period);
                dbl.AddParam("@PartnerName", SqlDbType.NVarChar, PartnerName);
                dbl.AddParam("@PartnerPCN", SqlDbType.NVarChar, partnerPCN);
                dbl.AddParam("@AccessingUser", SqlDbType.VarChar, Thread.CurrentPrincipal.Identity.Name);
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                scoreTable = dbl.ExecuteStoredProcedure("REP.Get_ComplianceReport");
                if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value && scoreTable!=null && scoreTable.Rows.Count == 0)
                {
                    userMessage = spUserMessage.Value.ToString();
                }

                if(scoreTable.Rows.Count > 0)
                {
                    DataView dv = scoreTable.DefaultView;
                    dv.Sort = "PartnerName";
                    scoreTable = dv.ToTable();
                }

            }

            if (scoreTable != null)
            {
                scoreCollection = new Collection<PartnerComplianceScore>();
                foreach (DataRow dr in scoreTable.Rows)
                {
                    scoreCollection.Add(PartnerComplianceScore.CreatePartnerComplianceScore(dr));
                }
            }

            return scoreCollection;
        }


        private ICollection<ChangedHistory> GetComplianceChangedHistory(string tableId, string fieldName, string id)
        {
            ICollection <ChangedHistory> changedHistories = null;

            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@TableId", SqlDbType.Int, int.Parse(tableId));
                dbl.AddParam("@FieldName", SqlDbType.VarChar, "RowId");
                dbl.AddParam("@Id", SqlDbType.VarChar, id);
                dbl.AddParam("@AccessingUser", SqlDbType.VarChar, Thread.CurrentPrincipal.Identity.Name);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_ChangedHistory");
            }

            if (dtData != null)
            {
                changedHistories = new Collection<ChangedHistory>();
                foreach (DataRow dr in dtData.Rows)
                {
                    changedHistories.Add(new ChangedHistory(dr));
                }
            }

            return changedHistories;
        }

        public ICollection<ChangedHistory> GetComplianceChangeHistory(string tableId, string fieldName, string partnerPCN, int complianceTypeId, string fiscalMonth)
        {
            ICollection<ChangedHistory> changedHistories;

            try
            {
                //int id = GetComplianceId(partnerPCN, complianceTypeId, CommonDataSources.GetFiscalMonth());                
                changedHistories = GetComplianceChangedHistory(tableId, fieldName, partnerPCN);

            }
            catch (Exception)
            {
                throw;
            }
           
            return changedHistories;
        }

        public string SaveChangeRequest(TaskRequest cr, string partnerPCN, int complianceTypeId, string fiscalMonth,int programBrandId)
        {         
            var crList = new List<TaskRequest>() { cr };
            return SaveHelper.SaveChanges(crList, "REP.Save_Request", "", programBrandId);
        }

        public string SaveChangeRequest(IList<TaskRequest> lstchangedPartnerCompliance, int programBrandId)
        {
            return SaveHelper.SaveChanges(lstchangedPartnerCompliance.ToList(), "REP.Save_Request", "", programBrandId);
        }

        public PartnerStatistics GetPartnerStats()
        {
            PartnerStatistics partnerStats = new PartnerStatistics();

            DataSet dsIssueDetails;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dsIssueDetails = dbl.ExecuteStoredProcedure_DS("REP.Get_partnerStats");
            }

            DataTable dtDetails;
            if (dsIssueDetails.Tables[0] != null)
            {
                dtDetails = dsIssueDetails.Tables[0];
                partnerStats.PartnerDetails = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
            }
            return partnerStats;
        }

        public IList<ComplianceDetails> GetComplianceTypes(string ProgramType, int ProgramBrandId, out string dbOutputString)
        {
            List<ComplianceDetails> ComplianceTypes = null;
            dbOutputString = "Success";

            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {                       
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@ProgramType", SqlDbType.NVarChar, ProgramType);
                dbl.AddParam("@ProgramBrandId", SqlDbType.Int, ProgramBrandId);     
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_ComplianceTypes");
                if (spUserMessage.Value != null
                 && spUserMessage.Value != DBNull.Value)
                {
                    dbOutputString = spUserMessage.Value.ToString();
                }
            }

            if (dtData != null)
            {
                ComplianceTypes = new List<ComplianceDetails>();
                foreach (DataRow dr in dtData.Rows)
                {
                    ComplianceTypes.Add(ComplianceDetails.ConvertRowToValue(dr));
                }
            }

            return ComplianceTypes;
        }


        public IList<PeriodDetails> GetPeriodValues(string ListName, int ProgramBrandId)
        {
            List<PeriodDetails> lstPeriodDetails = null;

            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ListName", SqlDbType.VarChar, ListName);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, ProgramBrandId);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_GeneralLookups");
            }

            if (dtData != null)
            {
                lstPeriodDetails = new List<PeriodDetails>();
                foreach (DataRow dr in dtData.Rows)
                {
                    lstPeriodDetails.Add( new PeriodDetails().ConvertFromDatabase(dr));
                }
            }

            return lstPeriodDetails;
        }


        public IList<KeyValueParameter> GetTaxonomyDetails()
        {
            List<KeyValueParameter> lstPeriodDetails = null;

            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_SubRegions");
            }

            if (dtData != null)
            {
                lstPeriodDetails = new List<KeyValueParameter>();
                foreach (DataRow dr in dtData.Rows)
                {
                    lstPeriodDetails.Add(new KeyValueParameter().ConvertRowToValue(dr));
                }
            }

            return lstPeriodDetails;
        }

        public IList<Partner> GetpartnerAuthorizationDetails(int programBrandId, int partnerProgramRuleTypeId)
        {
            List<Partner> lstpartner = null;

            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@PartnerProgramRuleType", SqlDbType.SmallInt, partnerProgramRuleTypeId);
                dbl.AddParam("@ProgramBrandId", SqlDbType.Int, programBrandId);
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_partnerAuthorizationDetails");
            }

            if (dtData != null)
            {
                lstpartner = (from DataRow dr in dtData.Rows select Partner.CreatePartner(dr)).ToList();
            }

            return lstpartner;
        }

        public string SaveParnerAuthorizationDetails(IList<Partner> ChangedPartners,int programBrandId)
        {
            return SaveHelper.SaveChanges(ChangedPartners.ToList(), "REP.Save_PartnerAutorizationDetails","",programBrandId );
        }

    }
}
