using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    public class ItemSet
    {
        private List<Article> articles;
        public ItemSet(int[] profit_Array,int[] weight_Array)
        {
            articles = new List<Article>();
            Article temp;
            for (int i = 0; i < 3; i++)
            {
                temp = new Article(profit_Array[i], weight_Array[i]);
                articles.Add(temp);
            }
        }

        public string get_All_Profit_Str()
        {
            string result = "";
            for(int i = 0; i < 3; i++)
            {
                result += articles[i].get_Profit()+" ";
            }

            return result;
        }

        public string get_All_Weight_Str()
        {
            string result = "";
            for (int i = 0; i < 3; i++)
            {
                result += articles[i].get_Weight() + " ";
            }

            return result;
        }

        public int get_Profit(int index)
        {
            return articles[index].get_Profit();
        }

        public int get_Weight(int index)
        {
            return articles[index].get_Weight();
        }

        public decimal get_Radio(int index)
        {
            return articles[index].get_Radio();
        }

        public string To_String()
        {
            string ret_Str = "";
            for(int i = 0; i < 3; i++)
            {
                ret_Str += articles[i].To_String();
            }
            ret_Str=ret_Str.Substring(0, ret_Str.Length - 1);
            return ret_Str;
        }

    }
}
