using System;
using System.Data;
using System.Runtime.Serialization;

namespace Microsoft.EIEC.Model.Entities
{
    [DataContract, Serializable]
    public class TaskRequest
    {
        [DataMember]
        public int RowId { get; set; }

        [DataMember]
        public Int16 RequestTypeId { get; set; }

        [DataMember]
        public int RequestId { get; set; }

        [DataMember]
        public string RequestName { get; set; }

        [DataMember]
        public string ExternalKey { get; set; }

        [DataMember]
        public string AgreementId { get; set; }

        [DataMember]
        public string Program { get; set; }

        [DataMember]
        public string SalesLocation { get; set; }

        [DataMember]
        public string ROC { get; set; }

        [DataMember]
        public string EarningMonth { get; set; }

        [DataMember]
        public string AssignedTo { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public string Comments { get; set; }

        [DataMember]
        public string DueDate { get; set; }

        [DataMember]
        public int TableId { get; set; }

        [DataMember]
        public string ReferenceColumnName { get; set; }

        [DataMember]
        public string ExternalServiceRequest { get; set; }

        [DataMember]
        public string RequestStatus { get; set; }

        [DataMember]
        public string Priority { get; set; }

        [DataMember]
        public string Manifest { get; set; }

        [DataMember]
        public string IncentiveRequestType { get; set; }

        [DataMember]
        public string AdjustmentPaid { get; set; }

        [DataMember]
        public string AdjustmentPayableId { get; set; }

        public TaskRequest()
        {
        }

        public TaskRequest(DataRow dr)
        {

            RowId = Convert.ToInt32(dr["RowId"]);
            RequestTypeId = Convert.ToInt16(dr["RequestTypeID"]);
            RequestId = dr["RequestId"] == System.DBNull.Value ? 0 : Convert.ToInt32(dr["RequestId"]);
            RequestName = dr["RequestNameType"].ToString();
            ExternalKey = dr["ExternalKey"].ToString();
            if (dr.Table.Columns.Contains("AgreementId"))
                AgreementId = dr["AgreementId"] == System.DBNull.Value ? "" : dr["AgreementId"].ToString();

            if (dr.Table.Columns.Contains("ProgramName"))
                Program = dr["ProgramName"] == System.DBNull.Value ? "" : dr["ProgramName"].ToString();
            
          
            SalesLocation = dr["SalesLocation"].ToString();
            ROC = dr["ROC"].ToString();
            EarningMonth = dr["EarningMonth"].ToString();
            DueDate = dr["DueDate"] == System.DBNull.Value ? null : AssignedTo = dr["DueDate"].ToString();

            AssignedTo = dr["AssignedTo"] == System.DBNull.Value ? "_Unassigned" : dr["AssignedTo"].ToString();
            Comments = dr["Comments"] == System.DBNull.Value ? "" : dr["Comments"].ToString();

            if (dr.Table.Columns.Contains("Priority"))
                Priority = dr["Priority"] == System.DBNull.Value ? "" : dr["Priority"].ToString();

            if (dr.Table.Columns.Contains("RequestStatus"))
                RequestStatus = dr["RequestStatus"] == System.DBNull.Value ? "" : dr["RequestStatus"].ToString();

            if (dr.Table.Columns.Contains("Value"))
                Value = dr["Value"] == System.DBNull.Value ? "" : dr["Value"].ToString();
            else
                Value = "";

            if (dr.Table.Columns.Contains("ExternalServiceRequest"))
                ExternalServiceRequest = dr["ExternalServiceRequest"] == System.DBNull.Value ? "" : dr["ExternalServiceRequest"].ToString();

            if (dr.Table.Columns.Contains("Manifest"))
                Manifest = dr["Manifest"] == System.DBNull.Value ? "" : dr["Manifest"].ToString();

            if (dr.Table.Columns.Contains("IncentiveRequestTypeName"))
                IncentiveRequestType = dr["IncentiveRequestTypeName"] == System.DBNull.Value ? "" : dr["IncentiveRequestTypeName"].ToString();

            if (dr.Table.Columns.Contains("AdjustmentPaid"))
                AdjustmentPaid = dr["AdjustmentPaid"] == System.DBNull.Value ? "" : dr["AdjustmentPaid"].ToString();
            if (dr.Table.Columns.Contains("AdjustmentPayableId"))
                AdjustmentPayableId = dr["AdjustmentPayableId"] == System.DBNull.Value ? "" : dr["AdjustmentPayableId"].ToString();
        }

    }
}
