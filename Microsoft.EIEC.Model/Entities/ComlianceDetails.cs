using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class ComplianceDetails
    {
        [DataMember]
        public string ComplianceId { get; set; }

        [DataMember]
        public string Compliance { get; set; }

        [DataMember]
        public string PeriodType { get; set; }


        public ComplianceDetails()
        {
        }

        public static ComplianceDetails ConvertRowToValue(DataRow dr)
        {

            ComplianceDetails param = new ComplianceDetails()
            {
                ComplianceId = dr["ComplianceId"].ToString(),
                Compliance = dr["Compliance"].ToString(),
                PeriodType = dr["PeriodType"].ToString()
            };
            return param;
        }
    }
}
