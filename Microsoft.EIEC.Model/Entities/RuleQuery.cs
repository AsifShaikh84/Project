using System;
using System.Runtime.Serialization;
using Microsoft.EIEC.Model.IEntity;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class RuleQuery : IRuleQuery
    {
        [DataMember]
        public int CalculationRuleQueryId { get; set; }

        [DataMember]
        public string QueryDescription { get; set; }

        public RuleQuery(DataRow dr)
        {
            CalculationRuleQueryId = Convert.ToInt32(dr["CalculationRuleQueryId"]);
            QueryDescription = dr["QueryDescription"].ToString();
        }

    }
}
