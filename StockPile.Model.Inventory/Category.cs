using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Model.Inventory
{
    public static class Category
    {
        static List<string> _categories = new List<string>()
        {
            "",
            "",
            "",
            ""
        };

        public static List<string> Categories {
            get
            {
                return new List<string>();
            }
        }    
    }
}
