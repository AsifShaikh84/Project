using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
      [DataContract, Serializable]
    public class SecurityPermission
    {
          [DataMember]
        public string Operation { get; set; }

          [DataMember]
        public bool IsRead { get; set; }

          [DataMember]
        public bool IsWrite { get; set; }

    }
}
