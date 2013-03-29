using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    [DataContract]
    public class PaymentChangeDetails
    {
        [DataMember]
        public int IncentivePayableId { get; set; }
        [DataMember]
        public int IncentiveAccrualId { get; set; }
        [DataMember]
        public int AlgorithmId { get; set; }
        [DataMember]
        public string Currency { get; set; }
        [DataMember]
        public string Program { get; set; }
        [DataMember]
        public string Lever { get; set; }
        [DataMember]
        public string Enrollment { get; set; }
        [DataMember]
        public string Manifest { get; set; }
        [DataMember]
        public string DeferralMonth { get; set; }        
        [DataMember]
        public Decimal Now { get; set; }
        [DataMember]
        public Decimal Before { get; set; }
        [DataMember]
        public Decimal Difference { get; set; }
        [DataMember]
        public IList<CalculationChangeDetails> CalculationChange  { get; set; }
        [DataMember]
        public Decimal TransmittedValue { get; set; }
        [DataMember]
        public string RowId { get; set; }
        [DataMember]
        public bool IsAdjustment { get; set; }
        [DataMember]
        public int TemplateId { get; set; }
        [DataMember]
        public string OpportunityId { get; set; }
        [DataMember]
        public string IncentiveRequestDate { get; set; }
        public PaymentChangeDetails()
        {
        }

        public PaymentChangeDetails(DataRow dr)
        {
            IncentivePayableId = Convert.ToInt32(dr["IncentivePayableId"]);
            IncentiveAccrualId = Convert.ToInt32(dr["IncentiveAccrualId"]);
            AlgorithmId = Convert.ToInt32(dr["AlgorithmId"]);
            Currency = dr["Currency"].ToString();
            Program = dr["Program"].ToString();
            Lever = dr["Lever"].ToString();
            Enrollment = dr["Enrollment"].ToString();
            Manifest = dr["Manifest"].ToString();
            if (dr.Table.Columns.Contains("DeferralMonth"))
            {
                DeferralMonth = dr["DeferralMonth"].ToString();
            } 
            Now = Convert.ToDecimal(dr["Now"]);
            Before = dr["Before"] != DBNull.Value ? Convert.ToDecimal(dr["Before"]) : 0;
            Difference = dr["Difference"]  != DBNull.Value ? Convert.ToDecimal(dr["Difference"]) : 0; 
            CalculationChange = new List<CalculationChangeDetails>();
            if (dr.Table.Columns.Contains("TransmittedValue"))
                this.TransmittedValue = dr["TransmittedValue"] != DBNull.Value ? Convert.ToDecimal(dr["TransmittedValue"]) : -1.0M;
            else
                this.TransmittedValue = -1.0M;
            RowId = dr["RowId"].ToString();
            if (dr.Table.Columns.Contains("IsAdjustment"))
                this.IsAdjustment = dr["IsAdjustment"] != DBNull.Value ? Convert.ToBoolean(dr["IsAdjustment"]) : false ;
            if (dr.Table.Columns.Contains("TemplateId"))
                this.TemplateId = dr["TemplateId"] != DBNull.Value ? Convert.ToInt16(dr["TemplateId"]) : 0;
            if (dr.Table.Columns.Contains("OpportunityId"))
                this.OpportunityId = dr["OpportunityId"] != DBNull.Value ? dr["OpportunityId"].ToString() : null;
            else
                this.OpportunityId = null;

            if (dr.Table.Columns.Contains("IncentiveRequestDate"))
                this.IncentiveRequestDate = dr["IncentiveRequestDate"] != DBNull.Value ? Convert.ToDateTime(dr["IncentiveRequestDate"]).ToString("yyyy-MM-dd") : null;
            else
                this.IncentiveRequestDate = null;
            RowId = dr["RowId"].ToString();
        }

        public string FilterQuery
        {
            get
            {
                return string.Format("IncentivePayableId = {0} AND IncentiveAccrualId = {1} AND AlgorithmId  = {2}", IncentivePayableId, IncentiveAccrualId, AlgorithmId);
            }
        }
    }
}
