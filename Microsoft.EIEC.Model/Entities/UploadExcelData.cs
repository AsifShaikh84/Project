using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.IEntity;
using System.Runtime.Serialization;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class UploadExcelData : IUploadExcelData
    {
        private string _storedProcedure = string.Empty;
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

        public UploadExcelData(string storedProcedure, DataTable excelDataTable)
        {
            this._excelDataTable = excelDataTable;
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
                        dbl.AddParam("@TableName", SqlDbType.Structured, this._excelDataTable);
                    dbl.ExecuteStoredProcedure(this._storedProcedure);

                    isUploaded = true;

                    //switch (_excelFileName)
                    //{
                    //    case "":
                    //        dbl.ExecuteStoredProcedure("Save_CustomRevenueforAgreement");
                    //        break;
                    //    default:
                    //        break;
                    //}
                }
            }
            catch (Exception)
            {

                isUploaded = false;
            }
            return isUploaded;
        }


        public System.Xml.Linq.XElement ExcelDataXML
        {
            get { return null; }
        }
    }
}
