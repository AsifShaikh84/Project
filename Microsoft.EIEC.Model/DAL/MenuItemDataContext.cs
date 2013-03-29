using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EIEC.Model.Entities;
using System.Data;
using Microsoft.EIEC.DataLayer;
using Microsoft.EIEC.Model.Helper;

namespace Microsoft.EIEC.Model.DAL
{
    [Serializable]
    public class MenuItemDataContext
    {
        public IList<MenuItem> MenuItemList;
        public IList<MenuItem> MenuItemListForTiles;
        public IList<MenuItem> ParentMenu
        {
            get
            {
                return MenuItemList.Where(p => p.MenuLevel == 0).ToList();
            }
        }

        public IList<MenuItem> GetMenuDetails(bool forceGet = false)
        {
            if (MenuItemList != null && forceGet == false)
                return MenuItemList;
            
            DataTable dtMenu;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dtMenu = dbl.ExecuteStoredProcedure("REP.Get_Menu");
            }

            if (dtMenu != null)
            {
                MenuItemList = new List<MenuItem>();
                foreach (DataRow dr in dtMenu.Rows)
                {
                    MenuItemList.Add(new MenuItem(dr));
                }
            }

            return MenuItemList;
        }

        public IList<MenuItem> GetMenuDetailsforTiles(int ProgramBrandId,bool forceGet = false)
        {
            if (MenuItemListForTiles != null && forceGet == false)
                return MenuItemListForTiles;
            
            DataTable dtMenu;
            using (var dbl = new DatabaseLayer(GlobalParameters.ConnectionString))
            {
                dbl.AddParam("@ProgramBrandId", SqlDbType.SmallInt, ProgramBrandId);
                dtMenu = dbl.ExecuteStoredProcedure("REP.Get_MenuforTiles");
            }

            if (dtMenu != null)
            {
                MenuItemListForTiles = new List<MenuItem>();
                foreach (DataRow dr in dtMenu.Rows)
                {
                    MenuItemListForTiles.Add(new MenuItem(dr));
                }
            }

            return MenuItemListForTiles;
        }


        public IList<MenuItem> GetChildNodes(int parentMenuId)
        {
            MenuItem pm = (from p in MenuItemList
                           where p.MenuId == parentMenuId
                           select p).FirstOrDefault<MenuItem>();

            var result = from p in MenuItemList
                         where p.ParentId == pm.MenuId && p.MenuLevel > pm.MenuLevel
                         orderby p.ParentId, p.MenuOrder
                         select p;

            return result.ToList();
        }
    }
}
