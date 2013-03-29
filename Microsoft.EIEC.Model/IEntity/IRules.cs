using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.EIEC.Model.IEntity
{
    public interface IRules
    {
        int CalculationRuleId { get; set; }

        string CalculationRuleName { get; set; }
    }
}
