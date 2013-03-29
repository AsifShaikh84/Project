using System;
using System.Runtime.Serialization;
using Microsoft.EIEC.Model.IEntity;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable ]
    public class Rules : IRules
    {
        public Rules()
        {
        }

        [DataMemberAttribute]
        public virtual int CalculationRuleId { get; set; }

        [DataMemberAttribute]
        public virtual string CalculationRuleName { get; set; }
    }
}
