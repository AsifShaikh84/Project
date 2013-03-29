using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class Priorities
    {
        public Priorities()
        {
        }

        [DataMemberAttribute]
        public int PriorityId { get; set; }

        [DataMemberAttribute]
        public string PriorityType { get; set; }
    }
}
