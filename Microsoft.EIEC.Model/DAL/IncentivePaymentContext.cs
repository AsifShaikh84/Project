using System;
using System.Collections.Generic;
using System.Linq;

using System.Xml.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Data.SqlClient;
using System.Threading;


namespace Microsoft.EIEC.Model.DAL
{
    public class IncentivePaymentContext
    {
        public static IList<IncentivePayments> GetIncentivePayments(int ProgramBrandId,out string userMessage)
        {
            IList<IncentivePayments> incentivePayments = null;
            DataTable dtIncentivePayments;
            userMessage = string.Empty;
            using (dtIncentivePayments = new DataTable())
            {
                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@ProgramBrandId", SqlDbType.Int, ProgramBrandId);
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                    dtIncentivePayments = dbl.ExecuteStoredProcedure("REP.Get_PaymentSummary");
                    if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                    {
                        userMessage = spUserMessage.Value.ToString();
                    }
                }

                incentivePayments = (from DataRow dr in dtIncentivePayments.Rows select IncentivePayments.CreateIncentivePayment(dr)).ToList();
            }

            return incentivePayments;
        }

        public static XElement GetIncentiveAccruals(long paymentId, out string userMessage)
        {
            DataTable dtIncentiveAccruals;
            userMessage = string.Empty;
            using (dtIncentiveAccruals = new DataTable())
            {
                try
                {
                    using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                    {
                        dbl.AddParam("@IncentivePayableId", SqlDbType.Int, paymentId);
                        SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                        dtIncentiveAccruals = dbl.ExecuteStoredProcedure("REP.Get_IncentiveAccruals");
                        if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                        {
                            userMessage = spUserMessage.Value.ToString();
                        }
                    }   
                }
                catch (SqlException sqlEx)
                {
                    dtIncentiveAccruals.Columns.Add("Error");
                    dtIncentiveAccruals.Rows.Add(sqlEx.Message);
                }
                
                return dtIncentiveAccruals.Rows == null || dtIncentiveAccruals.Rows.Count <= 0
                           ? null
                           : XmlHelper.ConverDataTableToXElemet(dataTableToConvert: dtIncentiveAccruals, tableName: string.Format("IncentiveAccruals{0}", "ARG0"));
            }
        }

        public string SaveChangeRequest(IList<IncentivePayments> changedList,int ProgramBrandId)
        {
            return SaveHelper.SaveChanges(changedList.ToList(), "REP.Save_ChangedPayments", "IncentivePayments", ProgramBrandId);
        }

        public string SaveApprovedPayments(IList<IncentivePayments> changedList, int ProgramBrandId)
        {
            return SaveHelper.SaveChanges(changedList.ToList(), "REP.Save_ApprovedPayments", "", ProgramBrandId);
        }

