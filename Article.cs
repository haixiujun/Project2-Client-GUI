using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    class Article
    {
        private int profit;
        private int weight;
        private decimal radio;
        public Article(int p,int w)
        {
            profit = p;
            weight = w;
            radio = profit / (decimal)weight;
        }
        public void set_Profit(int p)
        {
            profit = p;
            radio = profit / (decimal)weight;
        }
        public void set_Weight(int w)
        {
            weight = w;
            radio = profit / (decimal)weight;
        }

        public int get_Profit()
        {
            return profit;
        }
        public int get_Weight()
        {
            return weight;
        }
        public decimal get_Radio()
        {
            return radio;
        }

        public string To_String()
        {
            string ret_Str = "";
            ret_Str += profit.ToString() + "#";
            ret_Str += weight.ToString() + "#";
            ret_Str += radio.ToString()+"@";
            return ret_Str;
        }

    }
}
