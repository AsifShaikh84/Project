using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Microsoft.EIEC.Model.Helper.Resource;


namespace Microsoft.EIEC.Model.Helper
{
    public class ExcelHelper
    {
        #region General Functions

        public static Worksheet GetConfigSheet(Workbook activeWorkbook)
        {
            var configSheet = ExcelHelper.GetSheetByName("Configuration", activeWorkbook);
            return configSheet;
        }

        public static Worksheet GetSheetByName(string SheetName, Microsoft.Office.Interop.Excel.Workbook ParentWorkBook)
        {
            foreach (Worksheet sht in ParentWorkBook.Worksheets)
            {
                if (sht.Name.ToLower() == SheetName.ToLower())
                {
                    return sht;
                }
            }

            return null;
        }

        public static ListObject GetListObjectByName(string listName, Worksheet workSheet)
        {
            foreach (ListObject listObject in workSheet.ListObjects)
            {
                if (listObject.Name == listName)
                {
                    return listObject;
                }
            }

            return null;
        }

        public static void ClearWorkbookConnections(Workbook wb)
        {
            foreach (WorkbookConnection conn in wb.Connections)
            {
                conn.Delete();
            }
        }

        public static void DeleteWorkbookSheets(Workbook wb, string[] exclusionList)
        {
            foreach (_Worksheet ws in wb.Worksheets)
            {
                if (!exclusionList.Contains(ws.Name))
                {
                    ws.Delete();
                }
            }
        }

        public static void ReleaseComObject(object comObject)
        {
            try
            {
                if (comObject != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(comObject);
                    comObject = null;
                }
            }
            finally
            {
                GC.Collect();
            }
        }

        #endregion

        #region ProtectRange

        /// <summary>
        /// Protects a sheet
        /// </summary>
        /// <param name="ws"></param>
        public static void ProtectSheet(Workbook activeWorkbook, Worksheet ws)
        {

            try
            {
                activeWorkbook.Protect(ICEResource.UI_PROTECTION_PWD);

                if (ws.Name.ToLower().Equals("configuration")) //Special treatment for configuration workbook
                {
                    ws.Protect(ICEResource.UI_PROTECTION_PWD, true,
                   true, true, ws.ProtectionMode,
                   false,
                   false,//ws.Protection.AllowFormattingColumns,
                   false,
                   false,
                   false,
                   false,//ws.Protection.AllowInsertingHyperlinks,
                   false,
                   true,//ws.Protection.AllowDeletingRows,
                   false,//ws.Protection.AllowSorting,
                   false,
                   false);
                }
                else
                {
                    ws.Protect(ICEResource.UI_PROTECTION_PWD, ws.ProtectDrawingObjects,
                    true, ws.ProtectScenarios, ws.ProtectionMode,
                    ws.Protection.AllowFormattingCells,
                    true,//ws.Protection.AllowFormattingColumns,
                    ws.Protection.AllowFormattingRows,
                    ws.Protection.AllowInsertingColumns,
                    ws.Protection.AllowInsertingRows,
                    true,//ws.Protection.AllowInsertingHyperlinks,
                    ws.Protection.AllowDeletingColumns,
                    true,//ws.Protection.AllowDeletingRows,
                    true,//ws.Protection.AllowSorting,
                    true,
                    true);
                }

            }
            catch
            {
            }

        }

        #endregion

        #region UnprotectRange
        /// <summary>
        /// Unprotect a range
        /// </summary>
        /// <param name="ws"></param>
        public static void UnprotectSheet(Workbook activeWorkbook, Worksheet ws)
        {

            try
            {
                if (ws.ProtectContents) ws.Unprotect(ICEResource.UI_PROTECTION_PWD);
                activeWorkbook.Unprotect(ICEResource.UI_PROTECTION_PWD);
            }
            catch
            {
            }
        }
        #endregion
    }
}
