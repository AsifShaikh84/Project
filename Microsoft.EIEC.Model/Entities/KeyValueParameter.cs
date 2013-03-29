using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class KeyValueParameter
    {
        [DataMember]
        public string KeyValue { get; set; }

        [DataMember]
        public string Value { get; set; }


        public KeyValueParameter()
        {
        }

        public KeyValueParameter ConvertRowToValue (DataRow dr)
        {
           
            KeyValueParameter param = new KeyValueParameter()
            {
                KeyValue = dr["ColumnDesc"].ToString(),
                Value = dr["ColumnVal"].ToString()
            };
            return param;
        }
    }
}
