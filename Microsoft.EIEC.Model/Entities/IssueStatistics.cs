using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class IssueStatistics
    {
        [DataMember]
        public IList<SummaryDetails> CurrentIssues;

        [DataMember]
        public IList<SummaryDetails> PendingRequests;

        [DataMember]
        public IList<SummaryDetails> RequestTypeSummary;

    }
}
