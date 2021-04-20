using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    public class DataExtraction
    {
        private string selected_File_Path;
        private int number_Of_Original_File_Lines;
        private int number_Of_Handled_File_Lines;
        private int data_Set_Count;
        private List<DataSet> data_Sets;
        private SortByTheThirdItem sortByTheThirdItem;
        public DataExtraction(string path)
        {
            selected_File_Path = path;
        }

        public void set_Result(int index,int num)
        {
            data_Sets[index].set_Max_Value(num);
           
        }

        public void set_Route(int index,int[] selected)
        {
            data_Sets[index].set_Selected(selected);
        }


        public string[] get_DSW_Str(int index,int item_Ser)
        {
            return data_Sets[index].get_Item_Set_Str(item_Ser);
        }

        public string get_RSW_Str(int index)
        {
            string ret_Str = "";
            ret_Str += index + "#";
            ret_Str += data_Sets[index].get_Item_Sets_Count().ToString() + "#";
            ret_Str += data_Sets[index].get_Cubage().ToString() + "#";
            ret_Str += data_Sets[index].get_Result().ToString();
            return ret_Str;
        }


        public string get_Base_Data(int index)
        {
            string temp = "Item Sets Counts:";
            temp += data_Sets[index].get_Item_Sets_Count().ToString()+" Bag Cubage:"+data_Sets[index].get_Cubage().ToString();
            return temp;


        }

        public int get_items_Set_Count(int index)
        {
            return data_Sets[index].get_Item_Sets_Count();
        }

        public int[] get_Route(int index)
        {
            return data_Sets[index].get_Route();
        }

        public int get_Data_Set_Count()
        {
            return data_Set_Count;
        }

        public void read_From_Selected_File_Path()
        {
            data_Sets = new List<DataSet>();
            //清空中间数据
            string temp_Data = "";
            //读取文件按行读取
            String line = "";
            //文件行数计数器清零
            number_Of_Original_File_Lines = 0;
            number_Of_Handled_File_Lines = 0;
            StreamReader stream = new StreamReader(selected_File_Path);
            try
            {
                while ((line = stream.ReadLine()) != null)
                {
                    //如果是空行则不加到中间数据
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        temp_Data += line + "\n";
                        number_Of_Original_File_Lines++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR");
            }
            //文件行数要去掉第一行和最后一行的开始结束符
            number_Of_Handled_File_Lines = number_Of_Original_File_Lines - 2;
            //去掉第一行
            int first_Line_End_Index = temp_Data.IndexOf('\n');
            temp_Data = temp_Data.Remove(0, first_Line_End_Index + 1);

            //去掉最后一行
            int last_Line_Start_Index = temp_Data.LastIndexOf('\n');
            temp_Data = temp_Data.Substring(0, last_Line_Start_Index);

            //写入中间数据文件
            string temp = "";
            StreamWriter sw = new StreamWriter("test.txt");
            sw.Write(temp_Data);
            sw.Close();
            //检测是否是6的倍数，每个数据集在文件中应该为6行
            if (number_Of_Handled_File_Lines % 6 != 0)
            {
                Console.WriteLine("ERROR");
                return;
            }
            //通过文件行数来计算数据集的数量
            data_Set_Count = number_Of_Handled_File_Lines / 6;

            StreamReader sr = new StreamReader("test.txt");
            for (int i = 0; i < data_Set_Count; i++)
            {
                DataSet temp_Set;
                //奇数行为提示信息，不需要进行处理，仅对偶数行进行处理
                temp = sr.ReadLine();
                temp = sr.ReadLine();
                //提取d和c的字符串
                string[] blocks = temp.Split(",");
                string d_Str = blocks[0].Split("*")[1];
                string c_Str = blocks[1].Split(" ").Last();

                //去掉结尾的字符
                c_Str = c_Str.Substring(0, c_Str.Length - 1);

                //将d和c的字符串转为整型
                int temp_d = Convert.ToInt32(d_Str);
                int temp_c = Convert.ToInt32(c_Str);

                //初始化当前数据集
                temp_Set = new DataSet(temp_d, temp_c);

                //读取profit行的字符串
                temp = sr.ReadLine();
                temp = sr.ReadLine();
                //切割profit行字符串
                temp = temp.Substring(0, temp.Length - 1);
                string[] profit_Array_Str = temp.Split(",");

                //读取weight行的字符串
                temp = sr.ReadLine();
                temp = sr.ReadLine();
                //切割weight行字符串
                temp = temp.Substring(0, temp.Length - 1);
                string[] weight_Array_Str = temp.Split(",");

                //初始化profit和weight数组
                int[] profit_Array = new int[profit_Array_Str.Length];
                int[] weight_Array = new int[weight_Array_Str.Length];
                for (int j = 0; j < profit_Array_Str.Length; j++)
                {
                    //对应转换
                    profit_Array[j] = Convert.ToInt32(profit_Array_Str[j]);
                    weight_Array[j] = Convert.ToInt32(weight_Array_Str[j]);
                }

                //初始化数据集的profit和weight数组
                temp_Set.init_Item_Sets(profit_Array, weight_Array);
                //加入数据集列表中
                data_Sets.Add(temp_Set);
            }
            sr.Close();
            //删除中间文件
            File.Delete("test.txt");
        }

        public void dynamic_Deal(int index)
        {
            data_Sets[index].dynamic_Programming_Method();
        }

        public int get_Result(int index)
        {
            return data_Sets[index].get_Result();
        }

        public void backTracking_Deal(int index)
        {
            data_Sets[index].backtracking_Method();

        }

        public int get_Be_Sorted_Count()
        {
            return sortByTheThirdItem.get_Item_Count();
        }

        public string get_Be_Sorted_Str(int index)
        {
            return sortByTheThirdItem.get_Str(index);
        }

        public void sort_By_The_Third_Item(int index)
        {
            sortByTheThirdItem = new SortByTheThirdItem(data_Sets[index]);
            sortByTheThirdItem.start_Sort();
        }

        public void create_Scatter_Chart(int index)
        {
            Form3 form3 = new Form3(data_Sets[index]);
            form3.Show();
        }

        public void out_Put_To_Txt(int index,string file_Path)
        {
            int max_Result = data_Sets[index].get_Result();
            int[] route = data_Sets[index].get_Route();
            int is_Dynamic = data_Sets[index].get_Is_Dynmaic();
            OutputToFile outputToFile;
            if (is_Dynamic == 1)
            {
                int[,] temp_Dynamic_Array= data_Sets[index].get_Dynamic_Arrays();
                outputToFile = new OutputToFile(file_Path,max_Result,route,temp_Dynamic_Array);
            }
            else
            {
                outputToFile = new OutputToFile(file_Path, max_Result, route);
            }
            outputToFile.out_To_Txt();

        }

        public void out_Put_To_Excel(int index,string file_Path)
        {
            int max_Result = data_Sets[index].get_Result();
            int[] route = data_Sets[index].get_Route();
            int is_Dynamic = data_Sets[index].get_Is_Dynmaic();
            OutputToFile outputToFile;
            if (is_Dynamic == 1)
            {
                int[,] temp_Dynamic_Array = data_Sets[index].get_Dynamic_Arrays();
                outputToFile = new OutputToFile(file_Path, max_Result, route, temp_Dynamic_Array);
            }
            else
            {
                outputToFile = new OutputToFile(file_Path, max_Result, route);
            }
            outputToFile.out_To_Excel();
        }


        public string get_Result_Str(int index)
        {
            int max_Result = data_Sets[index].get_Result();

            return max_Result.ToString();
        }


        public string get_Route_Str(int index)
        {
            string route_Str = "";
            int[] route = data_Sets[index].get_Route();
            for(int i = 0; i < route.Length; i++)
            {
                route_Str += route[i].ToString() + "&";
            }
            route_Str += route[route.Length - 1].ToString();
            return route_Str;
        }

        public string genetic_Algorithm(int index)
        {
            StreamWriter sw = new StreamWriter("data.txt");
            int item_Set_Count = data_Sets[index].get_Item_Sets_Count();
            int cubage = data_Sets[index].get_Cubage();
            string profit = data_Sets[index].get_All_Profit_Str();
            string weight = data_Sets[index].get_All_Weight_Str();
            try
            {


                sw.WriteLine(item_Set_Count);
                sw.WriteLine(cubage);
                sw.WriteLine(profit);
                sw.WriteLine(weight);
                sw.Close();
                using (Process p = new Process())
                {

                    p.StartInfo.FileName = @"./Test1.exe";//可执行程序路径
                    p.StartInfo.Arguments = "";//参数以空格分隔，如果某个参数为空，可以传入""
                    p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
                    p.StartInfo.CreateNoWindow = true;//不显示程序窗口
                    p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
                    p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                    p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                    p.Start();
                    p.WaitForExit();
                    //正常运行结束放回代码为0
                    string result = "";
                    if (p.ExitCode == 0)
                    {
                        result = p.StandardOutput.ReadToEnd();
                        return result;
                    }
                }



            }
            catch(Exception e)
            {

            }
            finally
            {
                
            }
            return "ERROR";
           
        }


    }
}
