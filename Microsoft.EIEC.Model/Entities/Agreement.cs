using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class Agreement : ITranspose
    {      
        [DataMember]
        public string AgreementID   { get; set; }
        [DataMember]
        public DateTime FirstActivatedDate { get; set; }
        [DataMember]
        public DateTime StartEffectiveDate { get; set; }
        [DataMember]
        public DateTime ExpectedEndEffectiveDate { get; set; }
        [DataMember]
        public int EndCustomerTPID { get; set; }
        [DataMember]
        public string EndCustomerName { get; set; }
        [DataMember]
        public string AgreementState { get; set; }
        [DataMember]
        public DateTime LastSystemRefresh { get; set; }
        [DataMember]
        public int DesktopCount { get; set; }
        [DataMember]
        public Decimal CommitmentRevenue { get; set; }
        [DataMember]
        public int CommitmentDuration { get; set; }
        [DataMember]
        public bool OnTimeRenewed { get; set; }
        [DataMember]
        public bool ReceivedAccurate { get; set; }
        [DataMember]
        public string Subsegment { get; set; }
        [DataMember]
        public int RowId { get; set; }


        public static Agreement CreateAgreement(DataRow dr)
        {
            var p = new Agreement
                              {
                                  AgreementID = dr["AgreementID"].ToString(),
                                  FirstActivatedDate = Convert.ToDateTime(dr["FirstActivatedDate"]),
                                  StartEffectiveDate = Convert.ToDateTime(dr["StartEffectiveDate"]),
                                  ExpectedEndEffectiveDate = Convert.ToDateTime(dr["ExpectedEndEffectiveDate"]),
                                  EndCustomerTPID = Convert.ToInt32(dr["EndCustomerTPID"]),
                                  EndCustomerName = Convert.ToString(dr["EndCustomerName"]),
                                  AgreementState = Convert.ToString(dr["AgreementState"]),
                                  LastSystemRefresh = Convert.ToDateTime(dr["LastSystemRefresh"]),
                                  DesktopCount = Convert.ToInt32(dr["DesktopCount"]),
                                  CommitmentDuration = Convert.ToInt32(dr["CommitmentDuration"]),
                                  CommitmentRevenue = Convert.ToDecimal(dr["CommitmentRevenue"]),
                                  OnTimeRenewed = Convert.ToBoolean(dr["OnTimeRenewed"]),
                                  ReceivedAccurate = Convert.ToBoolean(dr["ReceivedAccurate"]),
                                  Subsegment = Convert.ToString(dr["Subsegment"]),
                                  RowId = Convert.ToInt32(dr["RowId"]) 
                              };

            return p;
        }

        public IList<FieldValueTranspose> TransposeToFieldValue()
        {
            return (from info in GetType().GetProperties() where info.CanRead let o = info.GetValue(this, null) select new FieldValueTranspose(info.Name, o, true)).ToList();
        }
    }
}
