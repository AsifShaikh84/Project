using System;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract]
    public class Template
    {
        [DataMember]
        public int TemplateId { get; set; }

        [DataMember]
        public string TemplateCode { get; set; }

        [DataMember]
        public string TemplateName { get; set; }

        [DataMember]
        public string DataSourceName { get; set; }

        [DataMember]
        public string DataSourceKey { get; set; }

        [DataMember]
        public bool IsApplicableForPayment { get; set; }

        [DataMember]
        public bool IsActive { get; set; }

        [DataMember]
        public DateTime ModifiedOn { get; set; }

        [DataMember]
        public string ModifiedBy { get; set; }

        [DataMember]
        public int TableId { get; set; }

        [DataMember]
        public string TableName { get; set; }





        public static Template CreateTemplate(DataRow dr)
        {

            var a = new Template
                              {
                                  TemplateId = dr["TemplateId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["TemplateId"]),
                                  TemplateCode = dr["TemplateCode"] == DBNull.Value ? string.Empty : dr["TemplateCode"].ToString(),
                                  TemplateName = dr["TemplateName"] == DBNull.Value ? string.Empty : dr["TemplateName"].ToString(),
                                  DataSourceName = dr["DataSourceName"] == DBNull.Value ? string.Empty : dr["DataSourceName"].ToString(),
                                  DataSourceKey = dr["DataSourceKey"] == DBNull.Value ? string.Empty : dr["DataSourceKey"].ToString(),
                                  IsApplicableForPayment = dr["IsApplicableForPayment"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsApplicableForPayment"]),
                                  IsActive = dr["IsActive"] == DBNull.Value ? false : Convert.ToBoolean(dr["IsActive"]),
                                  ModifiedOn = dr["ModifiedOn"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["ModifiedOn"]),
                                  ModifiedBy = dr["ModifiedBy"] == DBNull.Value ? string.Empty : dr["ModifiedBy"].ToString(),
                                  TableId = dr["BaseTableId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["BaseTableId"]),
                                  TableName = dr["TableName"] == DBNull.Value ? string.Empty :dr["TableName"].ToString() ,
                              };

            return a;
        }
    }
}
