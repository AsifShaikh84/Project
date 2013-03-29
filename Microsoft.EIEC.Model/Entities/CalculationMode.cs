using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class CalculationMode
    {
       [DataMember]
       public int CalculationModeId{get;set;}

        [DataMember]
        public string CalculationModeName{get;set;}

        [DataMember]
        public bool IsActive{get;set;}

        [DataMember]
        public DateTime ModifiedOn{get;set;}

        [DataMember]
        public string ModifiedBy{get;set;}

        [DataMember]
        public string  Earned { get; set; }
        
        [DataMember]
        public string Paid { get; set; }

        [DataMember]
        public string CalculationModelCode{get;set;}

        public CalculationMode()
        {
        }
    }
}
