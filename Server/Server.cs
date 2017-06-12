using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        public static string data = null;

        static void Main(string[] args)
        {
            byte[] ServerByte = new byte[1024];
            IPEndPoint ServerIPEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
            Socket ServerListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                ServerListener.Bind(ServerIPEP);
                ServerListener.Listen(10);
                while (true)
                {
                    Console.WriteLine("Waiting for connection...");
                    Socket ServerAccept = ServerListener.Accept();
                    data = null;
                    while (true)
                    {
                        ServerByte = new byte[1024];
                        int bytesRec = ServerAccept.Receive(ServerByte);
                        data += Encoding.ASCII.GetString(ServerByte, 0, bytesRec);
                        string path = "C:\\Users\\1201020005\\Documents\\Visual Studio 2015\\Projects\\TCP\\Server\\bin\\Debug\\asd.txt";
                        if (!File.Exists(path))
                        {
                            File.Create(path);
                            TextWriter tw = new StreamWriter(path);
                            tw.WriteLine(data);
                            tw.Close();

                            if (data.IndexOf("<stop>") > -1)
                            {
                                break;
                            }
                        }
                        else if (File.Exists(path))
                        {
                            TextWriter tw = new StreamWriter(path);
                            tw.WriteLine(data);
                            tw.Close();

                            if (data.IndexOf("<stop>") > -1)
                            {
                                break;
                            }
                        }

                        Console.WriteLine("Text received: {0}", data);
                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        ServerAccept.Send(msg);

                        ServerAccept.Shutdown(SocketShutdown.Both);
                        ServerAccept.Close();
                    }

                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
