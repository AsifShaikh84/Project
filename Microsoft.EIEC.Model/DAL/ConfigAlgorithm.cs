using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using Microsoft.EIEC.DataLayer;
using System.Data;
using Microsoft.EIEC.Model.Helper;
using System.Threading;

namespace Microsoft.EIEC.Model.DAL
{
    public class ConfigAlgorithm
    {
        #region Public Methods

        public static IList<Algorithm> GetAlgorithm(int programBrandId)
        {
            List<Algorithm> algorithms = null;

            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ProgramBrandId",SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_Algorithms");
            }

            if (dtResult != null)
            {
                algorithms = (from DataRow dr in dtResult.Rows select CreateAlgorithm(dr)).ToList();
            }

            return algorithms;
        }

        public IList<Algorithm> GetAlgorithm(int scenarioId,string a)
        {
            return GetAlgorithms(scenarioId, -1);
        }

        public IList<Algorithm> GetAlgorithm(int scenarioId, int algorithmId)
        {
            return GetAlgorithms(scenarioId, algorithmId);
        }

        public static Algorithm CreateAlgorithm(DataRow dr)
        {

            var a = new Algorithm
                              {
                                  AlgorithmId = Convert.ToString(dr["AlgorithmId"]),
                                  AlgorithmCode = dr["AlgorithmCode"].ToString(),
                                  AlgorithmName = dr["AlgorithmName"].ToString(),
                                  AlgorithmNameDate = dr["AlgorithmNameDate"].ToString(),
                                  AlgorithmDescription = dr["AlgorithmDescription"].ToString(),
                                  TemplateId = Convert.ToInt16(dr["TemplateId"]),
                                  CalculationModeId = Convert.ToInt16(dr["CalculationModeId"]),
                                  IncentiveTypeId = Convert.ToInt16(dr["IncentiveTypeId"]),
                                  IsRateInDollar = Convert.ToBoolean(dr["IsRateInDollar"]),
                                  IsActive = Convert.ToBoolean(dr["IsActive"]),
                                  IncentiveProgramId = Convert.ToInt16(dr["IncentiveProgramId"])
                                  //IsUsedForPreProcessing = Convert.ToBoolean(dr["IsUsedForPreProcessing"])
                              };

            return a;
        }

        public static string Save(int scenarioId, IList<Algorithm> changedList)
        {
            string result = string.Empty;
            using (var sh = new SaveHelper("ModelSqlConnectionString"))
            {
                sh.Connection.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                result = sh.Save(changedList, "REP.Save_Metadata", "Algorithm");
            }
            return result;
        }

        #endregion Public Methods

        #region Private Methods

        private static List<Algorithm> GetAlgorithms(int scenarioId, int algorithmId)
        {
            List<Algorithm> algorithms = null;
            
            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@AlgorithmId", SqlDbType.Int, algorithmId);
                dbl.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_Algorithms");
            }

            if (dtResult != null)
            {
                algorithms = (from DataRow dr in dtResult.Rows select CreateAlgorithm(dr)).ToList();
            }

            return algorithms;
        }

        #endregion Private Methods
    }
}
