using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class ChangeRequest
    {
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public string Approver { get; set; }
        [DataMember]
        public string MIMOS { get; set; }
        [DataMember]
        public string Comments { get; set; }

        public ChangeRequest()
        {
        }
    }
}
