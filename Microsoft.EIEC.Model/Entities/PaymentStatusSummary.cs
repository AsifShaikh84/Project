using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
   [Serializable, DataContract]
    public class PaymentStatusSummary
    {
        [DataMember]
        public string Header { get; set; }

        [DataMember]
        public Int64 RowId { get; set; }

        [DataMember]
        public string Message { get; set; }

        //[DataMember]
        //public string Comments { get; set; }

        public PaymentStatusSummary(DataRow dr)
        {
            Header = dr["ExternalKey"].ToString();
            Message = dr["Message"].ToString();
            RowId = Convert.ToInt64(dr["RowId"]);      
        }
    }
}
