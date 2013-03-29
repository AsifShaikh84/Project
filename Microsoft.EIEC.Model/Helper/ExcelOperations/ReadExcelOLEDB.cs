using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace Microsoft.EIEC.Model.ExcelOperations
{
    [Serializable]
    public class ReadExcelWorkBook
    {
        #region Private Member Variables

        string _excelWorkbookPath = string.Empty;
        string _connectionStr = string.Empty;

        public string ExcelWorkbookPath
        {
            get { return _excelWorkbookPath; }
        }

        #endregion

        #region Constructors

        public ReadExcelWorkBook(string excelWorkbookPath)
        {
            this._excelWorkbookPath = excelWorkbookPath;
            _connectionStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + this._excelWorkbookPath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1';";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Fill data table by Excel Data.
        /// </summary>
        /// <param name="workSheetName">Excel Worksheet name.</param>
        /// <returns>DataTable filled by excel data.</returns>
        public DataTable GetExcelDataTable(string workSheetName)
        {
            DataTable excelDataTable = null;
            try
            {
                //Make OLEDB connection and open it
                using (OleDbConnection oleDBCon = new OleDbConnection(_connectionStr))
                {
                    oleDBCon.Open();
                    //SQL query
                    string oledbQuery = string.Format("SELECT * from [{0}$]", workSheetName);
                    //Create OLEDB adapter
                    using (OleDbDataAdapter oleDtAd = new OleDbDataAdapter(oledbQuery, _connectionStr))
                    {
                        //Create a data set and fill it.
                        using (DataSet dtSet = new DataSet())
                        {
                            oleDtAd.Fill(dtSet);
                            using (excelDataTable = dtSet.Tables[0])
                            {
                                excelDataTable = ValidateTableColumnDataType(excelDataTable);
                            }
                        }
                    }
                }
                return excelDataTable;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// This method converts all datatype of columns for the Table into to String datatype. 
        /// This is because TEMP table in SQL DB have String type datatype for all columns.
        /// </summary>
        /// <param name="dtTable">DataTable: Of which column's datatype to be converted into String.</param>
        /// <returns>DataTable with columns of string datatype.</returns>
        private DataTable ValidateTableColumnDataType(DataTable dtTable)
        {
            DataTable newDtTable = null;
            try
            {
                newDtTable = dtTable.Clone();
                foreach (DataColumn dc in newDtTable.Columns)
                {
                    if (dc.DataType != typeof(DateTime))
                    {
                        dc.DataType = typeof(string);
                    }
                }

                foreach (DataRow row in dtTable.Rows)
                {
                    newDtTable.ImportRow(row);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return newDtTable;
        }

        /// <summary>
        /// Get list of worksheet in workbook.
        /// </summary>
        /// <returns>List of Worksheets.</returns>
        public IList<string> GetExcelSheetNames()
        {
            List<string> excelWorkBookList = null;
            try
            {
                //Create OLEDB Connection
                using (OleDbConnection oleDBCon = new OleDbConnection(_connectionStr))
                {
                    oleDBCon.Open();
                    using (DataTable dtTable = oleDBCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
                    {

                        if (dtTable == null)
                        {
                            return null;
                        }

                        excelWorkBookList = new List<string>();

                        //fetch each excel sheet from workbook
                        foreach (DataRow row in dtTable.Rows)
                        {
                            excelWorkBookList.Add(row["TABLE_NAME"].ToString().Replace("$", ""));
                        }
                    }
                }
                return excelWorkBookList;
            }
            catch (Exception )
            {
                return null;
            }
        }

        #endregion
    }
}