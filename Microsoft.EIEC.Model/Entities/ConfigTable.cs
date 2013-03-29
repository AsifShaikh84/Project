using System;
using System.Data;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable, DataContract]
    public class ConfigTable
    {
        public ConfigTable()
        {
        }

        public ConfigTable(string tableName, string columnName)
        {
            _tableName = tableName;
            ColumnName = columnName;
        }

        public ConfigTable(DataRow dr)
        {
            ColumnName =dr["ColumnName"].ToString() ;
            ColumnValue = dr["ColumnValue"].ToString();
            _columnID = dr["ColumnID"].ToString();
        }

        string _tableName = string.Empty;
        string _tableSchema = string.Empty;
        string _columnID = string.Empty;

        [DataMember]
        public bool Include { get; set; }

        [DataMember]
        public bool Exclude { get; set; }

        [DataMember]
        public string ColumnID
        {
            get { return _columnID; }
            set { _columnID = value; }
        }

        [DataMember]
        public bool IsIncluded { get; set; }

        #region Properties

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string ColumnValue { get; set; }

        [DataMember]
        public string TableName
        {
            //get { return string.Format("{0}.{1}", _tableSchema, _tableName); }\
            get { return _tableName; }
            set { _tableName = value; }
        }

        [DataMember]
        public string TableSchema
        {
            get { return _tableSchema; }
            set { _tableSchema = value; }
        }

        #endregion
    }
}
