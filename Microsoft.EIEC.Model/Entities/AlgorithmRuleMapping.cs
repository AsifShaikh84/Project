using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class AlgorithmRuleMapping
    {
         [DataMember]
        public int AlgorithmId { get; set; }

         [DataMember]
        public string AlgorithmName { get; set; }

         [DataMember]
        public int CalculationRuleId { get; set; }

         [DataMember]
        public string CalculationRuleName { get; set; }

         [DataMember]
        public int ReportId { get; set; }

         [DataMember]
        public string ReportedAs { get; set; }

         [DataMember]
        public int Sequence { get; set; }

         [DataMember]
        public bool PayableFlag { get; set; }

         [DataMember]
        public bool IsActive { get; set; }

         [DataMember]
        public int AlgorithmRuleId { get; set; }

         [DataMember]
        public Algorithm Algorithm { get; set; }

         [DataMember]
        public CalculationRule CalculationRule { get; set; }
    }
}
