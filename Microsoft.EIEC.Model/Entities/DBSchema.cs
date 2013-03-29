using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable,DataContract]
    public class DBSchema
    {
        [DataMember]
        public string DBName { get; set; }

        [DataMember]
        public IList<string> Schemas { get; set; }
    }
}
