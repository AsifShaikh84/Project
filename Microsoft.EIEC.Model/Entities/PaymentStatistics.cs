using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class PaymentStatistics
    {
        [DataMember]
        public IList<SummaryDetails> PotentialsIncentives;

        [DataMember]
        public IList<SummaryDetails> EarnedIncentives;

        [DataMember]
        public IList<SummaryDetails> PayoutSummary;

        [DataMember]
        public IList<PaymentStatisticsDetails> PaymentStatisticsDetail;


        
    }
}