        public static IList<PaymentChangeDetails> GetPaymentChanges(int incentivePayableId,out string userMessage, int timeElapsed = 10, int incentiveAccrualId = -1 )
        {
            IList<PaymentChangeDetails> incentivePayments = new List<PaymentChangeDetails>();
            userMessage = string.Empty;
            DataTable dtPaymentDetails;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@TimeElapsed", SqlDbType.Int, timeElapsed);
                dbl.AddParam("@IncentivePayableId", SqlDbType.Int, incentivePayableId);
                if (incentiveAccrualId > 0)
                {
                    dbl.AddParam("@IncentiveAccrualId", SqlDbType.Int, incentiveAccrualId);
                }
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dtPaymentDetails = dbl.ExecuteStoredProcedure("REP.Get_PaymentChanges");
                if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                {
                    userMessage = spUserMessage.Value.ToString();
                }
            }
            foreach (DataRow dr in dtPaymentDetails.Rows)
            {
                var pcd = new PaymentChangeDetails(dr);
                incentivePayments.Add(pcd);
            }
            return incentivePayments;
        }

        public static IList<CalculationChangeDetails> GetPaymentChangeDetails(int incentiveAccrualId,out string userMessage,int timeElapsed = 10)
        {
            IList<CalculationChangeDetails> incentivePaymentsCalc = new List<CalculationChangeDetails>();
            userMessage = string.Empty;
            DataTable dtCalculationDetails;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@TimeElapsed", SqlDbType.Int, timeElapsed);
                dbl.AddParam("@IncentiveAccrualId", SqlDbType.Int, incentiveAccrualId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dtCalculationDetails = dbl.ExecuteStoredProcedure("REP.Get_PaymentChangesDetails");
                if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                {
                    userMessage = spUserMessage.Value.ToString();
                }
            }
            foreach (DataRow dr in dtCalculationDetails.Rows)
            {
                var calc = new CalculationChangeDetails(dr);
                incentivePaymentsCalc.Add(calc);
            }
            return incentivePaymentsCalc;
        }

        public static IList<PaymentChangeDetails> GetAccrualsForManifest(string externalKey, int TemplateId, out string userMessage)
        {
            IList<PaymentChangeDetails> accruals = new List<PaymentChangeDetails>();
            userMessage = string.Empty;
            DataTable dtPaymentDetails;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@TableId", SqlDbType.Int, TemplateId);
                dbl.AddParam("@ExternalKey", SqlDbType.VarChar, externalKey);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                //SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dtPaymentDetails = dbl.ExecuteStoredProcedure("REP.Get_AccrualsForManifest");
            }

            foreach (DataRow dr in dtPaymentDetails.Rows)
            {
                var pcd = new PaymentChangeDetails(dr);
                accruals.Add(pcd);
            }
            return accruals;
        }


        public static IList<IncentivePayments> GetUnApprovedPayments(int programBrandId)
        {
            IList<IncentivePayments> incentivePayments = null;
            DataTable dtIncentivePayments;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@IsMonthRollOver", SqlDbType.Int, 1);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtIncentivePayments = dbl.ExecuteStoredProcedure("REP.Get_PaymentSummary");
            }

            if (dtIncentivePayments != null)
            {
                incentivePayments = (from DataRow dr in dtIncentivePayments.Rows select IncentivePayments.CreateIncentivePayment(dr)).ToList();
            }

            return incentivePayments;
        }

        public static void UnbindPaymentsOnMonthRollover(int programBrandId)
        {
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.ExecuteNonQueryStoredProcedure("REP.Save_UnbindPaymentsOnMonthRollover");
            }
        }

        public static PaymentStatistics GetPayoutDetails(out string outputMessage)
        {
            PaymentStatistics paymenstStats = null;
            try
            {
                DataSet dsPayoutDetails;

                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                    dsPayoutDetails = dbl.ExecuteStoredProcedure_DS("REP.Get_PayoutDetails");
                    outputMessage = spUserMessage.Value.ToString(); 
                }
                
                DataTable dtDetails;
                if (dsPayoutDetails != null && dsPayoutDetails.Tables.Count > 0)
                {
                    paymenstStats = new PaymentStatistics(); 
                    if (dsPayoutDetails.Tables[0] != null)
                    {
                        dtDetails = dsPayoutDetails.Tables[0];
                        paymenstStats.PotentialsIncentives = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
                    }
                    if (dsPayoutDetails.Tables[1] != null)
                    {
                        dtDetails = dsPayoutDetails.Tables[1];
                        paymenstStats.EarnedIncentives = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
                    }

                    if (dsPayoutDetails.Tables[2] != null)
                    {
                        dtDetails = dsPayoutDetails.Tables[2];
                        paymenstStats.PayoutSummary = (from DataRow dr in dtDetails.Rows select SummaryDetails.CreateSummaryDetails(dr)).ToList();
                    }
                }

                DataTable dsPaymentStatistics;

                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                    var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                    dsPaymentStatistics = dbl.ExecuteStoredProcedure("REP.Get_PaymentStatistics");
                    outputMessage = spUserMessage.Value.ToString();
                }

                if (dsPaymentStatistics != null && dsPaymentStatistics.Rows.Count> 0)
                {
                    paymenstStats.PaymentStatisticsDetail = (from DataRow dr in dsPaymentStatistics.Rows select PaymentStatisticsDetails.CreatePaymentStatisticsDetails(dr)).ToList();
                }

                return paymenstStats;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static PaymentStatusSummaryDetail GetPaymentStatusSummary(string externalkey, int templateId, int algorithmId, out string userMessage)
        {
            PaymentStatusSummaryDetail paymentStatusDetail = new PaymentStatusSummaryDetail();
            List<PaymentStatusSummary> paymentStatus = new List<PaymentStatusSummary>();
            userMessage = string.Empty;
            string outputMsg = null;
            DataSet dsIncentivePayments = null;
            DataTable dtIncentivePayments = null;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@Key", SqlDbType.VarChar, externalkey);
                dbl.AddParam("@TemplateId", SqlDbType.SmallInt, templateId);
                dbl.AddParam("@AlgorithmId", SqlDbType.Int, algorithmId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                SqlParameter param = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dsIncentivePayments = dbl.ExecuteStoredProcedure_DS("REP.Get_DiagnosticsSummary");
                if (param.Value != null && param.Value != DBNull.Value)
                {
                    outputMsg = param.Value.ToString();
                    userMessage = outputMsg;
                }
            }

            //Add Calculation results to list
            if (!string.IsNullOrEmpty(outputMsg))
            {
                paymentStatusDetail.OutputMessage = outputMsg;            
            }
            else
            {
                DataTable tblFiscalMonth = dsIncentivePayments.Tables[0];
                if (tblFiscalMonth.Rows.Count > 0)
                    paymentStatusDetail.FiscalMonth = Convert.ToInt32(tblFiscalMonth.Rows[0].ItemArray.GetValue(0));

                dtIncentivePayments = dsIncentivePayments.Tables[1];
                if (dtIncentivePayments != null)
                {
                    foreach (DataRow cr in dtIncentivePayments.Rows)
                    {
                        paymentStatus.Add(new PaymentStatusSummary(cr));
                    }
                }
                paymentStatusDetail.PaymentStatusSummary = paymentStatus;
                //paymentStatusDetail.OutputMessage  = 
                if (dsIncentivePayments.Tables.Count > 2)
                {
                    //Add TemplateFilter 
                    DataTable templateFilter = dsIncentivePayments.Tables[2];
                    if (templateFilter.Rows.Count > 0)
                        paymentStatusDetail.TemplateFilter = templateFilter.Rows[0].ItemArray.GetValue(0).ToString();

                    //Add TemplateFilter 
                    DataTable revenueFilter = dsIncentivePayments.Tables[3];
                    if (revenueFilter.Rows.Count > 0)
                        paymentStatusDetail.RevenuFilter = revenueFilter.Rows[0].ItemArray.GetValue(0).ToString();
                }
            }
            return paymentStatusDetail;
        }


        public static string SubmitForRecalculations(string RowIds, int TableId,int programBrandId)
        {
            string DBOperationMessage = string.Empty;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@RowIds", SqlDbType.NVarChar, RowIds);
                dbl.AddParam("@TableId", SqlDbType.Int, TableId);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dbl.ExecuteNonQueryStoredProcedure("REP.Save_SubmitForRecalculations");
                if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                {
                    DBOperationMessage = spUserMessage.Value.ToString();
                }
                return DBOperationMessage;
            }
           
        }
    }

}
