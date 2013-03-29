using System;
using System.Data;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    [Serializable]
    public class Partner
    {
        [DataMember]
        public string RowId { get; set; }

        [DataMember]
        public string PartnerName { get; set; }

        [DataMember]
        public string PartnerPCN { get; set; }

        [DataMember]
        public string PartnerInternalId { get; set; }

        [DataMember]
        public int PartnerProgramRuleTypeId { get; set; }

        [DataMember]
        public int ProgramBrandId { get; set; }

        [DataMember]
        public string ProgramBrandName { get; set; }

        [DataMember]
        public string StartMonth { get; set; }

        [DataMember]
        public string EndMonth { get; set; }

        [DataMember]
        public string SubsidiaryName { get; set; }

        [DataMember]
        public bool? IsAuthorized { get; set; }

        [DataMember]
        public bool? IsHighVolumePartner { get; set; }

        [DataMember]
        public string PartnerDetails { get; set; }

        [DataMember]
        public string CAAuthStartMonth { get; set; }

        [DataMember]
        public string CAAuthEndMonth { get; set; }

        [DataMember]
        public string PaymentCurrency { get; set; }

        [DataMember]
        public string IncentiveProgram { get; set; }



        public Partner()
        {
        }

        public static Partner CreatePartner(DataRow dr)
        {

            Partner p = new Partner();
            p.PartnerName = dr["PartnerName"].ToString();
            p.PartnerPCN = dr["PartnerPCN"].ToString();
            p.PartnerDetails = dr["PartnerName"].ToString() + ", " + dr["PartnerPCN"].ToString();
            if (dr.Table.Columns.Contains("RowId"))
                p.RowId = dr["RowId"] == null ? string.Empty : dr["RowId"].ToString();
            if (dr.Table.Columns.Contains("ProgramBrandName"))
                p.ProgramBrandName = dr["ProgramBrandName"] == null ? string.Empty : dr["ProgramBrandName"].ToString();
            if (dr.Table.Columns.Contains("PartnerInternalId"))
                p.PartnerInternalId = dr["PartnerInternalId"] == null ? string.Empty : dr["PartnerInternalId"].ToString();
            if (dr.Table.Columns.Contains("PartnerProgramRuleTypeId"))
                p.PartnerProgramRuleTypeId = dr["PartnerProgramRuleTypeId"] == null ? 0 : Convert.ToInt32(dr["PartnerProgramRuleTypeId"]);
            if (dr.Table.Columns.Contains("ProgramBrandId"))
                p.ProgramBrandId = dr["ProgramBrandId"] == null ? 0 : Convert.ToInt32(dr["ProgramBrandId"]);
            if (dr.Table.Columns.Contains("StartMonth"))
                p.StartMonth = dr["StartMonth"] == null ? string.Empty : dr["StartMonth"].ToString();
            if (dr.Table.Columns.Contains("EndMonth"))
                p.EndMonth = dr["EndMonth"] == null ? string.Empty : dr["EndMonth"].ToString();
            if (dr.Table.Columns.Contains("SubsidiaryName"))
                p.SubsidiaryName = dr["SubsidiaryName"] == null ? string.Empty : dr["SubsidiaryName"].ToString();
            if (dr.Table.Columns.Contains("IsAuthorized"))
                p.IsAuthorized = dr["IsAuthorized"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsAuthorized"]);
            if (dr.Table.Columns.Contains("IsHighVolumePartner"))
                p.IsHighVolumePartner = dr["IsHighVolumePartner"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsHighVolumePartner"]);
            if (dr.Table.Columns.Contains("PaymentCurrency"))
                p.PaymentCurrency = dr["PaymentCurrency"] == DBNull.Value ? null : dr["PaymentCurrency"].ToString();
            if (dr.Table.Columns.Contains("IncentiveProgram"))
                p.IncentiveProgram = dr["IncentiveProgram"] == DBNull.Value ? null : dr["IncentiveProgram"].ToString();
            return p;

           
        }     

    }
}
