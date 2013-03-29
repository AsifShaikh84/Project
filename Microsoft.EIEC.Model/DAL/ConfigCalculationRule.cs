using System;
using System.Linq;
using System.Data.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Collections.Generic;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    public class ConfigCalculationRule
    {
        #region Public Methods
        public IList<RulesConfiguration> GetCalculationRules(int scenarioId, int calCulationRuleId)
        {
            return GetConfigurationRules(scenarioId, calCulationRuleId);
        }

        public IList<RulesConfiguration> GetCalculationRules(int scenarioId)
        {
            return GetConfigurationRules(scenarioId, 0);
        }

        public static IList<RuleType> GetTypeOfRules(int scenarioId)
        {
            return GetRuleType(scenarioId);
        }

        public static IList<RuleQuery> GetQuerysForRules(int scenarioId)
        {
            return GetRuleQuery(scenarioId);
        }

        public static IList<Rules> GetAllRules(int scenarioId)
        {
            return GetAllConfigurationRules(scenarioId);
        }

        public string SaveRule(int scenarioId, IList<RulesConfiguration> currentRule)
        {
            return SaveOrUpdateRule(scenarioId, currentRule);
        }

        public static IList<Algorithm> GetDependentAlgorithm(int scenarioId, int calCulationRuleId)
        {
            return GetDependentAlgorithmForCurrentRule(scenarioId, calCulationRuleId);
        }

        public static IList<Status> GetAllErrorTypes()
        {
            return GetAllStatusErrorTypes();
        }

        
        #endregion

        private static IList<Status> GetAllStatusErrorTypes()
        {
            List<Status> lstErroStatus = null;

            using (var getPartnerData = new DataContext(GlobalParameters.ConnectionString))
            {
                try
                {
                    const string query = @"SET NOCOUNT ON SELECT CAST(StatusId AS INT) AS StatusId, StatusCode AS StatusCode from dbo.Status SET NOCOUNT OFF";

                    var results = getPartnerData.ExecuteQuery<Status>(query).ToList<Status>();

                    lstErroStatus = results.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return lstErroStatus;
        }

        #region Private Memebrs
        private static IList<Algorithm> GetDependentAlgorithmForCurrentRule(int scenarioId, int calCulationRuleId)
        {
            IList<Algorithm> lstAlgorithm = null;

            using (var getPartnerData = new DataContext(GlobalParameters.ModelConnectionString))
            {
                try
                {
                    var result = getPartnerData.ExecuteQuery<Algorithm>("exec dbo.Get_AlgortithmsForConfigRules @ScenarioId ={0}, @CalculationRuleId={1}",
                        scenarioId, calCulationRuleId).ToList<Algorithm>();

                    lstAlgorithm = result.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return lstAlgorithm;
        }

        /// <summary>
        /// ?????????????????????????????????????????
        /// </summary>
        /// <param name="scenarioId"> </param>
        /// <param name="currentRule"></param>
        /// <returns></returns>
        private static string SaveOrUpdateRule(int scenarioId, IList<RulesConfiguration> currentRule)
        {
            try
            {
                using (var sh = new SaveHelper("ModelSqlConnectionString"))
                {
                    sh.Connection.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                    return sh.Save(currentRule.ToList(), "REP.Save_Metadata", "CalculationRule");
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private static List<Rules> GetAllConfigurationRules(int scenarioId)
        {
            List<Rules> allRules = null;

            using (var getPartnerData = new DataContext(GlobalParameters.ModelConnectionString))
            {
                try
                {
                    var results = getPartnerData.ExecuteQuery<Rules>("exec dbo.Get_ConfigurationRules @ScenarioId ={0}", scenarioId).ToList<Rules>();

                    allRules = results.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return allRules;
        }

        private static IList<RuleQuery> GetRuleQuery(int scenarioId)
        {
            IList<RuleQuery> resultList = null;

            DataTable dtResults;

            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.VarChar, scenarioId);
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_CalculationRuleQuery");
            }

            if (dtResults != null)
            {
                resultList = (from DataRow dr in dtResults.Rows select new RuleQuery(dr)).ToList();
            }

            return resultList;
        
        }

        private static IList<RuleType> GetRuleType(int scenarioId)
        {
            IList<RuleType> coRuleType = null;

            using (var getPartnerData = new DataContext(GlobalParameters.ModelConnectionString))
            {
                try
                {
                    var results = getPartnerData.ExecuteQuery<RuleType>("exec dbo.Get_CalculationRuleType @ScenarioId ={0}", scenarioId).ToList<RuleType>();

                    coRuleType = results.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return coRuleType;
        }

        private static IList<RulesConfiguration> GetConfigurationRules(int scenarioId, int calculationRuleId = 0)
        {
            IList<RulesConfiguration> ocRulesConfiguration = null;

            using (var getPartnerData = new DataContext(GlobalParameters.ModelConnectionString))
            {
                try
                {
                    var result = getPartnerData.ExecuteQuery<RulesConfiguration>("exec dbo.Get_ConfigurationRules @ScenarioId ={0}, @CalculationRuleId={1}",
                        scenarioId, calculationRuleId).ToList<RulesConfiguration>();

                    ocRulesConfiguration = result.ToList();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return ocRulesConfiguration;
        }
        #endregion  
    
        
    }
}