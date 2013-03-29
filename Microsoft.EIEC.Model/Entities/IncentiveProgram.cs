using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class IncentiveProgram
    {
        [DataMember]
        public int IncentiveProgramId { get; set; }
        [DataMember]
        public string IncentiveProgramName { get; set; }
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string IconImagePath { get; set; }


        public IncentiveProgram()
        {
        }


        public IncentiveProgram(DataRow dr)
        {
            IncentiveProgramId = Convert.ToInt32(dr["ID"]);
            if (dr.Table.Columns.Contains("Name"))
            {
                IncentiveProgramName = dr["Name"] == DBNull.Value ? string.Empty : dr["Name"].ToString();
            }
            if (dr.Table.Columns.Contains("DisplayName"))
            {
                DisplayName = dr["DisplayName"] == DBNull.Value ? string.Empty : dr["DisplayName"].ToString();
            }
            if (dr.Table.Columns.Contains("IconImagePath"))
            {
                IconImagePath = dr["IconImagePath"] == DBNull.Value ? string.Empty : dr["IconImagePath"].ToString();
            }
        }
    }
}
