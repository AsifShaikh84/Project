using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class ModelExecutionSummary
    {
        [DataMember]
        public string AlgorithmName { get; set; }

        [DataMember]
        public Decimal ExecutionValue_LC { get; set; }

        [DataMember]
        public Decimal ExecutionValue_CD { get; set; }

        [DataMember]
        public Decimal CurrentValue_LC { get; set; }

        [DataMember]
        public Decimal CurrentValue_CD { get; set; }

        [DataMember]
        public Decimal DifferenceValue_LC 
        {
            get 
            { 
                return ExecutionValue_LC - CurrentValue_LC; 
            }
            private set { /* NOOP */ } 
       }

        [DataMember]
        public Decimal DifferenceValue_CD { get { return  ExecutionValue_CD - CurrentValue_CD; }
            private set { /* NOOP */ } 
        }

        public ModelExecutionSummary()
        {
        }

        public ModelExecutionSummary(DataRow dr)
        {
            AlgorithmName = dr["AlgorithmName"].ToString();
            ExecutionValue_LC = Convert.ToDecimal(dr["ExecutionValue_LC"]);
            ExecutionValue_CD = Convert.ToDecimal(dr["ExecutionValue_CD"]);
            CurrentValue_LC = Convert.ToDecimal(dr["CurrentValue_LC"]);
            CurrentValue_CD = Convert.ToDecimal(dr["CurrentValue_CD"]);
        }
    }
}
