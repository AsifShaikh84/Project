using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    [DataContract]
    public class CalculationChangeDetails
    {
        [DataMember]
        public int IncentivePayableId { get; set; }
        [DataMember]
        public int IncentiveAccrualId { get; set; }
        [DataMember]
        public int AlgorithmId { get; set; }
        [DataMember]
        public string CalculationRuleName { get; set; }
        [DataMember]
        public string NowFormula { get; set; }
        [DataMember]
        public string BeforeFormula { get; set; }
        [DataMember]
        public bool FormulaChangedFlag { get; set; }
        [DataMember]
        public string FieldName { get; set; }
        [DataMember]
        public string CalculationType { get; set; }
        [DataMember]
        public string Now { get; set; }
        [DataMember]
        public string OutputDataType { get; set; }
        [DataMember]
        public string Before { get; set; }
        [DataMember]
        public Decimal? Difference { get; set; }
        [DataMember]
        public bool IsRuleOverrideAllowed { get; set; }
        [DataMember]
        public int OverrideTargetTableId { get; set; }

        public CalculationChangeDetails()
        {
        }

        public CalculationChangeDetails(DataRow dr)
        {
            this.IncentivePayableId = Convert.ToInt32(dr["IncentivePayableId"]);
            this.IncentiveAccrualId = Convert.ToInt32(dr["IncentiveAccrualId"]);
            this.AlgorithmId = Convert.ToInt32(dr["AlgorithmId"]);
            this.CalculationRuleName = dr["CalculationRuleName"].ToString();
            this.NowFormula = dr["NowFormula"] == DBNull.Value ? string.Empty : dr["NowFormula"].ToString();
            this.BeforeFormula = dr["BeforeFormula"] == DBNull.Value ? string.Empty : dr["BeforeFormula"].ToString();
            this.FormulaChangedFlag = dr["FormulaChangedFlag"] == DBNull.Value ? false : Convert.ToBoolean(dr["FormulaChangedFlag"]);
            this.FieldName = dr["FieldName"].ToString();
            this.CalculationType = dr["CalculationType"].ToString();
            this.OutputDataType = dr["OutputDataType"] == DBNull.Value ? string.Empty : Convert.ToString(dr["OutputDataType"]);
            this.Now = this.OutputDataType == "DOMAIN" || this.OutputDataType == "DATETIME" ? dr["DomainValueNow"].ToString() : dr["Now"].ToString();
            this.Before = dr["Before"] == DBNull.Value ? string.Empty : (this.OutputDataType == "DOMAIN" || this.OutputDataType == "DATETIME" ? dr["DomainValueBefore"].ToString() : dr["Before"].ToString());
            if (this.OutputDataType != "DOMAIN" && this.OutputDataType != "DATETIME")
                this.Difference = dr["Difference"] != DBNull.Value ? Convert.ToDecimal(dr["Difference"]) : 0;
            if (dr.Table.Columns.Contains("isRuleOverrideAllowed"))
                this.IsRuleOverrideAllowed = dr["isRuleOverrideAllowed"] == null ? false : Convert.ToBoolean(dr["isRuleOverrideAllowed"]);
            if (dr.Table.Columns.Contains("OverrideTargetTableId"))
                this.OverrideTargetTableId = dr["OverrideTargetTableId"] == DBNull.Value ? -1 : Convert.ToInt32(dr["OverrideTargetTableId"]);
        }
    }
}
