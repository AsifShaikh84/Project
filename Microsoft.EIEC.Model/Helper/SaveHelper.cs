using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.EIEC.DataLayer;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace Microsoft.EIEC.Model.Helper
{
    public class SaveHelper : IDisposable
    {
        public DatabaseLayer Connection { get; set; }
        public SaveHelper(string connectToDatabase = "SqlConnectionString")
        {
            Connection = new DatabaseLayer(ConfigurationManager.ConnectionStrings[connectToDatabase].ConnectionString);
        }

        public string Save<T>(IList<T> changedData, string storedProcedure, string worksheetname)
        {
            XmlDocument xmlDoc = XmlHelper.ConvertToXml(changedData, worksheetname);
            return Execute(storedProcedure, xmlDoc);
            //TODO
        }

        private string Execute(string saveProcedure, XmlDocument xmlDoc)
        {
            string userMsg = string.Empty;

            Connection.AddParam("@DataFromSheet", SqlDbType.Xml, xmlDoc.InnerXml);
            Connection.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
            SqlParameter spUserMessage = Connection.AddOutputParam("@UserMsg", SqlDbType.VarChar);
            Connection.ExecuteNonQueryStoredProcedure(saveProcedure);

            if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
            {
                userMsg = spUserMessage.Value.ToString();
            }

            return userMsg;
        }

        public static string SaveChanges<T>(IList<T> changedData, string storedProcedure, string worksheetname, int? programBrandId, string connectToDatabase = "SqlConnectionString")
        {
            XmlDocument xmlDoc = XmlHelper.ConvertToXml(changedData, worksheetname);
            return RunSqlCommand(storedProcedure, xmlDoc, connectToDatabase,programBrandId);
            //TODO
        }
        //
        private static string RunSqlCommand(string saveProcedure, XmlDocument xmlDoc, string connectToDatabase, int? programBrandId)
        {
            string userMsg = string.Empty;
            using (var dbl = new DatabaseLayer(ConfigurationManager.ConnectionStrings[connectToDatabase].ConnectionString))
            {
                dbl.AddParam("@DataFromSheet", SqlDbType.Xml, xmlDoc.InnerXml);
                if(programBrandId !=null)
                    dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, programBrandId);
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                SqlParameter spUserMessage = dbl.AddOutputParam("@UserMsg", SqlDbType.VarChar);
                dbl.ExecuteNonQueryStoredProcedure(saveProcedure);

                if (spUserMessage.Value != null && spUserMessage.Value != DBNull.Value)
                {
                    userMsg = spUserMessage.Value.ToString();
                }
            }

            return userMsg;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (Connection != null)
                {
                    Connection.Dispose();
                    Connection = null;
                }
            }
        }


        #endregion
    }
}
