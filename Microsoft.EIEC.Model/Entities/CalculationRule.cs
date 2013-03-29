using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public class CalculationRule 
    {
        public int CalculationRuleId { get; set; }
        public int CalculationRuleTypeId { get; set; }
        public string CalculationRuleCode { get; set; }
        public string CalculationRuleName { get; set; }
        public string CalculationRuleDescription { get; set; }
        public string OutputVariableName { get; set; }
        public string Formula { get; set; }
        public int CalculationRuleQueryId { get; set; }
        public string GeneratedQuery { get; set; }
        public bool IsActive { get; set; }
        public string DefaultVariableValue { get; set; }
        public bool IsTrackError { get; set; }
        public int ErrorValue { get; set; }
        public string ErrorStatusCode { get; set; }


        
        public CalculationRule()
        {
        }



        public CalculationRule(DataRow dr)
        {

            CalculationRuleId = Convert.ToInt32(dr["CalculationRuleId"]);
            CalculationRuleTypeId = Convert.ToInt32(dr["CalculationRuleId"]);
            CalculationRuleCode = dr["CalculationRuleCode"].ToString();
            CalculationRuleName = dr["CalculationRuleName"].ToString();
            CalculationRuleDescription = dr["CalculationRuleDescription"].ToString();
            OutputVariableName = dr["OutputVariableName"].ToString();
            Formula = dr["Formula"].ToString();
            CalculationRuleQueryId = Convert.ToInt32(dr["CalculationRuleQueryId"]);
            GeneratedQuery = "";
            IsActive = Convert.ToBoolean(dr["IsActive"]);
            DefaultVariableValue = dr["DefaultVariableValue"].ToString();
            IsTrackError = Convert.ToBoolean(dr["TrackError"]);
            ErrorValue = Convert.ToInt32(dr["ErrorValue"]);
            ErrorStatusCode = dr["ErrorStatusCode"].ToString();
        }

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

                return false;
            }
        }
    }
}
