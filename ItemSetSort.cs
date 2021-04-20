using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    class ItemSetSort : IComparer<ItemSet>
    {
        public int Compare(ItemSet x, ItemSet y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            {
                
                decimal temp = (x.get_Radio(2) - y.get_Radio(2));
                if (temp > 0) return 1;
                else if (temp < 0) return -1;
            }
            return 0;
        }
    }
}
