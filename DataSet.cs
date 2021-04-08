using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    class DataSet
    {
        private int c;
        private int item_Set_Count;
        private List<ItemSet> item_Sets;
        private int maximum_Value;
        private int[] route;
        private int[,] dynamic_Array;
        private int[] temp_Route;
        private double process_Time;

        public int get_Result()
        {
            return maximum_Value;
        }

       public int get_Item_Sets_Count()
        {
            return item_Set_Count;
        }

        public int get_Cubage()
        {
            return c;
        }

        public List<ItemSet> get_Item_Sets()
        {
            return item_Sets;
        }

        public int[] get_Route()
        {
            return route;
        }

        public double get_Process_Time()
        {
            return process_Time;
        }

        public DataSet(int dimension, int cubage)
        {
            item_Set_Count = dimension;
            c = cubage;
            item_Sets = new List<ItemSet>();
        }

        public void init_Item_Sets(int[] profit_Array,int[] weight_Array)
        {
            int[] init_Profit;
            int[] init_Weight;
            ItemSet temp;
            for (int i = 0; i < item_Set_Count; i++)
            {
                
                init_Profit = new int[] {profit_Array[i*3], profit_Array[i * 3+1] , profit_Array[i * 3+2] };
                init_Weight = new int[] { weight_Array[i * 3], weight_Array[i * 3 + 1], weight_Array[i * 3 + 2]};
                temp = new ItemSet(init_Profit, init_Weight);
                item_Sets.Add(temp);
            }
        }

        public void dynamic_Programming_Method()
        {
            maximum_Value = 0;
            route = new int[item_Set_Count];
            DateTime start_Time = DateTime.Now;
            //首先初始化结果数组
            dynamic_Array = new int[item_Set_Count+1, c+1];
            //存储几个选择的各自结果
            int num1 = 0;
            int num2 = 0;
            for (int row = 1; row < item_Set_Count + 1; row++)
            {
                for (int col = 1; col < c + 1; col++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (col >= item_Sets[row - 1].get_Weight(i))
                        {
                            num1 = dynamic_Array[row - 1, col];
                            num2 = dynamic_Array[row - 1, col - item_Sets[row - 1].get_Weight(i)] + item_Sets[row - 1].get_Profit(i);
                            num1 = Math.Max(num1, num2);
                            //当前格子选取最大的
                            dynamic_Array[row, col] = Math.Max(num1, dynamic_Array[row, col]);
                        }
                        else
                        {
                            dynamic_Array[row, col] = Math.Max(dynamic_Array[row, col], dynamic_Array [row - 1, col]);
                        }
                    }
                }
            }
            //通过矩阵获得选择情况
            get_Dynamic_Route();
            maximum_Value = dynamic_Array[item_Set_Count, c];
            DateTime end_Time = DateTime.Now;
            process_Time = (end_Time - start_Time).TotalSeconds;
        }

        private void get_Dynamic_Route()
        {
            int col = c;
            for (int i = item_Set_Count; i > 0 && col > 0; i--)
            {
                //当前价值等于背包容量-1时
                if (dynamic_Array[i, col] == dynamic_Array[i, col - 1])
                {
                    col--;
                    i++;
                }
                //当前价值等于当前物品组不选并且同背包容量时
                else if (dynamic_Array[i, col] == dynamic_Array[i - 1, col])
                {
                    route[i - 1] = -1;
                }
                else
                {
                    //计算当前物品组选择的情况
                    for (int j = 0; j < 3; j++)
                    {
                        int temp_Weight = item_Sets[i - 1].get_Weight(j);
                        int temp_Profit = item_Sets[i - 1].get_Profit(j);
                        //选中当前物品组的第i个数据时
                        if ((dynamic_Array[i, col] - temp_Profit) == dynamic_Array[i - 1, col - temp_Weight])
                        {
                            col -= temp_Weight;
                            route[i - 1] = j + 1;
                            j = 4;
                        }
                    }
                }
            }
        }


        public void backtracking_Method()
        {
            maximum_Value = 0;
            DateTime start_Time = DateTime.Now;
            route = new int[item_Set_Count];
            temp_Route = new int[item_Set_Count];
            //初始化选择数组
            for (int i = 0; i < item_Set_Count; i++)
            {
                route[i] = -1;
                temp_Route[i] = -1;
            }

            //开始进行回溯
            back_Trace(0, 0, 0);

            for(int i = 0; i < item_Set_Count; i++)
            {
                route[i] = temp_Route[i];
            }
            DateTime end_Time = DateTime.Now;
            process_Time = (end_Time - start_Time).TotalSeconds;
        }


        //递归函数
        private void back_Trace(int group_Id, int profit_Now, int weight_Now)
        {
            //递归出口是当前深度到达最深
            if (group_Id == item_Set_Count)
            {
                //当前结果大于当前最优
                if (profit_Now > maximum_Value)
                {
                    for (int i = 0; i < item_Set_Count; i++)
                    {
                        route[i] = temp_Route[i];
                    }
                    maximum_Value = profit_Now;
                }
                return;
            }
            else
            {
                //在当前组分别进行下一步
                for (int i = 0; i < 3; i++)
                {
                    temp_Route[group_Id] = i + 1;
                    if (weight_Now + item_Sets[group_Id].get_Weight(i) <= c)
                    {
                        back_Trace(group_Id + 1, profit_Now + item_Sets[group_Id].get_Profit(i), weight_Now + item_Sets[group_Id].get_Weight(i));
                    }
                }
                //不选择当前组进行下一步
                temp_Route[group_Id] = -1;
                back_Trace(group_Id + 1, profit_Now, weight_Now);
            }
        }





    }
}
