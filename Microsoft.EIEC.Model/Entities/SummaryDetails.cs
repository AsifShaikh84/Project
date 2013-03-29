using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class SummaryDetails
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public double JointSell { get; set; }

        [DataMember]
        public double Renewal { get; set; }

        [DataMember]
        public double Others { get; set; }

        [DataMember]
        public string CurrentMonth { get; set; }

        [DataMember]
        public double Total { get; set; }


        public static SummaryDetails CreateSummaryDetails(DataRow dr)
        {
            try
            {
                var p = new SummaryDetails
                {
                    Description = dr["ColumnDesc"].ToString(),
                    JointSell = dr.Table.Columns.Contains("JointSell") && dr["JointSell"] != DBNull.Value ? Convert.ToDouble(dr["JointSell"]) : 0,
                    Renewal = dr.Table.Columns.Contains("Renewal") && dr["Renewal"] != DBNull.Value ? Convert.ToDouble(dr["Renewal"]) : 0,
                    Others = dr.Table.Columns.Contains("AllOther") && dr["AllOther"] != DBNull.Value ? Convert.ToDouble(dr["AllOther"]) : 0,
                    CurrentMonth = dr.Table.Columns.Contains("CurrentMonth") && dr["CurrentMonth"] != DBNull.Value ? dr["CurrentMonth"].ToString() : "0",
                    Total = dr.Table.Columns.Contains("Total") && dr["Total"] != DBNull.Value ? Convert.ToDouble(dr["Total"]) : 0,
                };
                return p;
            }
            catch (Exception)
            {

            }
            return null;
        }

    }
}
