using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Threading;
using System.Data.SqlClient;

namespace Microsoft.EIEC.Model.DAL
{

    public class ChangeApprovalContext
    {
        public DataSet GetChangeApprovalDetails(string reqCategoryId, int programBrandId, out string userMessage)
        {
            return GetDetails(reqCategoryId,programBrandId,  out userMessage);
        }

        private DataSet GetDetails(string reqCategoryId,int programBrandId,  out string userMessage)
        {
            SqlParameter spUserMessage = new SqlParameter();
            userMessage = string.Empty;
            DataSet dtResults;
            try
            {
                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@RequestCategory", SqlDbType.NVarChar, reqCategoryId);
                    dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                    dtResults = dbl.ExecuteStoredProcedure_DS("REP.Get_PendingRequest");
                  
                }
                if (dtResults != null && dtResults.Tables!=null  && dtResults.Tables.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                {
                    userMessage = spUserMessage.Value.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return dtResults;
        }
        public string SaveChangeApproval(IList<ChangeApproval> changedRows, int programBrandId)
        {
            return changedRows != null ? SaveHelper.SaveChanges(changedRows.ToList(), "REP.Save_ChangeRequestApproval", "", programBrandId) : string.Empty;
        }
    }
}
