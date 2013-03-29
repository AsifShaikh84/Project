using System;
using System.Data;

namespace Microsoft.EIEC.Model.Entities
{
    public class MenuItem
    {
        public int ParentId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int MenuLevel { get; set; }
        public int MenuOrder { get; set; }
        public string ImageFile { get; set; }
        public string UIControlName { get; set; }
        public UIControlType ControlType { get; set; }
        public string NavigationPage { get; set; }
        public int Count { get; set; }

        public MenuItem()
        {
        }

        public MenuItem(DataRow dr)
        {
            ParentId = Convert.ToInt32(dr["ParentId"]);
            MenuId = Convert.ToInt32(dr["MenuId"]);
            MenuName = dr["MenuName"].ToString();
            MenuLevel = Convert.ToInt32(dr["MenuLevel"]);
            MenuOrder = Convert.ToInt32(dr["MenuOrder"]);
            ImageFile = dr["ImageFile"].ToString();
            UIControlName = dr["UIControlName"].ToString();
            ControlType = ConvertToControlType(dr["UIControlType"].ToString());
            NavigationPage = dr["NavigationPage"].ToString();
            Count = dr["Count"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Count"]); 
        }

        public static UIControlType ConvertToControlType(string value)
        {
            switch (value)
            {
                case "ReadOnlyView":
                    return UIControlType.ReadOnlyView;
                case "UserControl":
                    return UIControlType.UserControl;
                default:
                    return UIControlType.NA;
            }
        }

    }

    public enum UIControlType
    {
        UserControl,
        ReadOnlyView,
        NA
    }
}
