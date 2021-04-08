using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project2_Client_GUI
{
    class NetworkSendData
    {
        private IPHostEntry server_IP;
        private IPEndPoint server_Interface;
        private Socket link_To_Server;
        private int receive_Count;
        private int connect_State;

        public NetworkSendData()
        {
            server_IP = Dns.GetHostEntry("jiaaoyang.top");
            server_Interface = new IPEndPoint(server_IP.AddressList[0], 40400);
            link_To_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public int get_Connect_State()
        {
            return connect_State;
        }

        public void connect_To_Server()
        {
            try
            {
                link_To_Server.Connect(server_Interface);
                connect_State = 1;
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                connect_State = 0;
            }
        }

        public void disconnect_To_Server()
        {
            try
            {
                link_To_Server.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void send_Data_To_Server(string s)
        {
            try
            {
                link_To_Server.Send(Encoding.UTF8.GetBytes(s));
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public string read_Data_From_Server()
        {
            try
            {
                byte[] cache_Receive = new byte[104857600];
                receive_Count = link_To_Server.Receive(cache_Receive);
                return System.Text.Encoding.UTF8.GetString(cache_Receive);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return "Error";
        }


    }
}
