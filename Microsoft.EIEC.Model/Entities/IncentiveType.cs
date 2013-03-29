using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class IncentiveType
    {
        [DataMember]
        public int IncentiveTypeId {get;set;}

        [DataMember]
        public string IncentiveTypeName {get;set;}

        [DataMember]
        public string IncentiveTypeCode {get;set;}

        [DataMember]
        public bool IsActive {get;set;}

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public string ModifiedBy {get;set;}
    }
}
