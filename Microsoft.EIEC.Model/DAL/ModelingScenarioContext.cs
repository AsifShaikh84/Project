using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using System.Data.SqlClient;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    public class ModelingScenarioContext
    {
        public IEnumerable<ConfigScenario> GetScenarioList()
        {
            List<ConfigScenario> changeApprovalList = null;
            DataTable dtResults;
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_ScenarioList");
            }

            if (dtResults != null)
            {
                changeApprovalList = (from DataRow dr in dtResults.Rows select new ConfigScenario(dr)).ToList();
            }
            return changeApprovalList;
        }

        public string CreateScenario(ConfigScenario newScenario)
        {
            var scenarioList = new List<ConfigScenario>() { newScenario };
            return SaveHelper.SaveChanges(scenarioList, "REP.Save_Scenario", "", 0,"ModelSqlconnectionString");
        }

        public ConfigScenario GetScenarioById(int scenarioId)
        {
            var scenario = new ConfigScenario();
            DataTable dtResults;
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_ScenarioList");
            }

            if (dtResults != null)
                foreach (DataRow dr in dtResults.Rows)
                {
                    scenario = new ConfigScenario(dr);
                    break;
                }

            return scenario;
        }

        public ConfigScenario GetScenarioByName(string scenarioName)
        {
            var scenario = new ConfigScenario();
            DataTable dtResults;
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                if (!string.IsNullOrEmpty(scenarioName))
                    dbl.AddParam("@ScenarioName", SqlDbType.VarChar, scenarioName);
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_ScenarioList");
            }

            if (dtResults != null)
                foreach (DataRow dr in dtResults.Rows)
                {
                    scenario = new ConfigScenario(dr);
                    break;
                }

            return scenario;
        }

        public IEnumerable<ModelScenarioAlgorithm> GetModelScenarioAlgorithm(int scenarioId)
        {
            List<ModelScenarioAlgorithm> changeApprovalList = null;
            DataTable dtResults;

            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.VarChar, scenarioId);
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_ScenarioAlgorithmMapping");
            }

            if (dtResults != null)
            {
                changeApprovalList = (from DataRow dr in dtResults.Rows select new ModelScenarioAlgorithm(dr)).ToList();
            }

            return changeApprovalList;
        }

        public string SaveScenarioAlgorithm(IList<ModelScenarioAlgorithm> changedRows)
        {
            return SaveHelper.SaveChanges(changedRows, "REP.Save_ModelScenarioAlgorithm", "", 0,"ModelSqlconnectionString");
        }

        public string RunAnalyis(int scenarioId)
        {
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.VarChar, scenarioId);
                SqlParameter sp = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dbl.ExecuteNonQueryStoredProcedure("RunAnalysis");
                return sp.Value.ToString();
            }
        }

        public IEnumerable<ModelExecutionSummary> GetModelingExecutionSummary(int scenarioId)
        {
            List<ModelExecutionSummary> resultList = null;
            DataTable dtResults;
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.VarChar, scenarioId);
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_AnalysisSummary");
            }

            if (dtResults != null)
            {
                resultList = (from DataRow dr in dtResults.Rows select new ModelExecutionSummary(dr)).ToList();
            }

            return resultList;
        }

        public string DeleteScenarioById(int scenarioId)
        {
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.VarChar, scenarioId);
                SqlParameter sp = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dbl.ExecuteNonQueryStoredProcedure("Delete_Scenario");

                return sp.Value.ToString();
            }
        }

        public string AddScenarioToQueue(int scenarioId)
        {
            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.VarChar, scenarioId);
                SqlParameter sp = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dbl.ExecuteNonQueryStoredProcedure("Add_ScenarioToQueue");

                return sp.Value.ToString();
            }
        }

    }
}
