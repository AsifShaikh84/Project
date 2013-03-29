using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
     [DataContract]
    public class Currency
    {
         [DataMember]
        public decimal LocalCurrency { get; set; }

         [DataMember]
        public decimal Dollar{get;set;}
    }
}
