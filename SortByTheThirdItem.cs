using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    class SortByTheThirdItem
    {
        private ItemSet[] data_Sets_To_Be_Sorted;
        private int item_Set_Count;
        public SortByTheThirdItem(DataSet dataSets)
        {
            data_Sets_To_Be_Sorted = dataSets.get_Item_Sets().ToArray();
            item_Set_Count = data_Sets_To_Be_Sorted.Length;
        }

        public int get_Item_Count()
        {
            return item_Set_Count;
        }

        public void start_Sort()
        {
            Array.Sort(data_Sets_To_Be_Sorted, new ItemSetSort());
        }

        public string get_Str(int index)
        {
            return data_Sets_To_Be_Sorted[index].To_String();
        }

    }
}
