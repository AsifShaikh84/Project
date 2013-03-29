using System;
using System.Runtime.Serialization;
using Microsoft.EIEC.Model.IEntity;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class RulesConfiguration : Rules, ICurrentRule
    {
        public RulesConfiguration()
            :base()
        {
        }

        [DataMemberAttribute]
        public override int CalculationRuleId { get; set; }

        [DataMemberAttribute]
        public override string CalculationRuleName { get; set; }

        [DataMemberAttribute]
        public string CalculationRuleCode { get; set; }

        [DataMemberAttribute]
        public int CalculationRuleTypeId { get; set; }

        [DataMemberAttribute]
        public bool IsActive { get; set; }       

        [DataMemberAttribute]
        public string CalculationRuleDescription { get; set; }

        [DataMemberAttribute]
        public int CalculationRuleQueryId { get; set; }

        [DataMemberAttribute]
        public string Formula { get; set; }

        [DataMemberAttribute]
        public string OutputVariableName { get; set; }

        [DataMemberAttribute]
        public bool TrackError { get; set; }

        [DataMemberAttribute]
        public string DefaultVariableValue { get; set; }

        [DataMemberAttribute]
        public string ErrorStatusCode { get; set; }

        [DataMember]
        public bool IsNull
        {
            get
            {
                if (string.IsNullOrEmpty(CalculationRuleName))
                    return true;

                if (string.IsNullOrEmpty(OutputVariableName))
                    return true;

                if (string.IsNullOrEmpty(Formula) && CalculationRuleQueryId == -1)
                    return true;

                if (CalculationRuleTypeId == 0)
                    return true;

                return false;
            }
            set
            {
            }
        }
        
    }
}
