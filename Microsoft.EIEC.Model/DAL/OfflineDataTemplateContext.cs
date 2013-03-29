using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    [Serializable]
    public class OfflineDataTemplateContext
    {
        public IList<OfflineDataTemplate> GetOfflineDataTemplates()
        {
            List<OfflineDataTemplate> offDataTemplateList = null;
            try
            {
                offDataTemplateList = new List<OfflineDataTemplate>();

                DataTable dtTable = null;

                using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
                {
                    dtTable = dbl.ExecuteStoredProcedure("REP.Get_OfflineDataTemplate");
                }

                if (dtTable != null)
                {
                    offDataTemplateList = (from DataRow dr in dtTable.Rows select new OfflineDataTemplate(dr)).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return offDataTemplateList;
        }
    }
}
