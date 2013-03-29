using System;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    public class MenuSchema
    {
        public int ObjectId { get; set; }
        public int ParentId { get; set; }
        public string ObjectName { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnID { get; set; }


        public MenuSchema()
        {
        }

        public MenuSchema(DataRow dr)
        {
            ParentId = Convert.ToInt32(dr["ParentId"]);
            ObjectId = Convert.ToInt32(dr["ObjectId"]);
            ObjectName = dr["ObjectName"].ToString();
            TableName = dr["TableName"] == null ? string.Empty : dr["TableName"].ToString();
            ColumnName = dr["ColumnName"] == null ? string.Empty : dr["ColumnName"].ToString();
            ColumnID = dr["ColumnID"] == null ? string.Empty : dr["ColumnID"].ToString();

        }
    }
}
