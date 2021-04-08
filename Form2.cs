using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2_Client_GUI
{
    public partial class Form2 : Form
    {
        private string user_Name;
        private NetworkSendData server_Linker;
        private string selected_File_Path;
        private DataExtraction dataExtraction;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            selected_File_Path = openFileDialog1.FileName;
            textBox1.Text = selected_File_Path;
            dataExtraction = new DataExtraction(selected_File_Path);
            listBox1.Items.Clear();
            dataExtraction.read_From_Selected_File_Path();
            for(int i = 0; i < dataExtraction.get_Data_Set_Count(); i++)
            {
                listBox1.Items.Add("DataSet-" + (i + 1).ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            MessageBox.Show(dataExtraction.get_Base_Data(index));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            dataExtraction.dynamic_Deal(index);
            int result = dataExtraction.get_Result(index);
            textBox2.Text = result.ToString();
            int[] selected = dataExtraction.get_Route(index);
            listBox2.Items.Clear();
            for (int i = 0; i < dataExtraction.get_items_Set_Count(index); i++)
            {
                listBox2.Items.Add((i+1).ToString()+":"+selected[i].ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            dataExtraction.backTracking_Deal(index);
            int result = dataExtraction.get_Result(index);
            textBox2.Text = result.ToString();
            int[] selected = dataExtraction.get_Route(index);
            listBox2.Items.Clear();
            for (int i = 0; i < dataExtraction.get_items_Set_Count(index); i++)
            {
                listBox2.Items.Add((i + 1).ToString() + ":" + selected[i].ToString());
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            dataExtraction.sort_By_The_Third_Item(index);
            int count = dataExtraction.get_Be_Sorted_Count();
            listBox3.Items.Clear();
            for(int i = 0; i < count; i++)
            {
                listBox3.Items.Add(dataExtraction.get_Be_Sorted_Str(i));
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            dataExtraction.create_Scatter_Chart(index);
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}
