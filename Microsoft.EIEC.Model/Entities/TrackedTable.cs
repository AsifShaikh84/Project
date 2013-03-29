using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
      [DataContract]
    public class TrackedTable
    {
        [DataMember]
        public int TableId { get; set; }

        [DataMember]
        public string TableName { get; set; }

        [DataMember]
        public bool AllowUISearchFlag { get; set; }

        public static TrackedTable CreateTrackedTable(DataRow dr)
        {
            var a = new TrackedTable
            {
                TableId = dr["TableId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TableId"]),
                TableName = dr["TableName"] == DBNull.Value ? string.Empty : dr["TableName"].ToString(),
                AllowUISearchFlag = dr["AllowUISearchFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["AllowUISearchFlag"]),
            };

            return a;
        }
    }
}
