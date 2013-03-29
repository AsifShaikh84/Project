using System.Runtime.Serialization;
using Microsoft.EIEC.Model.IEntity;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class Status : IStatus
    {
        [DataMember]
        public int StatusId { get; set; }

        [DataMember]
        public string StatusCode { get; set; }
    }
}
