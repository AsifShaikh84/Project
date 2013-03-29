using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data.Linq;
using System.Configuration;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Xml.Linq;
using System.Threading;
using System.Data.SqlClient;

namespace Microsoft.EIEC.Model.DAL
{
    public class CommonDataSources
    {       
        public static string GetFilePath(int paymentId)
        {
            DataTable dtData;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@IncentivePayableId", SqlDbType.Int, paymentId);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_ReportFileName");
            }

            return dtData != null && dtData.Rows.Count > 0 ? dtData.Rows[0].ItemArray[0].ToString() : String.Empty;
        }

        public static IList<CalculationMode> GetCalculationMode()
        {
            IList<CalculationMode> calculationModes = null;

            using (var getCalculationMode = new DataContext(GlobalParameters.ConnectionString))
            {
                try
                {
                    const string query = @"select CAST(CalculationModeId AS INT) AS CalculationModeId,
                                            CalculationModeName,
                                            IsActive,
                                            ModifiedOn,
                                            ModifiedBy,
                                            Earned,
                                            Paid,
                                            CalculationModelCode from CalculationMode(NOLOCK)";

                    var result = getCalculationMode.ExecuteQuery<CalculationMode>(query);

                    if (result != null)
                    {
                        calculationModes = result.ToList();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return calculationModes;
        }

        public static IList<IncentiveType> GetIncentiveTypes()
        {
            IList<IncentiveType> incentiveTypes = null;

            using (var getIncentiveType = new DataContext(GlobalParameters.ConnectionString))
            {
                try
                {

                    const string query = @"select CAST(IncentiveTypeId AS INT) AS IncentiveTypeId,
                                            IncentiveTypeName,
                                            IncentiveTypeCode,
                                            IsActive,
                                            ModifiedOn,
                                            ModifiedBy from IncentiveType(NOLOCK)";

                    var lstIncentiveType = getIncentiveType.ExecuteQuery<IncentiveType>(query);

                    if (lstIncentiveType != null)
                    {
                        incentiveTypes = lstIncentiveType.ToList();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return incentiveTypes;

        }

        public static IList<Template> GetTemplates(out string dbOutputString)
        {
            dbOutputString = "Success";
            IList<Template> templates = null;

            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_TemplateDetails");
                if (spUserMessage.Value != null
                  && spUserMessage.Value != DBNull.Value)
                {
                    dbOutputString = spUserMessage.Value.ToString();
                }
            }

            if (dtResult != null)
            {
                templates = (from DataRow dr in dtResult.Rows select Template.CreateTemplate(dr)).ToList();
            }

            return templates;
        }

        public static IList<TrackedTable> GetTrackedTableList(int ProgramBrandId, out string dbOutputString)
        {
            dbOutputString = "Success";
            IList<TrackedTable> templates = null;

            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@ProgramBrandId", SqlDbType.Int, ProgramBrandId);
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_TrackedTableList");
                if (spUserMessage.Value != null
                  && spUserMessage.Value != DBNull.Value)
                {
                    dbOutputString = spUserMessage.Value.ToString();
                }
            }

            if (dtResult != null)
            {
                templates = (from DataRow dr in dtResult.Rows select TrackedTable.CreateTrackedTable(dr)).ToList();
            }

            return templates;
        }

        public static System.Data.DataTable GetGeneralLookup(string listName, int programBrandId = 0)
        {
            System.Data.DataTable dtData;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@ListName", SqlDbType.VarChar, listName);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_GeneralLookups");
                dtData.TableName = "Lookup";
            }
            return dtData;
        }

        public static IList<IncentiveProgram> GetIncentivePrograms()
        {
            IList<IncentiveProgram> incentivePrograms = new List<IncentiveProgram>();
            DataTable dtIncentivePayments = GetGeneralLookup("ProgramBrand");

            foreach (DataRow dr in dtIncentivePayments.Rows)
                incentivePrograms.Add(new IncentiveProgram(dr));

            return incentivePrograms;
        }

        public static IList<IncentiveProgram> GetAllProgramBrands()
        {
            IList<IncentiveProgram> incentivePrograms = new List<IncentiveProgram>();
            DataTable dtIncentivePayments = GetGeneralLookup("ProgramBrandForAccess");

            foreach (DataRow dr in dtIncentivePayments.Rows)
                incentivePrograms.Add(new IncentiveProgram(dr));

            return incentivePrograms;
        }

        public static IList<IncentiveProgram> GetProgramBrand()
        {
            IList<IncentiveProgram> incentivePrograms = new List<IncentiveProgram>();
            DataTable dtIncentivePayments;

            using (dtIncentivePayments = new DataTable())
            {
                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    dtIncentivePayments = dbl.ExecuteStoredProcedure("REP.Get_ProgramBrand");
                }

                foreach (DataRow dr in dtIncentivePayments.Rows)
                    incentivePrograms.Add(new IncentiveProgram(dr));
            }
            return incentivePrograms;
        }        

        public static IList<QualifiedDataSource> GetDataSources()
        {
            IList<QualifiedDataSource> qualifiedDataSource = null;

            using (var getCalculationRuleData = new DataContext(GlobalParameters.ConnectionString))
            {
                const string query = @"EXEC Get_DataSource";
                
                var result = getCalculationRuleData.ExecuteQuery<QualifiedDataSource>(query);

                if (result != null)
                { 
                    qualifiedDataSource = result.ToList();
                }
                    
            }

            return qualifiedDataSource;
        }

        public static IList<string> GetRoleList(out string userMessage)
        {
            IList<AppRole> roleList = GetRoleDetails(out userMessage);
            return GetRoleNames(roleList);
        }

        public static IList<string> GetOperationCenterList()
        {
            IList<string> operationCenterList = new List<string>();
            DataTable dtOperationCenterList = GetGeneralLookup("UserOperationsCenter");
            foreach (DataRow dr in dtOperationCenterList.Rows)
                operationCenterList.Add(dr["Name"].ToString());

            return operationCenterList;
        }

        public static IList<Metadata> GetMetadata(int templateId, string friendlyName, int requestTypeID)
        {
            DataTable dtMetadataList;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@RequestTypeID", SqlDbType.Int, requestTypeID);
                if (templateId != int.MinValue)
                    dbl.AddParam("@TableId", SqlDbType.VarChar, templateId);

                if (!string.IsNullOrEmpty(friendlyName))
                    dbl.AddParam("@FriendlyName", SqlDbType.VarChar, friendlyName);

                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtMetadataList = dbl.ExecuteStoredProcedure("REP.Get_MetadataValues");
            }

            return dtMetadataList != null ? (from DataRow dr in dtMetadataList.Rows select Metadata.CreateMetadata(dr)).ToList() : null;
        }

        public static void UploadCustomRevenue_Agreement(DataTable revenueTable)
        {
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                if (revenueTable != null)
                    dbl.AddParam("@TableName", SqlDbType.Structured, revenueTable);

                dbl.ExecuteStoredProcedure("REP.Save_CustomRevenueforAgreement");
            }
        }

        public static IList<IncentiveProgram> GetIncentiveProgram()
        {
            IList<IncentiveProgram> programList = null;

            using (var getData = new DataContext(GlobalParameters.ConnectionString))
            {
                const string query = @"SELECT  CAST(IncentiveProgramId AS INT) AS IncentiveProgramId, CHIPIncentiveProgramName AS IncentiveProgramName FROM IncentiveProgram (NOLOCK) WHERE IsActive = 1";
                
                var result = getData.ExecuteQuery<IncentiveProgram>(query);

                if (result != null)
                {
                    programList = result.ToList();
                }
            }

            return programList;
        }

        public static string GetFiscalMonth()
        {
            DataTable dtData;

            using (var dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@ListName", SqlDbType.VarChar, "FiscalMonth");
                dtData = dbl.ExecuteStoredProcedure("REP.Get_GeneralLookups");
            }

            return dtData.Rows.Count > 0 ? dtData.Rows[0].ItemArray[0].ToString() : String.Empty;
        }

        public static IList<AppRole> GetRoleDetails(out string userMessage)
        {
            DataTable dtResult;
            userMessage = string.Empty;
            SqlParameter spUserMessage = new SqlParameter(); 
            
            using (var dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_Roles");
            }
            if (dtResult != null && dtResult.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMessage = spUserMessage.Value.ToString();
            }
         
            return dtResult != null ? (from DataRow dr in dtResult.Rows select new AppRole(dr)).ToList() : null;
        }

        public static IList<ConfigTable> GetTableColumnValues(string tableName, string columnName, string columnID, int fiscalMonthId)
        {
            DataTable dtResult;

            try
            {
                using (var dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
                {
                   dbl.AddParam("@TableName", SqlDbType.NVarChar, tableName);
                   dbl.AddParam("@ColumnName", SqlDbType.NVarChar, columnName);
                   dbl.AddParam("@ColumnID", SqlDbType.NVarChar, columnID);
                   dbl.AddParam("@FiscalMonthId", SqlDbType.Int, fiscalMonthId);
                   dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                   dtResult = dbl.ExecuteStoredProcedure("REP.Get_TableColumnValues");
                }
                return dtResult != null ? (from DataRow dr in dtResult.Rows select new ConfigTable(dr)).ToList() : null;
            }
            catch (Exception)
            {
                throw;
            }
           
        }

        public static XElement GetOverrideColumnList(int TemplateId, int requestTypeID, int ProgramBrandId, string id = null)
        {
            DataTable dtOverrideColumnList;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@RequestTypeID", SqlDbType.Int, requestTypeID);
                dbl.AddParam("@ProgramBrandId", SqlDbType.Int, ProgramBrandId);
                if (TemplateId != int.MinValue)
                    dbl.AddParam("@TableId", SqlDbType.VarChar, TemplateId);
                if (!string.IsNullOrEmpty(id))
                    dbl.AddParam("@Id", SqlDbType.VarChar, id);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtOverrideColumnList = dbl.ExecuteStoredProcedure("REP.Get_OverrideColumns");
            }

            return dtOverrideColumnList == null || (dtOverrideColumnList.Rows == null || dtOverrideColumnList.Rows.Count <= 0)
                       ? null
                       : XmlHelper.ConverDataTableToXElemet(dtOverrideColumnList, string.Format("OverrideColumns{0}", "ARG0"));
        }

        public static IList<Priorities> GetPriorities()
        {
            IList<Priorities> lstPriorities = null;

            using (var getPartnerData = new DataContext(GlobalParameters.ConnectionString))
            {
                const string query = @"SET NOCOUNT ON SELECT CAST(StatusId AS INT) AS PriorityId, InternalMessage AS PriorityType from dbo.Status WHERE StatusCode IN ('RSPR_HIGH', 'RSPR_MED', 'RSPR_LOW') SET NOCOUNT OFF";

                var results = getPartnerData.ExecuteQuery<Priorities>(query).ToList<Priorities>();

                lstPriorities = results.ToList();
                
            }

            return lstPriorities;
        }

        private static IList<string> GetRoleNames(IList<AppRole> Approles)
        {
            return (from m in Approles 
                    select m.AppRoleName).ToList<string>();
             
        }


        public static IList<KeyValueParameter> GetDashboardStatistics()
        {
            List<KeyValueParameter> keyValues = null;
            DataSet dsDashboardDetails;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dsDashboardDetails = dbl.ExecuteStoredProcedure_DS("REP.Get_DashboardStatus");
            }

            DataTable dtDetails;
            if (dsDashboardDetails.Tables[0] != null)
            {
                dtDetails = dsDashboardDetails.Tables[0];

                if (dtDetails != null && dtDetails.Rows != null)
                {
                    keyValues = new List<KeyValueParameter>();
                    foreach (DataRow dr in dtDetails.Rows)
                    {
                        keyValues.Add(new KeyValueParameter().ConvertRowToValue(dr));
                    }
                }
            }

            return keyValues;
        }


        public static int GetTableIdFromReferenceColumn(string ReferenceColumnName)
        {            
            DataSet dsDetails;
            int tableid = -1;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ReferenceColumn", SqlDbType.NVarChar, ReferenceColumnName);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dsDetails = dbl.ExecuteStoredProcedure_DS("REP.Get_TableIdFromReferenceColumn");
            }

            DataTable dtDetails;
            if (dsDetails != null && dsDetails.Tables.Count > 0)
            {
                dtDetails = dsDetails.Tables[0];

                if (dtDetails != null && dtDetails.Rows != null)
                {

                    if(! int.TryParse(dtDetails.Rows[0]["TABLEID"].ToString(), out tableid))
                    {
                        tableid = -1;
                    } 
                }
            }

            return tableid;
        }

        public static IList<Template> GetSearchTableList(int ProgramBrandId, out string dbOutputString)
        {
            dbOutputString = "Success";
            IList<Template> templates = null;

            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, ProgramBrandId);
                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_SearchTableList");
                if (spUserMessage.Value != null
                  && spUserMessage.Value != DBNull.Value)
                {
                    dbOutputString = spUserMessage.Value.ToString();
                }
            }

            if (dtResult != null)
            {
                templates = (from DataRow dr in dtResult.Rows select Template.CreateTemplate(dr)).ToList();
            }

            return templates;
        }

    }
}
