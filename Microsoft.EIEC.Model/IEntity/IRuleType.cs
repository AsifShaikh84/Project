using System;
using System.Linq;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface IRuleType
    {
        int CalculationRuleTypeId { get; set; }

        string CalculationRuleTypeName { get; set; }
    }
}
