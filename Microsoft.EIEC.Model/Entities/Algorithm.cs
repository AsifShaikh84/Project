using System;
using Microsoft.EIEC.Model.IEntity;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class Algorithm : IAlgorithm
    {
        [DataMember]
        public string AlgorithmId { get; set; }

        [DataMember]
        public string AlgorithmCode { get; set; }

        [DataMember]
        public string AlgorithmName { get; set; }

        [DataMember]
        public string AlgorithmNameDate { get; set; }

        [DataMember]
        public string AlgorithmDescription { get; set; }

        [DataMember]
        public int TemplateId { get; set; }

        [DataMember]
        public int CalculationModeId { get; set; }

        [DataMember]
        public int IncentiveTypeId { get; set; }

        [DataMember]
        public bool IsRateInDollar { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public int IncentiveProgramId { get; set; }

        [DataMember]
        public bool IsUsedForPreProcessing { get; set; }

        public Algorithm()
        {
        }

        [DataMember]
        public bool IsNull
        {
            get
            {
                if (string.IsNullOrEmpty(AlgorithmName))
                    return true;

                if (TemplateId == 0)
                    return true;

                if (CalculationModeId == 0)
                    return true;

                if (IncentiveTypeId == 0)
                    return true;

                if (IncentiveProgramId == 0)
                    return true;

                return false;
            }
            set
                {}
        }
    }
}
