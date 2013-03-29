using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using System.Data.SqlClient;
using Microsoft.EIEC.Model.Helper;
using System.Xml.Linq;

namespace Microsoft.EIEC.Model.DAL
{
    [Serializable]
    public class CalculationRuleQueryContext
    {
        public static IList<CalculationRuleQuery> GetDataCalculationRuleQuery(int scenarioId)
        {
            IList<CalculationRuleQuery> calculationRuleQueryList = null;

            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ModelConnectionString))
            {
                dbl.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                dtResult = dbl.ExecuteStoredProcedure("REP.Get_CalculationRuleQuery");
            }

            if (dtResult != null)
            {
                calculationRuleQueryList = (from DataRow dr in dtResult.Rows select new CalculationRuleQuery(dr)).ToList();
            }

            return calculationRuleQueryList;
        }

        public static XElement GetRuleQuery(int scenarioId, CalculationRuleQuery calculationRuleQuery)
        {
            DataTable dtResult;

            using (dtResult = new DataTable())
            {
                try
                {
                    using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                    {
                        if (calculationRuleQuery != null) 
                            dtResult = dbl.ExecuteCommand(calculationRuleQuery.FormQuery());
                    }
                }
                catch (SqlException sqlEx)
                {
                    dtResult.Columns.Add("Error");
                    dtResult.Rows.Add(sqlEx.Message);
                }

                return dtResult == null || ( dtResult.Rows == null || dtResult.Rows.Count <= 0 )
                           ? null
                           : XmlHelper.ConverDataTableToXElemet(dtResult, string.Format("CalculationResult{0}", "ARG0"));
            }
        }

        public IList<string> GetFieldList(string datasourceName, bool forceGet = false)
        {
            IList<string> fieldList = null;

            if (forceGet == false)
                return null;

            if (!string.IsNullOrEmpty(datasourceName))
            {
                DataTable dtResult;

                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dbl.AddParam("@ObjectName", SqlDbType.VarChar, datasourceName);
                    dtResult = dbl.ExecuteStoredProcedure("REP.Get_FieldList");
                }

                if (dtResult != null)
                    fieldList = (from DataRow dr in dtResult.Rows select dr["FieldName"].ToString()).ToList();
            }

            return fieldList;
        }

        public string SaveCalculationRuleQueryData(int scenarioId, IList<CalculationRuleQuery> changedList)
        {
            string result = string.Empty;
            if (changedList != null)
            {
                using (var sh = new SaveHelper("ModelSqlConnectionString"))
                {
                    sh.Connection.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                    result = sh.Save(changedList.ToList(), "REP.Save_Metadata", "CalculationRuleQuery");
                }
            }

            return result;
        }
    }
}
