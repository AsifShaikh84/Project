using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class PeriodDetails
    {
        [DataMember]
        public int PeriodID { set; get; }

        [DataMember]
        public string PeriodName { set; get; }

        public PeriodDetails() { }

        public PeriodDetails ConvertFromDatabase(DataRow dr)
        {
            return new PeriodDetails() { PeriodID = int.Parse(dr["ID"].ToString()), PeriodName = dr["Name"].ToString() };
        }
    }
}
