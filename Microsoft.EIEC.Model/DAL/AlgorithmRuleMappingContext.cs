using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    public class AlgorithmRuleMappingContext
    {

        public static IList<AlgorithmRuleMapping> GetCalculationRuleForAlgorithm(int scenarioId, string algorithmId)
        {
            var calculationRules = new List<AlgorithmRuleMapping>();

            try
            {
                DataTable dtResult;
                using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
                {
                    dbl.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                    dbl.AddParam("@AlgorithmId", SqlDbType.VarChar, algorithmId);
                    dtResult = dbl.ExecuteStoredProcedure("REP.Get_CalculationRulesForAlgorithm");

                }

                calculationRules.AddRange(from DataRow dr in dtResult.Rows select CreateAlgorithmRuleMapping(dr));
            }
            catch (Exception)
            {
                throw;
            }
            return calculationRules;
        }

        private static AlgorithmRuleMapping CreateAlgorithmRuleMapping(DataRow dr)
        {
            var a = new AlgorithmRuleMapping();
            a.CalculationRule = new CalculationRule
                                    {
                                        CalculationRuleId =
                                            a.CalculationRuleId =
                                            dr["CalculationRuleId"] == DBNull.Value
                                                ? 0
                                                : Convert.ToInt16(dr["CalculationRuleId"]),
                                        CalculationRuleTypeId = Convert.ToInt16(dr["CalculationRuleTypeId"].ToString()),
                                        CalculationRuleCode = dr["CalculationRuleCode"].ToString(),
                                        CalculationRuleName = dr["CalculationRuleName"].ToString(),
                                        CalculationRuleDescription = Convert.ToString(dr["CalculationRuleDescription"]),
                                        GeneratedQuery = Convert.ToString(dr["GeneratedQuery"]),
                                        Formula = Convert.ToString(dr["Formula"]),
                                        CalculationRuleQueryId = Convert.ToInt16(dr["CalculationRuleQueryId"]),
                                        IsActive = Convert.ToBoolean(dr["IsActive"]),
                                        DefaultVariableValue = Convert.ToString(dr["DefaultVariableValue"]),
                                        IsTrackError = Convert.ToBoolean(dr["TrackError"]),
                                        ErrorValue =
                                            dr["ErrorValue"] == DBNull.Value ? 0 : Convert.ToInt16(dr["ErrorValue"]),
                                        ErrorStatusCode = Convert.ToString(dr["ErrorStatusCode"])
                                    };
            a.AlgorithmRuleId = dr["AlgorithmRuleId"] == DBNull.Value ? 0 : Convert.ToInt16(dr["AlgorithmRuleId"]);
            a.Sequence = dr["sequence"] == DBNull.Value ? 0 : Convert.ToInt16(dr["sequence"]);
            a.ReportedAs = Convert.ToString(dr["ReportedAs"]);
            a.ReportId = dr["ReportId"] == DBNull.Value ? 0 : Convert.ToInt16(dr["ReportId"]);
            a.AlgorithmId = dr["AlgorithmId"] == DBNull.Value ? 0 : Convert.ToInt16(dr["AlgorithmId"]);
            a.IsActive = Convert.ToBoolean(dr["IsActive"]);
            return a;
        }

        public static string SaveAlgorithmRules(int scenarioId, IList<AlgorithmRuleMapping> changedList)
        {
            using (var sh = new SaveHelper("ModelSqlConnectionString"))
            {
                sh.Connection.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                return sh.Save(changedList, "REP.Save_Metadata", "AlgorithmRule");
            }
        }
    }
}
