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
    public partial class Form1 : Form
    {
        private NetworkSendData server_Linker;
        public Form1()
        {
            InitializeComponent();
            server_Linker = new NetworkSendData();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            server_Linker.connect_To_Server();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user_ID = textBox1.Text;
            string password = textBox2.Text;
            server_Linker.SIGNIN(Convert.ToInt32(user_ID), password,this);

        }

        public NetworkSendData get_Dsd()
        {
            return server_Linker;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user_ID = textBox1.Text;
            string password = textBox2.Text;
            server_Linker.REG(Convert.ToInt32(user_ID), password);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            server_Linker.send_Data_To_Server("END");
        }
    }
}
