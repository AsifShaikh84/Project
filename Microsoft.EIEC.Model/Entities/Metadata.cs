using System.Data;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class Metadata
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Id { get; set; }

        public static Metadata CreateMetadata(DataRow dr)
        {
            Metadata mt = new Metadata {Value = dr["Value"].ToString(), Id = dr["Id"].ToString()};
            return mt;
        }
    }
}