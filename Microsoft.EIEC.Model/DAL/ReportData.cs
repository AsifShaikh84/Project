using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    public class ReportData
    {
        public static IList<Report> GetReports()
        {
            List<Report> reports = null;
            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dtResult = dbl.ExecuteCommand(@"SELECT  ReportId, 
                                                        ReportCode,
                                                        ReportName,
                                                        ReportDescription,
                                                        ModifiedOn,
                                                        ModifiedBy FROM Report(NOLOCK)");
            }

            if (dtResult != null)
            {
                reports = (from DataRow dr in dtResult.Rows select CreateReports(dr)).ToList();
            }

            return reports;
        }

        private static Report CreateReports(DataRow dr)
        {

            Report a = new Report
                           {
                               ReportId = Convert.ToString(dr["ReportId"]),
                               ReportCode = dr["ReportCode"].ToString(),
                               ReportName = dr["ReportName"].ToString(),
                               ReportDescription = dr["ReportDescription"].ToString(),
                               ModifiedOn = Convert.ToDateTime(dr["ModifiedOn"]),
                               ModifiedBy = Convert.ToString(dr["ModifiedBy"])
                           };
            return a;
        }

        public static IList<string> GetDistinctReportedAs()
        {
            List<string> reportAs = null;
            DataTable dtResult;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dtResult = dbl.ExecuteCommand("SELECT DISTINCT(ReportedAs) FROM ReportRule");
            }


            if (dtResult != null)
            {
                reportAs = (from DataRow dr in dtResult.Rows select dr["ReportedAs"].ToString()).ToList();
            }
            return reportAs;
        }

        public static IList<string> GetReportNames()
        {
            IList<Report> lstReports = GetReports();

            return lstReports != null ? lstReports.Select(report => "Report0" + report.ReportId.ToString()).ToList() : null;
        }

        public string SaveReportRule(int scenarioId, IList<ReportRule> changedList)
        {
            using (var sh = new SaveHelper("ModelSqlConnectionString"))
            {
                sh.Connection.AddParam("@ScenarioId", SqlDbType.Int, scenarioId);
                return sh.Save(changedList, "REP.Save_Metadata", "ReportRule");
            }
        }
    }
}
