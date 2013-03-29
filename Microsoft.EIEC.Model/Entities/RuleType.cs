using System.Runtime.Serialization;
using Microsoft.EIEC.Model.IEntity;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class RuleType : IRuleType
    {
        [DataMember]
        public int CalculationRuleTypeId { get; set; }

        [DataMember]
        public string CalculationRuleTypeName { get; set; }
    }
}
