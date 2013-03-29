using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    public class IncentiveAdjustment
    {
        public int IncentiveAccrualId { get; set; }
        
        public int AdjustmentNumber { get; set; }
        
        public decimal AdjustmentAmount { get; set; }
        
        public string Comments { get; set; }
        
        public string AdjustmentPaid { get; set; }
        
        public bool DeleteFlag { get; set; }
        
        public int IsApproved { get; set; }
        
        public DateTime ApprovedOn { get; set; }
        
        public string ApprovedBy { get; set; }
        
        public DateTime ModifiedOn { get; set; }
        
        public string ModifiedBy { get; set; }
        
        public int IsSaved { get; set; }
    }

    [DataContract, Serializable]
    public class DetailIncentiveAdjustment
    {
        public DetailIncentiveAdjustment(){}

        [DataMemberAttribute]
        public int IncentiveAccrualId { get; set; }

        [DataMemberAttribute]
        public int AdjustmentNumber { get; set; }

        [DataMemberAttribute]
        public decimal AdjustmentAmount { get; set; }

        [DataMemberAttribute]
        public string Comments { get; set; }

        [DataMemberAttribute]
        public bool IsApproved { get; set; }

        [DataMemberAttribute]
        public bool DeleteFlag { get; set; }
    }
}
