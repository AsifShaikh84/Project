using System;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [Serializable]
    [DataContract]
    public class QualifiedDataSource
    {
        [DataMember]
        public string DataSourceName { get; set; }

        public QualifiedDataSource()
        {
        }

        public QualifiedDataSource(string datasource)
        {
            DataSourceName = datasource;
        }
    }
}
