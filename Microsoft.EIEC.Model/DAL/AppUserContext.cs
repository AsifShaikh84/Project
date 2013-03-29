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
    [Serializable]
    public class AppUserContext
    {
        public IList<AppUser> GetAppUserData(int? ProgramBrandId, out string userMessage,bool forceGet = false)
        {
            return GetAppUsers(true, ProgramBrandId, out userMessage); 
        }

        public IList<string> GetDistinctEmailAliases(int? programBrandId,  out string userMessage)
        {
            return  GetAppUsers(false,programBrandId,out userMessage).Select(u => u.EmailAlias).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
        }

        public IList<string> GetAllDistinctEmailAliases(out string userMessage)
        {
            return GetAppUsers(true, null, out userMessage).Select(u => u.EmailAlias).Distinct(StringComparer.CurrentCultureIgnoreCase).ToList();
        }

        public static string SaveAppUserData(IList<AppUser> userList)
        {
            return userList != null ? SaveHelper.SaveChanges(userList.ToList(), "REP.Save_UserPermissions", "",null) : string.Empty;
        }

        public static IList<AppUser> GetAppUsers(bool isForAccess, int? programBrandId, out string userMessage)
        {
            List<AppUser> users = new List<AppUser>(); 
            DataTable dtResult;
            userMessage = string.Empty;
            SqlParameter spUserMessage = new SqlParameter(); 

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@IsForAccess", SqlDbType.Bit, isForAccess);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_AppUserPermissions");
            }
            if (dtResult != null && dtResult.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMessage = spUserMessage.Value.ToString();
            }
            else if (dtResult != null)
            {
                users= (from DataRow dr in dtResult.Rows 
                                              select new AppUser(dr)).ToList();
            }
            return users;
           
            
        }

        public static IList<AppUser> GetUserIncentivePrograms(out string userMessage)
        {
            List<AppUser> users = new List<AppUser>();
            DataTable dtResult;
            userMessage = string.Empty;
            SqlParameter spUserMessage = new SqlParameter();

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dtResult = dbl.ExecuteStoredProcedure("[REP].[Get_UserIncentivePrograms]");
            }
            if (dtResult != null && dtResult.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMessage = spUserMessage.Value.ToString();
            }
            else if (dtResult != null)
            {
                users = (from DataRow dr in dtResult.Rows
                         select new AppUser(dr)).ToList();
            }
            return users;


        }

        public static string SaveUserIncentivePrograms(IList<AppUser> userList)
        {
            return userList != null ? SaveHelper.SaveChanges(userList.ToList(), "REP.Save_UserPrograms", "",0) : string.Empty;
        }
    }
}
