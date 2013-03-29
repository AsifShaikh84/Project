using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [ DataContract]
    public class EIECCalcJob
    {
        [DataMember]
        public string JobName { get; set; }

        [DataMember]
        public string JobDescription { get; set; }

        [DataMember]
        public string Enabled { get; set; }

        [DataMember]
        public EIECJobStatus JobStatus { get; set; }

        [DataMember]
        public EIECJobRunStatus LastRunStatus { get; set; }

        [DataMember]
        public DateTime LastRunDate { get; set; }

        [DataMember]
        public string LastRunDuration { get; set; }

        [DataMember]
        public DateTime NextRunDate { get; set; }
    }
}
