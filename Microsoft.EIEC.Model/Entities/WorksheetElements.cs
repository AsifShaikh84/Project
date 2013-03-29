using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    public enum RangeType
    {
        Table,
        Pivot,
        Chart
    }

    public enum TemplateRefreshType
    {
        Search,
        Get
    }

    [Serializable]
    [DataContract]
   public class WorksheetElements
    {
        [DataMember]
        public string TemplateName { get; set; }
       [DataMember]
        public string SheetName { get; set; }
       [DataMember]
        public string GetProcedure { get; set; }
       [DataMember]
        public string RangeTable { get; set; }
       [DataMember]
        public RangeType RangeType { get; set; }
       [DataMember]
       public bool ToUseExternalReference { get; set; }
       [DataMember]
       public int DatasetId { get; set; }
        [DataMember]
       public IList<InputParameters> Parameters { get; set; }

        public WorksheetElements(string _templateName, string _sheetName, string _getProcedure, string _rangeTable,
                                 string rangeType, int dataSetId, bool toUseExternalReference = true)
        {
            this.TemplateName = _templateName;
            this.SheetName = _sheetName;
            this.GetProcedure = _getProcedure;
            this.RangeTable = _rangeTable;
            this.RangeType = ConvertToRangeType(rangeType);
            this.ToUseExternalReference = ToUseExternalReference;
            this.DatasetId = dataSetId;
        }

        public WorksheetElements() { }

        public static WorksheetElements ConvertFromDatabase(DataRow dr)
        {
            WorksheetElements param = new WorksheetElements();
            if (dr != null)
            {
                param.TemplateName = dr["TemplateName"].ToString();
                param.SheetName = dr["SheetName"].ToString();
                param.GetProcedure = dr["ProcedureName"].ToString();
                param.RangeTable = dr["DatasetName"].ToString();
                param.RangeType = ConvertToRangeType(dr["RangeType"].ToString());
                param.ToUseExternalReference = Convert.ToBoolean(dr["ToUseExternalReference"]);
                param.DatasetId = int.Parse(dr["DatasetId"].ToString());
            }
            return param;
        }

        private static RangeType ConvertToRangeType(string rangeType)
        {
            switch (rangeType.ToUpper())
            {
                case "PIVOT":
                    return RangeType.Pivot;
                case "CHART":
                    return RangeType.Chart;
                default:
                    return RangeType.Table;
            }
        }
    }

    [Serializable]
    [DataContract]
   public class InputParameters
   {
        [DataMember]
        public int ReportId { get; set; }
        [DataMember]
        public int DatasetId { get; set; }
       [DataMember]
       public string SheetName { get; set; }
       [DataMember]
       public string ParameterName { get; set; }
       [DataMember]
       public string DisplayName { get; set; }
       [DataMember]
       public string ParameterValue { get; set; }
       [DataMember]
       public string ParamterStyleCue { get; set; }
       [DataMember]
       public bool IsRequired { get; set; }
       [DataMember]
       public string ListName { get; set; }
       [DataMember]
       public string DataType { get; set; }
       [DataMember]
       public int ParameterType { get; set; }
       [DataMember]
       public string PageControlName { get; set; }

       public InputParameters(string sheetName, string parameterName, string parameterValue)
       {
           this.SheetName = sheetName;
           this.ParameterName = parameterName;
           this.ParameterValue = parameterValue;
           this.ParamterStyleCue = "EDIT";
           this.IsRequired = false;
           this.ListName = string.Empty;
           this.DataType = "STRING";
       }

       public InputParameters(int reportId, int datasetId, string sheetName, string parameterName, string displayName, string parameterValue,
                              string ParamterStyleCue, bool isRequired, string listName,
                              string dataType, int parameterType)
       {
           this.ReportId = reportId;
           this.DatasetId = datasetId;
           this.SheetName = sheetName;
           this.ParameterName = parameterName;
           this.DisplayName = displayName;
           this.ParameterValue = parameterValue;
           this.ParamterStyleCue = ParamterStyleCue;
           this.IsRequired = isRequired;
           this.ListName = listName;
           this.DataType = dataType;
           this.ParameterType = parameterType;
       }

       public InputParameters() { }
        
       public InputParameters ConvertFromDatabase(DataRow dr)
       {
           InputParameters param = new InputParameters(); 
           if (dr != null)
           {
               param.ReportId = int.Parse(dr["ReportId"].ToString());
                   param.DatasetId = int.Parse(dr["DatasetId"].ToString());
               param.SheetName = dr["SheetName"] ==  null ? "" : dr["SheetName"].ToString();
               param.ParameterName = dr["ParameterName"].ToString();
               param.DisplayName = dr["DisplayName"].ToString();
              // param.ParameterValue = dr["ParameterValue"].ToString();
               param.ParamterStyleCue = dr["UserControlType"].ToString();
               param.IsRequired = bool.Parse(dr["IsRequired"].ToString());
               param.ListName = dr["ListName"].ToString();
               param.DataType = dr["DataType"].ToString();
               //this.ParameterType = parameterType;
           }
           return param;
       }

       
   }

    [Serializable]
    [DataContract]
   public class ColumnsToPublish
   {
       [DataMember]
       public string SheetName { get; set; }
       [DataMember]
       public string ColumnName { get; set; }

       public ColumnsToPublish(string sheetName, string ColumnName)
       {
           this.SheetName = sheetName;
           this.ColumnName = ColumnName;
       }
   }

    [Serializable]
    [DataContract]
    public class ReportParameters
    {
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public string ExcelTemplateName { get; set; }
        [DataMember]
        public string ActiveSheet { get; set; }
        [DataMember]
        public int ReportId { get; set; }

        public ReportParameters()
        {
        }

        public ReportParameters(int reportId, string displayName, string excelTemplateName, string activeSheet)
        {
            this.DisplayName = displayName;
            this.ActiveSheet = activeSheet;
            this.ExcelTemplateName = excelTemplateName;
            this.ReportId = reportId;

        }

        public ReportParameters GetDataFromDatabase(DataRow dr)
        {
            ReportParameters param = new ReportParameters(); 
            param.ReportId = int.Parse(dr["ReportId"].ToString());
            param.DisplayName = dr["ReportName"].ToString();
            param.ExcelTemplateName = dr["TemplateName"].ToString();
            //param.ActiveSheet = dr["SheetName"] == null ? "" : dr["SheetName"].ToString();

            return param;
        }
    }
}
