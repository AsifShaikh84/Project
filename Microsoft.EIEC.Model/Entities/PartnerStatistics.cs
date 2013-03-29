using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
     [DataContract]
    public class PartnerStatistics
    {
         [DataMember]
         public IList<SummaryDetails> PartnerDetails;
    }
}
