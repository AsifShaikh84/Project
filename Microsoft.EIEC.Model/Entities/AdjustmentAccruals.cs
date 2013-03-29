using System;
namespace Microsoft.EIEC.Model.Entities
{   
    [Serializable]
    public class AdjustmentAccruals
    {
        public long IncentiveAccrualId { get; set; }
        
        public decimal IncentiveAmount { get; set; }
        
        public double AdjustmentAmount { get; set; }
        
        public double TotalAmount { get; set; }
        
        public string Currency { get; set; }
        
        public string PCN { get; set; }
        
        public string Partner { get; set; }
        
        public string Program { get; set; }
        
        public string Lever { get; set; }
        
        public string Compliant { get; set; }
        
        public string FiscalMonth { get; set; }

        public string Approved { get; set; }

        public string IncentivePaid { get; set; }

        public int RequiredApprovalCount { get; set; }

        public string ROCCode { get; set; }

        public Int16 RequestTypeId { get; set; }

        public long IncentivePayableId { get; set; }

        public string Manifest { get; set; }

        public string ManifestInfo { get; set; }
    }
}
