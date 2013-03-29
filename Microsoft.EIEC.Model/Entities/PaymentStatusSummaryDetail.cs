using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
  [Serializable, DataContract]
    public class PaymentStatusSummaryDetail
    {
        [DataMember]
        public int FiscalMonth { get; set; }

        [DataMember]
        public string TemplateFilter { get; set; }

        [DataMember]
        public string RevenuFilter { get; set; }

        [DataMember]
        public string OutputMessage { get; set; }

        [DataMember]
        public IList<PaymentStatusSummary> PaymentStatusSummary { get; set; }
    }
}
