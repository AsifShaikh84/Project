using System; 
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public class AppUser
    {
        public int AppUserId { get; set; }
        public int AppRolePrivilegeId { get; set; }
        public string EmailAlias { get; set; }
        public string AppUserName { get; set; }
        public string Role { get; set; }
        public string ROC { get; set; }
        public bool Active { get; set; }
        public DBOperationType State { get; set; }
        public string Program { get; set; }
        public bool IsProgramActive { get; set; }
        public int DataFilterId { get; set; }

        public AppUser()
        {
        }

        public AppUser(DataRow dr)
        {
            if (dr.Table.Columns.Contains("AppUserId"))
                this.AppUserId = dr["AppUserId"] == null ? 0 : Convert.ToInt32(dr["AppUserId"]);

            if (dr.Table.Columns.Contains("AppRolePrivilegeId"))
                this.AppRolePrivilegeId = dr["AppRolePrivilegeId"] == null ? 0 : Convert.ToInt32(dr["AppRolePrivilegeId"]);

            if (dr.Table.Columns.Contains("EmailAlias"))
                this.EmailAlias = dr["EmailAlias"] == null ? string.Empty : dr["EmailAlias"].ToString();

            if (dr.Table.Columns.Contains("AppUserName"))
                this.AppUserName = dr["AppUserName"] == null ? string.Empty : dr["AppUserName"].ToString();

            if (dr.Table.Columns.Contains("AppRoleName"))
                this.Role = dr["AppRoleName"] == null ? string.Empty : dr["AppRoleName"].ToString();

            if (dr.Table.Columns.Contains("OperationsCenterCode"))
                this.ROC = dr["OperationsCenterCode"] == null ? string.Empty : dr["OperationsCenterCode"].ToString();

            if (dr.Table.Columns.Contains("IsActive"))
                this.Active = dr["IsActive"] == null ? false  :Convert.ToBoolean(dr["IsActive"]);

            if (dr.Table.Columns.Contains("IsProgramActive"))
                this.IsProgramActive = dr["IsProgramActive"] == null ? false : Convert.ToBoolean(dr["IsProgramActive"]);

            if (dr.Table.Columns.Contains("ProgramBrandName"))
                this.Program = dr["ProgramBrandName"] == null ? string.Empty : dr["ProgramBrandName"].ToString();

            if (dr.Table.Columns.Contains("DataFilterId"))
                this.DataFilterId = dr["DataFilterId"] == null ? 0 : Convert.ToInt32(dr["DataFilterId"]);
            
        }
    }
}
