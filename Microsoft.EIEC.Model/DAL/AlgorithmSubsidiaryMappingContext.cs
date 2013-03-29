using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using Microsoft.EIEC.DataLayer;
using System.Data;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    public class AlgorithmSubsidiaryMappingContext
    {
        public static IList<AlgorithmSubsidiaryMapping> GetAlgorithmSubsidiaryMapping(int scenarioId, int algorithmId)
        {
            DataTable dtResults;

            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                dbl.AddParam("@AlgorithmId", SqlDbType.Int, algorithmId);
                dtResults = dbl.ExecuteStoredProcedure("REP.Get_AlgorithmSubsidiaryMapping");
            }

            return (from DataRow dr in dtResults.Rows select new AlgorithmSubsidiaryMapping(dr)).ToList();
        }

        public static string SaveAlgorithmSubsidiaryMapping(int scenarioId, IList<AlgorithmSubsidiaryMapping> changedList)
        {
            using (var sh = new SaveHelper("ModelSqlConnectionString"))
            {
                sh.Connection.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                return sh.Save(changedList, "REP.Save_AlgorithmSubsidiaryMapping", "AlgorithmSubsidiary");
            }
        }
    }


}
