using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class ConfigScenarioBase
    {
        [DataMember]
        public int ScenarioId { get; set; }
        [DataMember]
        public string ScenarioName { get; set; }
        [DataMember]
        public string Comments { get; set; }
    }
}
