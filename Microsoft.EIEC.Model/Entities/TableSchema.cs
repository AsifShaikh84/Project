using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract,Serializable]
    public class TableSchema
    {
        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string ColumnDescription { get; set; }

        [DataMember]
        public string DataType { get; set; }

        [DataMember]
        public int Size { get; set; }



        public static TableSchema CreateExtendedProperties(DataRow dr)
        {
            TableSchema extendedProp = new TableSchema
                                           {
                                               ColumnName = Convert.ToString(dr["Column Name"]),
                                               DataType = Convert.ToString(dr["Data Type"]),
                                               Size = Convert.ToInt32(dr["Size"]),
                                               ColumnDescription = Convert.ToString(dr["Column Description"])
                                           };
            return extendedProp;
        }

    }
}
