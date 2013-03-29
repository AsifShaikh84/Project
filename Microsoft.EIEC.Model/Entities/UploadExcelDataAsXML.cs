using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.IEntity;
using System.Runtime.Serialization;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Xml.Linq;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class UploadExcelDataAsXML : IUploadExcelData
    {
        private string _storedProcedure = string.Empty;

        [NonSerializedAttribute]
        private XElement _excelDataInXML = null;

        private DataTable _excelDataTable = null;


        [DataMember]
        public string StoredProcedure
        {
            get { return _storedProcedure; }
        }

        [DataMember]
        public DataTable ExcelDataTable
        {
            get
            {
                return _excelDataTable;
            }
        }

        [DataMember]
        public XElement ExcelDataXML
        {
            get
            {
                return _excelDataInXML;
            }
        }

        public UploadExcelDataAsXML(string storedProcedure, XElement excelDataTable)
        {
            this._excelDataInXML = excelDataTable;
            this._storedProcedure = storedProcedure;
        }

        public bool UploadData()
        {
            bool isUploaded = false;
            try
            {
                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    if (this.ExcelDataTable != null)
                    {
                        dbl.AddParam("@TableName", SqlDbType.Structured, this._excelDataTable);
                    }

                    if (this.ExcelDataXML != null)
                    {
                        dbl.AddParam("@XML", SqlDbType.Xml, this._excelDataInXML.ToString());
                    }

                    if (dbl != null)
                    {
                        dbl.ExecuteStoredProcedure(this._storedProcedure);
                        isUploaded = true;
                    }
                }
            }
            catch (Exception)
            {
                isUploaded = false;
            }
            return isUploaded;
        }
    }
}
