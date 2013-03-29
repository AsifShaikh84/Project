using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;
using System.Threading;

namespace Microsoft.EIEC.Model.DAL
{
    [Serializable]
    public class MenuSchemaDataContext
    {
        private List<MenuSchema> _menuSchemaItemList;

        public IEnumerable<MenuSchema> ParentMenu
        {
            get
            {
                return _menuSchemaItemList.Where(p => p.ObjectId == p.ParentId);
            }
        }

        public IList<MenuSchema> GetMenuSchemaDetails(bool forceGet = false)
        {
            if (_menuSchemaItemList != null 
                    && forceGet == false)
                return _menuSchemaItemList;

            DataTable dtMenu;

            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@AccessingUser", SqlDbType.NVarChar, Thread.CurrentPrincipal.Identity.Name);
                dtMenu = dbl.ExecuteStoredProcedure("REP.Get_MenuTableSchema");
            }

            if (dtMenu != null)
            {
                _menuSchemaItemList = new List<MenuSchema>();

                foreach (DataRow dr in dtMenu.Rows)
                {
                    _menuSchemaItemList.Add(new MenuSchema(dr));
                }
            }

            return _menuSchemaItemList;
        }

        public IEnumerable<MenuSchema> GetChildNodes(int parentMenuId)
        {
            MenuSchema pm = (from p in _menuSchemaItemList
                             where p.ObjectId == parentMenuId
                             select p).FirstOrDefault<MenuSchema>();

            var result = from p in _menuSchemaItemList
                         where p.ParentId == pm.ObjectId && !p.ObjectName.Equals(pm.ObjectName)
                         orderby p.ParentId
                         select p;
            return result;
        }

    }
}
