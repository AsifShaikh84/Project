using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class OverrideColumn
    {
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string FriendlyName { get; set; }

        public OverrideColumn()
        {
        }

        public OverrideColumn(DataRow dr)
        {
            TableName = dr["TableName"].ToString();
            FriendlyName = dr["FriendlyName"].ToString();
        }
    }
}
