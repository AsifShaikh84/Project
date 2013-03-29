using System;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public class AppRole
    {
        public int AppRoleId { get; set; }
        public string AppRoleName { get; set; }
        public string AppRoleDesc { get; set; }
        public bool Active { get; set; }
        public DBOperationType State { get; set; }


        public AppRole()
        {
        }

        public AppRole(DataRow dr)
        {
            AppRoleId = Convert.ToInt32(dr["AppRoleId"]);
            AppRoleName = dr["AppRoleName"].ToString();
            AppRoleDesc = dr["AppRoleDesc"].ToString();
            Active = Convert.ToBoolean(dr["IsActive"]);
        }
    }
}
