using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class ConfigScenario : ConfigScenarioBase
    {
        [DataMember]
        public DateTime? LastModifiedOn { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ExecutionStatus { get; set; }
        [DataMember]
        public bool IsApproved { get; set; }
        [DataMember]
        public string ApprovedBy { get; set; }
        [DataMember]
        public DateTime? ApprovedOn { get; set; }
        [DataMember]
        public bool IsPublished { get; set; }
        [DataMember]
        public DateTime? PublishedOn { get; set; }
        [DataMember]
        public string ExecutionMessage { get; set; }
        [DataMember]
        public string ScenarioFilter { get; set; }

        public ConfigScenario() : base()
        {
        }

        public ConfigScenario(DataRow dr)
        {
            ScenarioId = Convert.ToInt32(dr["ScenarioId"]);
            ScenarioName = dr["ScenarioName"].ToString();
            Comments = dr["Comments"].ToString();
            LastModifiedOn = Convert.ToDateTime(dr["LastModifiedOn"]);
            Status = dr["Status"].ToString();
            ExecutionStatus = dr["ExecutionStatus"].ToString();
            IsApproved = Convert.ToBoolean(dr["IsApproved"]);
            ApprovedBy = dr["ApprovedBy"].ToString();
            ExecutionMessage = dr["ExecutionMessage"].ToString();
            ScenarioFilter = dr["ScenarioFilter"].ToString();

            if (dr["ApprovedOn"] != DBNull.Value)
                ApprovedOn = Convert.ToDateTime(dr["ApprovedOn"]);
            IsPublished = Convert.ToBoolean(dr["IsPublished"]);
            if (dr["PublishedOn"] != DBNull.Value)
                PublishedOn = Convert.ToDateTime(dr["PublishedOn"]);
        }
    }
}
