using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class ModelScenarioAlgorithm
    {
        [DataMember]
        public int ScenarioId { get; set; }

        [DataMember]
        public int AlgorithmId { get; set; }

        [DataMember]
        public string AlgorithmName { get; set; }

        [DataMember]
        public bool IncludedInScenario { get; set; }

        public ModelScenarioAlgorithm()
        {
        }

        public ModelScenarioAlgorithm(DataRow dr)
        {
            ScenarioId = Convert.ToInt32(dr["ScenarioId"]);
            AlgorithmId = Convert.ToInt32(dr["AlgorithmId"]);
            AlgorithmName = dr["AlgorithmName"].ToString();
            IncludedInScenario = Convert.ToBoolean(dr["IncludedInScenario"]);
        }
    }
}
