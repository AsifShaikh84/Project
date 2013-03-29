using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Threading;
using System.Data.SqlClient;
using System;

namespace Microsoft.EIEC.Model.DAL
{
    public class TaskRequestContext
    {
        public IList<TaskRequest> GetNewRequests(string FiscalMonthId, int categoryId,int ProgramBrandId, out string userMessage)
        {
            IList<TaskRequest> taskRequests=new List<TaskRequest>();
            userMessage = string.Empty;
            DataTable dtResult;
            SqlParameter spUserMessage= new SqlParameter() ;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                string procedureName = string.Empty;
                switch (categoryId)
                {
                    case 1:
                        procedureName = "REP.Get_IssuesMissingInfo";
                        break;
                    case 2:
                        procedureName = "REP.Get_IssuesWaivers";
                        break;
                    case 3:
                        procedureName = "REP.Get_IssuesCriticalChanges";
                        break;
                    case 4:
                        procedureName = "REP.Get_IssuesAuditAlerts";
                        break;
                }

                if (procedureName.Length != 0)
                {

                    dbl.AddParam("@FiscalMonthId", SqlDbType.SmallInt, FiscalMonthId);
                    dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, ProgramBrandId);
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                    dtResult = dbl.ExecuteStoredProcedure(procedureName);
                }
                else
                {
                    dbl.AddParam("@RequestCategory", SqlDbType.SmallInt, categoryId);
                    dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, ProgramBrandId);
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                    dtResult = dbl.ExecuteStoredProcedure("REP.Get_NewRequests");
                }

            }


            if (dtResult != null && dtResult.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMessage = spUserMessage.Value.ToString();
            }
            else
            {
               taskRequests= ( from DataRow dr in dtResult.Rows
                                                   select new TaskRequest(dr)
                                                 ).ToList();
            }
            return taskRequests;
        }

        public string Save(IList<TaskRequest> changedRows,int programBrandId)
        {
            return SaveHelper.SaveChanges(changedRows.ToList(), "REP.Save_Request", "", programBrandId);
        }

        public string Update(IList<TaskRequest> changedRows, int programBrandId)
        {
            return SaveHelper.SaveChanges(changedRows.ToList(), "REP.Save_Request", "", programBrandId);
        }

    }
}
