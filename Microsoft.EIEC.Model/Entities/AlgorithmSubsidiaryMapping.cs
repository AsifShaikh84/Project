using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class AlgorithmSubsidiaryMapping
    {
        [DataMember]
        public int AlgorithmId { get; set; }

        [DataMember]
        public int SubsidiaryId { get; set; }

        [DataMember]
        public string OperationsCenterName { get; set; }

        [DataMember]
        public string SubsidiaryName { get; set; }

        [DataMember]
        public string AreaName { get; set; }

        [DataMember]
        public bool IsMapped { get; set; }

        [DataMember]
        public DateTime? StartFiscalMonth { get; set; }

        [DataMember]
        public DateTime? EndFiscalMonth { get; set; }

        public AlgorithmSubsidiaryMapping()
        {
        }

        public AlgorithmSubsidiaryMapping(DataRow dr)
        {
            AlgorithmId = Convert.ToInt32(dr["AlgorithmId"]);
            SubsidiaryId = Convert.ToInt32(dr["SubsidiaryId"]);
            OperationsCenterName = dr["OperationsCenterName"].ToString();
            SubsidiaryName = dr["SubsidiaryName"].ToString();
            AreaName = dr["AreaName"].ToString();
            IsMapped = Convert.ToBoolean(dr["IsMapped"]);
            
            if (dr["StartFiscalMonth"] != null && dr["StartFiscalMonth"] != DBNull.Value)
                StartFiscalMonth = Convert.ToDateTime(dr["StartFiscalMonth"]);
            
            if (dr["EndFiscalMonth"] != null && dr["EndFiscalMonth"] != DBNull.Value)
                EndFiscalMonth = Convert.ToDateTime(dr["EndFiscalMonth"]);
        }
    }
}
