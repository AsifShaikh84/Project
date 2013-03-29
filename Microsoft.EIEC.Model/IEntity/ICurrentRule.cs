using System;
using System.Linq;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface ICurrentRule
    {
        int CalculationRuleId { get; set; }

         string CalculationRuleName { get; set; }

         string CalculationRuleCode { get; set; }

         int CalculationRuleTypeId { get; set; }

         bool IsActive { get; set; }

         string CalculationRuleDescription { get; set; }

         int CalculationRuleQueryId { get; set; }

         string Formula { get; set; }

         string OutputVariableName { get; set; }

         bool TrackError { get; set; }

         string DefaultVariableValue { get; set; }

         string ErrorStatusCode { get; set; }
    }
}
