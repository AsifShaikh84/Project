using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class PaymentStatisticsDetails
    {
        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public double PartnerCount { get; set; }

        [DataMember]
        public double PaymentCount { get; set; }

        public static PaymentStatisticsDetails CreatePaymentStatisticsDetails(DataRow dr)
        {
            try
            {
                var p = new PaymentStatisticsDetails
                {
                    Category = dr["Category"].ToString(),
                    Amount = Convert.ToDouble(dr["Amount"]),
                    PartnerCount = Convert.ToDouble(dr["PartnerCount"]),
                    PaymentCount = Convert.ToDouble(dr["PaymentCount"]),
                };
                return p;
            }
            catch (Exception )
            {

            }
            return null;
        }

    }
}


