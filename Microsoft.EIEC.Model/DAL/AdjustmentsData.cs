using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data.Linq;
using System.Configuration;
using Microsoft.EIEC.DataLayer;
using System.Data;
using System.Data.Linq.Mapping;
using System.Reflection;
using Microsoft.EIEC.Model.Helper;
using System.Data.SqlClient;
using System.Threading;
using Microsoft.EIEC.Model.Helper.Resource;

namespace Microsoft.EIEC.Model.DAL
{
    public class AdjustmentsData 
        : DataContext
    {
        public AdjustmentsData()
             : base(GlobalParameters.ConnectionString)
        {

        }       


        #region Public Methods       

        public static string SaveAdjustments(IList<TaskRequest> changedAdjustmentRows,int programBrandId)
        {
            return SaveHelper.SaveChanges(changedAdjustmentRows.ToList(), "REP.Save_Request", "", programBrandId);
        }

        public static AdjustmentDetails GetAdjustments(int templateId, string searchValue, int programBrandId, out string userMessage)
        {
            return GetAdjustmentDetails(templateId, searchValue,programBrandId , out userMessage);
        }

        public static IList<Priorities> GetAdjustmentPriorities()
        {
            return CommonDataSources.GetPriorities();
        }
        public static IList<AppUser> GetAppUsers(out string userMessage)
        {
            return GetAllAppUsers(out userMessage);
        }        
        #endregion

        #region Private Methods

        private static IList<AppUser> GetAllAppUsers(out string userMessage)
        {
            IList<AppUser> appUsersLst;

            try
            {
                AppUserContext appContext = new AppUserContext();

                appUsersLst = appContext.GetAppUserData(null,out userMessage);
            }
            catch (Exception)
            {                
                throw;
            }

            return appUsersLst;
        }

        private static AdjustmentDetails GetAdjustmentDetails(int templateId, string searchValue, int programBrandId, out string userMessage)
        {
            AdjustmentDetails adjustmentDetails = new AdjustmentDetails();
            try
            {
                var adjustmentdataSet = GetAdjustmentDataSet(templateId, searchValue,programBrandId , out userMessage);
                if (adjustmentdataSet.Tables.Count == 0 && userMessage.ToUpperInvariant().Contains(ICEResource.Message))
                {
                }
                else if (adjustmentdataSet != null && adjustmentdataSet.Tables != null && adjustmentdataSet.Tables.Count > 0)
                {
                    if (adjustmentdataSet.Tables[0] != null)
                    {
                        if (adjustmentdataSet.Tables[0].Rows.Count > 0)
                            adjustmentDetails.AdjustmentAccruals = DataSetToCollectonHelper.ConvertTo<AdjustmentAccruals>(adjustmentdataSet.Tables[0]);
                    }

                    if (adjustmentdataSet.Tables[1] != null)
                    {
                        //if there is any valid accrual data, then bind adjustment data
                        if (adjustmentdataSet.Tables[0].Rows.Count > 0)
                            adjustmentDetails.IncentiveAdjustment = (
                                                                                      from DataRow dr in adjustmentdataSet.Tables[1].Rows
                                                                                      select new TaskRequest(dr)
                                                                            ).ToList();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return adjustmentDetails;
        }

        [FunctionAttribute(Name = "REP.Get_AdjustmentRequests")]
        [ResultType(typeof(AdjustmentAccruals))]
        [ResultType(typeof(IncentiveAdjustment))]
        private IMultipleResults GetAdjustmentDetails1(int templateId, string searchValue)
        {
            IExecuteResult result = ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), templateId, searchValue);

            return ((IMultipleResults)(result.ReturnValue));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        private static DataSet GetAdjustmentDataSet(int templateId, string searchValue,int programBrandId,out string userMessage)
        {
            DataSet adjustmentDetails;
            userMessage = string.Empty;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@TemplateId", SqlDbType.Int, templateId);
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@SearchValue", SqlDbType.VarChar, searchValue);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);

                var spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.NVarChar);
                adjustmentDetails = dbl.ExecuteStoredProcedure_DS("REP.Get_AdjustmentRequests");     
                if (spUserMessage.Value != null
                 && spUserMessage.Value != DBNull.Value)
                {
                    userMessage = spUserMessage.Value.ToString();
                }
                         
            }

            return adjustmentDetails;
        }

        
        #endregion
    }
}
