using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Project2_Client_GUI
{
    class OutputToFile
    {
        private string file_Path;
        private int maximum_Value;
        private int[] route;
        private int[,] dynamic_Array;
        private int is_Dynamic;

        public OutputToFile(string f, int m,int[] r,int[,] d)
        {
            file_Path = f;
            maximum_Value = m;
            route = r;
            dynamic_Array = d;
            is_Dynamic = 1;
        }
        public OutputToFile(string f, int m, int[] r)
        {
            file_Path = f;
            maximum_Value = m;
            route = r;
            is_Dynamic = 0;
        }

        public void out_To_Txt()
        {
            if (!File.Exists(file_Path))
            {
                FileStream fs1 = new FileStream(file_Path, FileMode.Create, FileAccess.Write);
                fs1.Close();
            }
            StreamWriter sw = new StreamWriter(file_Path);
            string line = "maximum Value is:"+maximum_Value.ToString();
            sw.WriteLine(line);
            line = "the route is:";
            sw.WriteLine(line);
            line = "";
            for (int i = 0; i < route.Length; i++)
            {
                line += route[i].ToString() + " ";
            }
            sw.WriteLine(line);
            if (is_Dynamic == 1)
            {
                line = "The dynamic array is:";
                sw.WriteLine(line);
                for(int i=0;i< dynamic_Array.GetUpperBound(0); i++)
                {
                    line = "";
                    for(int j = 0; j < dynamic_Array.GetUpperBound(1); j++)
                    {
                        line += dynamic_Array[i, j].ToString() + " ";
                    }
                    sw.WriteLine(line);
                }
            }

            sw.Close();
            
        }


        public void out_To_Excel()
        {
            IWorkbook myXSSFworkbook = new XSSFWorkbook();
            ISheet sheet = myXSSFworkbook.CreateSheet("Result");
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("MaxResult:");
            row.CreateCell(1).SetCellValue(maximum_Value);
            row.CreateCell(2).SetCellValue("Route:");
            for(int i = 0; i < route.Length;i++)
            {
                row.CreateCell(i+3).SetCellValue(route[i].ToString());
            }
            if (is_Dynamic == 1)
            {
                for (int i = 0; i < dynamic_Array.GetUpperBound(0); i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dynamic_Array.GetUpperBound(1); j++)
                    {
                        row.CreateCell(j).SetCellValue(dynamic_Array[i, j]);
                    }
                }
            }
            FileStream fs = new FileStream(file_Path, FileMode.Create, FileAccess.ReadWrite);
            myXSSFworkbook.Write(fs);
            fs.Close();
        }


    }
}
