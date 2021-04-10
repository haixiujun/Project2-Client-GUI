using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project2_Client_GUI
{
    public class NetworkSendData
    {
        private IPHostEntry server_IP;
        private IPEndPoint server_Interface;
        private Socket link_To_Server;
        private int receive_Count;
        private int connect_State;
        private DataExtraction dataExtraction;

        public NetworkSendData()
        {
            //server_IP = Dns.GetHostEntry("localhost");
            server_Interface = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 40400);
            link_To_Server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void init_DE(DataExtraction de)
        {
            dataExtraction = de;
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


        public void set_Unvisiable(Form1 form1)
        {
            form1.Visible = false;
        }

        public void close_Form1(Form1 form1)
        {
            form1.Close();
        }

        public void disconnect_To_Server()
        {
            try
            {
                link_To_Server.Send(Encoding.UTF8.GetBytes("END"));
                link_To_Server.Shutdown(SocketShutdown.Both);
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
                byte[] cache_Receive = new byte[1024*1024*50];
                byte[] clear_Array;
                receive_Count = link_To_Server.Receive(cache_Receive);
                clear_Array = cache_Receive.Take(receive_Count).ToArray();
                return System.Text.Encoding.UTF8.GetString(clear_Array);
            }
            catch (Exception e)
            {
                
            }
            return "Error";
        }

        public void DSW(string fn, int index)
        {
            try
            {
                int state = get_Connect_State();
                if (state == 1)
                {
                    send_Data_To_Server("DSW");
                    string temp = read_Data_From_Server();

                    if (temp.Equals("OK"))
                    {
                        int count = dataExtraction.get_items_Set_Count(index)*3;
                        send_Data_To_Server(count.ToString());

                        temp = read_Data_From_Server();
                        if (temp.Equals("OK"))
                        {
                            int i = 0;
                            do
                            {
                                string[] send = dataExtraction.get_DSW_Str(index, i);
                                for(int j = 0; j < 3&&temp.Equals("OK"); j++)
                                {
                                    send_Data_To_Server(fn + "#" + index.ToString() + "#" + i.ToString() + "#" + send[j]);
                                    temp= read_Data_From_Server();
                                }


                                i++;
                            } while (temp.Equals("OK")&&i<count);

                        }
                        
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public void RSW(string fn,int index)
        {
            try
            {
                int state = get_Connect_State();
                if (state == 1)
                {
                    send_Data_To_Server("RSW");
                    string temp = read_Data_From_Server();

                    if (temp.Equals("OK"))
                    {
                        send_Data_To_Server(fn + "#" + dataExtraction.get_RSW_Str(index));
                        temp = read_Data_From_Server();
                        if (temp.Equals("OK"))
                        {
                            MessageBox.Show("Result is successfully send to server!");

                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }




        public void REG(int id,string password)
        {
            try
            {
                int state = get_Connect_State();
                if (state == 1)
                {
                    ThreadPool.QueueUserWorkItem((obj) =>
                    {
                        send_Data_To_Server("REG");
                        string temp = read_Data_From_Server();

                        if (temp.Equals("OK"))
                        {
                            send_Data_To_Server("user#"+id.ToString() + "#" + password);
                            temp = read_Data_From_Server();
                            if (temp.Equals("OK"))
                            {
                                MessageBox.Show("Register successful!Your User ID is:" + id.ToString() + ".Password is :" + password);

                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }


                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }



        public void SIGNIN(int id,string password,Form1 form1)
        {
            try
            {
                int state = get_Connect_State();
                if (state == 1)
                {
                    send_Data_To_Server("SIN");
                    string temp = read_Data_From_Server();

                    if (temp.Equals("OK"))
                    {
                        send_Data_To_Server(id.ToString() + "#" + password);
                        temp = read_Data_From_Server();
                        if (temp.Equals("OK"))
                        {
                            set_Unvisiable(form1);
                            Form2 form2 = new Form2();
                            form2.init_NSD(this);
                            form2.Show();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


    }
}
