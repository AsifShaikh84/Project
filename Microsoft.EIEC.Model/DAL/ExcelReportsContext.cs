using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Entities;
using Microsoft.EIEC.Model.Helper;
using Microsoft.EIEC.Model.Helper.Resource;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Microsoft.EIEC.Model.DAL
{
    public class ExcelReportsContext
    {
        public static void RefreshPivotTable(string QueryToExecute, string SheetName, string PivotTableName, Workbook ParentWorkBook, bool ToUseExternalReference)
        {
            Worksheet sheet = ExcelHelper.GetSheetByName(SheetName, ParentWorkBook);
            if (sheet == null)
            {
                throw new Exception(string.Format(ICEResource.MSG_SHEET_NOT_FOUND_ERROR, SheetName));
            }
            try
            {
                if (sheet.PivotTables(Type.Missing).Count == 0)
                {
                    throw new Exception(string.Format(ICEResource.MSG_PIVOT_TABLE_NOT_FOUND, SheetName));
                }
                if (sheet.PivotTables(Type.Missing).Count > 0 && sheet.PivotTables(PivotTableName) != null)
                {
                    PivotTable t = sheet.PivotTables(PivotTableName);// (1);
                    if (ToUseExternalReference == false)
                    {
                        t.RefreshTable();
                    }
                    else
                    {
                        PivotCache ch = t.PivotCache();
                        ch.Connection = GlobalParameters.OLEDBConnectionString;
                        ch.CommandText = QueryToExecute;
                        ch.CommandType = XlCmdType.xlCmdSql;
                        try
                        {
                            ch.MissingItemsLimit = XlPivotTableMissingItems.xlMissingItemsNone;
                            ch.Refresh();
                        }
                        catch (Exception ex)
                        {
                            string message = "Error occured while refreshing the data sheet for " + SheetName + Environment.NewLine;
                            message += "Error Message: " + ex.Message;
                            throw new Exception(message);
                        }
                    }
                }
            }
            finally
            {
                Marshal.ReleaseComObject(sheet);
            }
        }

        public static void RefreshTable(string QueryToExecute, string SheetName, string tableName, Workbook ParentWorkBook)
        {
            Worksheet sheet = ExcelHelper.GetSheetByName(SheetName, ParentWorkBook);
            if (sheet == null)
            {
                throw new Exception(string.Format(ICEResource.MSG_SHEET_NOT_FOUND_ERROR, SheetName));
            }
            ListObject lstObj = null;
            try
            {
                if (sheet.ListObjects.Count == 0)
                {
                    throw new Exception(string.Format(ICEResource.MSG_TABLE_NOT_FOUND_IN_SHEET, SheetName));
                }

                lstObj = ExcelHelper.GetListObjectByName(tableName, sheet);
                if (lstObj != null)
                {
                    QueryTable ch = lstObj.QueryTable;
                    OLEDBConnection cn = ParentWorkBook.Connections[ch.WorkbookConnection.Name].OLEDBConnection;
                    cn.Connection = GlobalParameters.OLEDBConnectionString;
                    cn.CommandType = XlCmdType.xlCmdSql;
                    cn.CommandText = QueryToExecute;
                    cn.BackgroundQuery = false;

                    try
                    {
                        cn.Refresh();
                    }
                    catch (Exception ex)
                    {
                        string message = "Error occured while refreshing the sheet " + SheetName + Environment.NewLine;
                        message += "Error Message: " + ex.Message;
                        throw new Exception(message);
                    }
                }

            }
            finally
            {
                if (lstObj != null)
                {
                    Marshal.ReleaseComObject(lstObj);
                }
                Marshal.ReleaseComObject(sheet);
            }
        }

        private void RefreshChart(string QueryToExecute, string SheetName, string chartTableName, Workbook ParentWorkBook)
        {
            Worksheet sheet = ExcelHelper.GetSheetByName(SheetName, ParentWorkBook);
            if (sheet == null)
            {
                throw new Exception(string.Format(ICEResource.MSG_SHEET_NOT_FOUND_ERROR, SheetName));
            }           
            try
            {
                Chart chart = null;
                if (sheet.ChartObjects(Type.Missing).Count > 0 && sheet.ChartObjects(chartTableName) != null)
                {
                    chart = sheet.ChartObjects(chartTableName).Chart;
                    
                    //Chart
                    //PivotCache ch = t.PivotCache();
                    //ch.Connection = GlobalParameters.OLEDBConnectionString;
                    //ch.CommandText = QueryToExecute;
                    //ch.CommandType = XlCmdType.xlCmdSql;


                    try
                    {
                        chart.Refresh();
                    }
                    catch (Exception ex)
                    {
                        string message = "Error occured while refreshing the data sheet for " + SheetName + Environment.NewLine;
                        message += "Error Message: " + ex.Message;
                        throw new Exception(message);
                    }
                }
            }
            finally
            {
                Marshal.ReleaseComObject(sheet);
            }
        }

        public static void GetSourceData(IList<ColumnsToPublish> columnList, ListObject lstObj, string sheetName)
        {
            using (System.Data.DataTable sourceData = new System.Data.DataTable())
            {
                sourceData.TableName = "sourceData" + sheetName;// +"_" + GlobalParameters.Name;

                IList<ColumnsToPublish> sheetColumns = GetSheetColumns(columnList, sheetName);

                if (sheetColumns.Count > 0)
                {
                    var listdata = lstObj.Range.Value2;

                    for (int i = 1; i <= lstObj.ListColumns.Count; i++)
                    {
                        sourceData.Columns.Add(lstObj.ListColumns[i].Name.ToString(), typeof(string));
                    }

                    for (int j = 2; j <= lstObj.ListRows.Count + 1; j++)
                    {
                        var newRow = sourceData.NewRow();
                        for (int k = 0; k <= lstObj.ListColumns.Count - 1; k++)
                        {

                            newRow[k] = listdata[j, k + 1];

                        }
                        sourceData.Rows.Add(newRow);
                    }
                    DataColumn[] sourceprimarykeys = new DataColumn[sheetColumns.Count];

                    for (int ctp = 0; ctp < sheetColumns.Count; ctp++)
                    {
                        sourceprimarykeys[ctp] = sourceData.Columns[sheetColumns[ctp].ColumnName];

                    }
                    sourceData.PrimaryKey = sourceprimarykeys;
                }
            }
        }


        public static IList<ColumnsToPublish> GetSheetColumns(IList<ColumnsToPublish> columnList, string sheetName)
        {
            var columns = columnList.Where(p => p.SheetName == sheetName);
            List<ColumnsToPublish> sheetColumns = new List<ColumnsToPublish>();
            foreach (ColumnsToPublish param in columns)
            {
                sheetColumns.Add(param);
            }
            return sheetColumns;
        }

        #region moved from presenter
        public IList<ColumnsToPublish> ColumnList { get; set; }

        /// <summary>
        /// temporrary method to do logging for debugging, delete it later
        /// example: LogMessageToFile("entered GenerateExcelFile");
        /// </summary>
        /// <param name="msg"></param>
        public void LogMessageToFile(string msg)
        {

            System.IO.StreamWriter sw = System.IO.File.AppendText(ConfigurationManager.AppSettings["ExcelDownloadLogLocation"].ToString() + "ExcelDownloadsLog" + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log");
            try
            {
                string logLine = System.String.Format(
                    "{0:G}: {1}.", System.DateTime.Now, msg);
                sw.WriteLine(logLine);
            }
            finally
            {
                sw.Close();
            }
        }
        public string GenerateExcelFile(int reportId, string reportName, IList<InputParameters> reportInputParameters, string sharePointTemplatePath, string saveFileLocation)
        {
            string saveFilePath = string.Empty;
            Application xlApp = new Application();
            xlApp.Visible = false;
            xlApp.UserControl = false;
            xlApp.DisplayAlerts = false;

            List<WorksheetElements> sheetsAttribute = GetWorksheetParameters(reportId);

            if (sheetsAttribute.Count > 0)
            {
                string templatePath = sharePointTemplatePath + sheetsAttribute[0].TemplateName;
                saveFilePath = saveFileLocation + reportName + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ffff") + ".xlsx";
                Workbook workbook = xlApp.Workbooks.Add(templatePath);
                try
                {
                    string queryParameter = string.Empty;
                    foreach (WorksheetElements sheetAttribute in sheetsAttribute)
                    {
                        queryParameter = GetQueryParametersForSheet(reportInputParameters, sheetAttribute.Parameters);
                        RefreshActiveModel(workbook, queryParameter, sheetAttribute);
                    }
                    SaveExcelFile(xlApp, workbook, saveFilePath);
                    LogMessageToFile("Accessing User : " + Thread.CurrentPrincipal.Identity.Name + "    File generated at : " + saveFilePath);

                }
                finally
                {
                    if (xlApp != null)
                    {
                        xlApp.Quit();
                        xlApp.Visible = true;
                        xlApp.DisplayAlerts = true;
                    }
                    ReleaseComObject(workbook);
                    ReleaseComObject(xlApp);
                }

            }
            return saveFilePath;
        }
        
        private string GetQueryParametersForSheet(IList<InputParameters> reportInputParameters, IList<InputParameters> sheetParameters)
        {
            StringBuilder returnValue = new StringBuilder();
            foreach (InputParameters sheetParameter in sheetParameters)
            {
                string paramValue = reportInputParameters.Where(u => u.ParameterName == sheetParameter.ParameterName).First().ParameterValue;
                bool isTypeString = sheetParameter.DataType == "STRING";
                if (!string.IsNullOrEmpty(paramValue))
                {
                    if (returnValue.Length > 0)
                        returnValue.Append(" , ");

                    returnValue.Append(sheetParameter.ParameterName);
                    returnValue.Append("=");
                    if (isTypeString) returnValue.Append("'");
                    returnValue.Append(paramValue);
                    if (isTypeString) returnValue.Append("'");
                }               
            }
            return returnValue.ToString();
        }

        private void ReleaseComObject(object comObject)
        {
            try
            {
                if (comObject != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(comObject);
                    comObject = null;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to dispose object.", ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        public void RefreshActiveModel(Workbook workbook, string queryParameter, WorksheetElements sheetAttribute)
        {
            if (sheetAttribute == null)
            {
                throw new Exception("Report is read-only hence changes done to it cannot be saved.");
            }

            RefreshSheet(queryParameter, workbook, sheetAttribute);
        }

        public void SaveExcelFile(Application xlApp, Workbook workbook, string saveFilePath)
        {

            object misValue = System.Reflection.Missing.Value;
            try
            {
                xlApp.DisplayAlerts = false; //Supress overwrite request 
                workbook.SaveAs(saveFilePath,
                    Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue,
                    Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            }
            catch (Exception exExcel)
            {
                LogMessageToFile("Error while generating file : " + saveFilePath + Environment.NewLine + "  Error Message : " + exExcel.Message + Environment.NewLine + "  Error details : " + exExcel.InnerException + Environment.NewLine + "  Error StackTrace : " + exExcel.StackTrace);
            }
            finally
            {
                workbook.Close(true, misValue, misValue);
                xlApp.Quit();

            }
        }

        private void RefreshSheet(string queryParameter, Workbook workbook, WorksheetElements attribute)
        {
            switch (attribute.RangeType)
            {
                case RangeType.Table:
                    RefreshTable("Exec " + attribute.GetProcedure + " " + queryParameter,
                                            attribute.SheetName,
                                            attribute.RangeTable,
                                            workbook);
                    break;
                case RangeType.Pivot:
                    RefreshPivotTable("Exec " + attribute.GetProcedure + " " + queryParameter,
                                           attribute.SheetName,
                                           attribute.RangeTable,
                                           workbook,
                                           attribute.ToUseExternalReference);
                    break;
                case RangeType.Chart:
                    RefreshChart("Exec " + attribute.GetProcedure + " " + queryParameter, attribute.SheetName, attribute.RangeTable, workbook);
                    break;
            }
        }

        public static System.Data.DataTable GetGeneralLookup(string listName,int? programBrandId)
        {
            System.Data.DataTable dtData;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@ListName", SqlDbType.VarChar, listName);
                dtData = dbl.ExecuteStoredProcedure("REP.Get_GeneralLookups");
                dtData.TableName = "Lookup";
            }

            return dtData;
        }

        public static string GetCurrentFiscalMonth(int? programBrandId)
        {
            //int currentFiscalMonth;
            System.Data.DataTable dtData = ExcelReportsContext.GetGeneralLookup("CurrentFiscalMonth",programBrandId);
            return dtData.Rows[0]["Name"].ToString();
        }

        public IList<ReportParameters> GetExcelReportList(out string userMessage, int programBrandId)
        {
            userMessage = string.Empty;
            SqlParameter spUserMessage = new SqlParameter();
            System.Data.DataTable dtData;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ProgramBrandId", SqlDbType.Int, programBrandId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dtData = dbl.ExecuteStoredProcedure("[REP].[Get_ExcelReportList]");
            }
            List<ReportParameters> lstParameters = new List<ReportParameters>();
            if (dtData != null && dtData.Rows.Count == 0 && spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMessage = spUserMessage.Value.ToString();
            }
            else
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    lstParameters.Add(new ReportParameters().GetDataFromDatabase(dr));
                }
            }

            return lstParameters;
        }

        public IList<InputParameters> GetReportParameters()
        {
            System.Data.DataTable dtData;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtData = dbl.ExecuteStoredProcedure("[REP].[Get_ReportParameters]");
            }
            List<InputParameters> lstParameters = new List<InputParameters>();
            if (dtData != null)
            {
                foreach (DataRow dr in dtData.Rows)
                {
                    InputParameters ip = new InputParameters().ConvertFromDatabase(dr);
                    if (lstParameters.Where(u => u.ParameterName == ip.ParameterName && u.ReportId == ip.ReportId).Count() == 0)
                    {
                        lstParameters.Add(ip);
                    }
                }
            }
            return lstParameters;

        }

        private List<WorksheetElements> GetWorksheetParameters(int excelReportId)
        {

            List<WorksheetElements> sheetParametersList = new List<WorksheetElements>();
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                dbl.AddParam("@ReportId", SqlDbType.Int, excelReportId);
                DataSet ds = dbl.ExecuteStoredProcedure_DS("[REP].[Get_ReportDetails]");
                System.Data.DataTable dSheetstData = ds.Tables[0];
                System.Data.DataTable dSParameterstData = ds.Tables[1];

                List<InputParameters> lstParameters = new List<InputParameters>();
            if (lstParameters != null)
            {
                foreach (DataRow dr in dSParameterstData.Rows)
                {
                    InputParameters ip = new InputParameters().ConvertFromDatabase(dr);
                    lstParameters.Add(ip);
                }
            }

                if (dSheetstData != null && dSheetstData.Rows.Count > 0)
                    foreach (DataRow row in dSheetstData.Rows)
                    {
                        WorksheetElements sheetParameters = WorksheetElements.ConvertFromDatabase(row);
                        sheetParameters.Parameters = lstParameters.Where(r => r.DatasetId == sheetParameters.DatasetId).ToList();
                        sheetParametersList.Add(sheetParameters);
                    }
            }
            return sheetParametersList;
        }
        #endregion
    }
}
