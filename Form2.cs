using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        private string log;

        public Form2()
        {
            InitializeComponent();
            log = "";

        }

        public void init_NSD(NetworkSendData nsd)
        {
            server_Linker = nsd;
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
            server_Linker.init_DE(dataExtraction);
            listBox1.Items.Clear();
            dataExtraction.read_From_Selected_File_Path();
            for (int i = 0; i < dataExtraction.get_Data_Set_Count(); i++)
            {
                listBox1.Items.Add("DataSet-" + (i + 1).ToString());
            }
            log += DateTime.Now.ToString() + ":Read Data From File\n";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            MessageBox.Show(dataExtraction.get_Base_Data(index));
            log += DateTime.Now.ToString() + ":Show "+index+"'s Base Data\n";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                log += DateTime.Now.ToString() + ":Start to Dynamic Deal the Question\n";
                DateTime dt = DateTime.Now;
                int index = listBox1.SelectedIndex;
                dataExtraction.dynamic_Deal(index);
                int result = dataExtraction.get_Result(index);
                textBox2.Text = result.ToString();
                int[] selected = dataExtraction.get_Route(index);
                listBox2.Items.Clear();
                for (int i = 0; i < dataExtraction.get_items_Set_Count(index); i++)
                {
                    listBox2.Items.Add((i + 1).ToString() + ":" + selected[i].ToString());
                }
                DateTime dtt = DateTime.Now;
                textBox3.Text= Convert.ToString((dtt - dt).TotalSeconds);
                log += DateTime.Now.ToString() + ":Dynamic Deal Finished\n";
            });
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                log += DateTime.Now.ToString() + ":Start BackTracking Deal the Question\n";
                DateTime dt = DateTime.Now;
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
                DateTime dtt = DateTime.Now;
                textBox3.Text = Convert.ToString((dtt - dt).TotalSeconds);
                log += DateTime.Now.ToString() + ":Back Tracking Deal finished\n";
            });
               
        }

        private void button5_Click(object sender, EventArgs e)
        {
            log += DateTime.Now.ToString() + ":Start Sort Selected Data Set\n";
            int index = listBox1.SelectedIndex;
            dataExtraction.sort_By_The_Third_Item(index);
            int count = dataExtraction.get_Be_Sorted_Count();
            listBox3.Items.Clear();
            for(int i = 0; i < count; i++)
            {
                listBox3.Items.Add(dataExtraction.get_Be_Sorted_Str(i));
            }
            log += DateTime.Now.ToString() + ":Sorted Selected Data Set Finished\n";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            log += DateTime.Now.ToString() + ":Create Selected Scatter Chart\n";
            int index = listBox1.SelectedIndex;
            dataExtraction.create_Scatter_Chart(index);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            log += DateTime.Now.ToString() + ":Start Out Put Selected Data Set Result To TXT File\n";
            saveFileDialog1.ShowDialog();
            int index = listBox1.SelectedIndex;
            dataExtraction.out_Put_To_Txt(index,saveFileDialog1.FileName);
            log += DateTime.Now.ToString() + ":Out Put Selected Data Set Result To TXT File Finished\n";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            log += DateTime.Now.ToString() + ":Start Out Put Selected Data Set Result To XLSX File\n";
            saveFileDialog2.ShowDialog();
            int index = listBox1.SelectedIndex;
            dataExtraction.out_Put_To_Excel(index, saveFileDialog2.FileName);
            log += DateTime.Now.ToString() + ":Out Put Selected Data Set Result To XLSX File Finished\n";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            log += DateTime.Now.ToString() + ":Try Reconnecting To The Server\n";
            server_Linker.disconnect_To_Server();
            server_Linker = new NetworkSendData();
            server_Linker.connect_To_Server();
            log += DateTime.Now.ToString() + ":Connecting To The Server Successfully\n";


        }

        private void button10_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            server_Linker.RSW(openFileDialog1.SafeFileName,index);
            log += DateTime.Now.ToString() + ":Selected Data Set Result Successfully Stored In Data Base\n";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            server_Linker.DSW(openFileDialog1.SafeFileName,index);
            log += DateTime.Now.ToString() + ":Selected Data Set All Data Successfully Stored In Data Base\n";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            server_Linker.DSR(openFileDialog1.SafeFileName, index, listBox3);
            log += DateTime.Now.ToString() + ":Read Selected Data Set Data From Data Base\n";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            log += DateTime.Now.ToString() + ":Clear ListBox3\n";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            server_Linker.RSRA(listBox4);
            log += DateTime.Now.ToString() + ":Read All Data Set Result From Data Base\n";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            log += DateTime.Now.ToString() + ":Application End\n";
            server_Linker.send_Data_To_Server("END");
            Application.Exit();
        }

        private void button15_Click(object sender, EventArgs e)
        {

            log += DateTime.Now.ToString() + ":Start Genetic Algorithm To Deal  Selected Data Set\n";
            DateTime dt = DateTime.Now;
            int index = listBox1.SelectedIndex;
            string[] results = dataExtraction.genetic_Algorithm(index).Split("\n");
            int[] route = new int[results.Length-1];
            textBox2.Text = results[0];
            listBox2.Items.Clear();
            for(int i=0; i < dataExtraction.get_items_Set_Count(index); i++)
            {
                if (results[i + 1].Equals("4"))
                {
                    results[i + 1] = "-1";
                    
                }
                route[i] = Convert.ToInt32(results[i + 1]);
                listBox2.Items.Add(results[i + 1]);
                
            }
            dataExtraction.set_Result(index,Convert.ToInt32(results[0]));
            dataExtraction.set_Route(index, route);
            DateTime dtt = DateTime.Now;
            textBox3.Text = Convert.ToString((dtt - dt).TotalSeconds);
            log += DateTime.Now.ToString() + ":Genetic Algorithm To Deal  Selected Data Set Finished\n";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text += log;
            log = "";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            log += DateTime.Now.ToString() + ":Auto Clear Log Successfully\n";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            System.GC.Collect();
        }
    }
}
