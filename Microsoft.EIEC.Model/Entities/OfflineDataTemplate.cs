using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class OfflineDataTemplate
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        [DataMember]
        public string ExcelFileName { get; set; }

        [DataMember]
        public string DataTableType { get; set; }

        [DataMember]
        public string StoredProcedure { get; set; }

        public OfflineDataTemplate(DataRow dataRow)
        {
            try
            {
                this.ID = dataRow["ID"] == DBNull.Value ? Int32.MinValue : Convert.ToInt32(dataRow["ID"]);
                this.TemplateName = dataRow["TemplateName"] == DBNull.Value ? string.Empty : dataRow["TemplateName"].ToString();
                this.ExcelFileName = dataRow["ExcelFileName"] == DBNull.Value ? string.Empty : dataRow["ExcelFileName"].ToString();
                this.DataTableType = dataRow["DataTableType"] == DBNull.Value ? string.Empty : dataRow["DataTableType"].ToString();
                this.StoredProcedure = dataRow["StoredProcedure"] == DBNull.Value ? string.Empty : dataRow["StoredProcedure"].ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
