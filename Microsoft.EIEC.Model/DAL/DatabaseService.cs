using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using System.Configuration;
using Microsoft.EIEC.DataLayer;
using System.Data.SqlClient;
using Microsoft.EIEC.Model.Helper;
using System.Threading;

namespace Microsoft.EIEC.Model.DAL
{
    public class DatabaseService
    {
        public IList<string> GetTableNames(string dbName,string schemaName)
        {
            List<string> tableNames = null;
            DataTable dtTables;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@DBname", SqlDbType.NVarChar, dbName);
                dbl.AddParam("@SchemaName", SqlDbType.NVarChar, schemaName);
                dtTables = dbl.ExecuteStoredProcedure("REP.Get_TableNames");
            }

            if (dtTables != null)
            {
                tableNames = (from DataRow dr in dtTables.Rows select dr["Table Name"].ToString()).ToList();
            }
            return tableNames;
        }

        public static IList<TableSchema> GetTableDesignInfo(string dbName, string tableName)
        {
            List<TableSchema> extendedProperties = null;
            DataTable dtTables;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@DBName", SqlDbType.NVarChar, dbName);
                dbl.AddParam("@TableName", SqlDbType.NVarChar, tableName);
                dtTables = dbl.ExecuteStoredProcedure("REP.Get_TableDesignInfo");
            }

            if (dtTables != null)
            {
                extendedProperties = (from DataRow dr in dtTables.Rows select TableSchema.CreateExtendedProperties(dr)).ToList();
            }
            return extendedProperties;
        }

        public static IList<DBSchema> GetDataBaseSchemas()
        {
            List<DBSchema> dbSchema = null;
            DataTable dtTables;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dtTables = dbl.ExecuteStoredProcedure("REP.Get_DBSchema");
            }

            if (dtTables != null)
            {
                dbSchema = (from DataRow dr in dtTables.Rows select CreateDBSchema(dr)).ToList();
            }
            return dbSchema;
        }

        public static IList<EIECCalcJob> GetEIECCalcJobs()
        {
            List<EIECCalcJob> jobs = null;
            DataTable dtTables;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dtTables = dbl.ExecuteStoredProcedure("REP.Get_PICalcJobStatus");
            }

            if (dtTables != null)
            {
                jobs = (from DataRow dr in dtTables.Rows select CreateEIECJob(dr)).ToList();
            }

            return jobs;
        }

        public void UpdateEIECJobStatus(string jobName)
        {
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@JobName", SqlDbType.NVarChar, jobName);
                 dbl.ExecuteNonQueryStoredProcedure("Update_EIECJobStatus");
            }
        }

        public static string ExecuteEIECJob(string jobName)
        {
            string userMsg = string.Empty;

            try
            {
                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@JobName", SqlDbType.NVarChar, jobName);
                    SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                    dbl.ExecuteNonQueryStoredProcedure("ExecuteEIECJob");

                    if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                    {
                        userMsg = spUserMessage.Value.ToString();
                    }
                }
            }
            catch(SqlException)
            {
                userMsg = "Job is already running.";
            }
            return userMsg;
        }

        public static IList<SecurityPermission> GetApplicationPermissions(int roleId, out string userMessage)
        {
            List<SecurityPermission> appPermissions = new List<SecurityPermission>() ;
            userMessage = string.Empty;
            SqlParameter spUserMessage = new SqlParameter(); 
            DataTable dtTables;
            using (var dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString))
            {
                dbl.AddParam("@RoleId", SqlDbType.Int, roleId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtTables = dbl.ExecuteStoredProcedure("REP.Get_ApplicationPermissions");
            }
            if (dtTables != null && dtTables.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMessage = spUserMessage.Value.ToString();
            }
            else  if (dtTables != null)
            {
                appPermissions = (from DataRow dr in dtTables.Rows select CreateSecurityPermission(dr)).ToList();
            }
            return appPermissions;
        }


        public string SaveRoles(IList<AppRole> changedList)
        {
            return SaveHelper.SaveChanges(changedList, "REP.Save_Roles", "",null);
        }

        public string SaveAppicationPermissions(int roleId, IList<SecurityPermission> changedList)
        {
            using (var helper = new SaveHelper())
            {
                helper.Connection.AddParam("@RoleId", SqlDbType.Int, roleId);
                return helper.Save(changedList, "REP.Save_ApplicationPermissions", "");
            }
        }

        private static DBSchema CreateDBSchema(DataRow dr)
        {
            var a = new DBSchema {DBName = Convert.ToString(dr["DatabaseName"])};
            string[] schemaNames = Convert.ToString(dr["AllSchemas"]).Split(',');
            a.Schemas=schemaNames.ToList(); 
            return a;
        }

        private static EIECCalcJob CreateEIECJob(DataRow dr)
        {

            EIECCalcJob a = new EIECCalcJob
                                {
                                    JobName = Convert.ToString(dr["JobName"]),
                                    JobDescription = Convert.ToString(dr["JobDescription"]),
                                    Enabled = Convert.ToString(dr["Enabled"]),
                                    JobStatus =
                                        (EIECJobStatus)
                                        Enum.Parse(typeof (EIECJobStatus), Convert.ToString(dr["JobStatus"])),
                                    LastRunStatus =
                                        (EIECJobRunStatus)
                                        Enum.Parse(typeof (EIECJobRunStatus), Convert.ToString(dr["LastRunStatus"]))
                                };
            if (dr["LastRunDate"] != DBNull.Value)
                a.LastRunDate = Convert.ToDateTime(dr["LastRunDate"]);
            a.LastRunDuration = Convert.ToString(dr["LastRunDuration"]);
            if (dr["NextRunDate"] != DBNull.Value)
                a.NextRunDate = Convert.ToDateTime(dr["NextRunDate"]);
            return a;
        }


        private static SecurityPermission CreateSecurityPermission(DataRow dr)
        {
            SecurityPermission a = new SecurityPermission
                                       {
                                           Operation = Convert.ToString(dr["Operation"]),
                                           IsWrite = Convert.ToBoolean(dr["WriteAccess"]),
                                           IsRead = Convert.ToBoolean(dr["ReadAccess"])
                                       };
            return a;
        }

        
    }
}
