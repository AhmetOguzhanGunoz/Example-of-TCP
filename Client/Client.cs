using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[1024];
            try
            {
                IPEndPoint ClientIPEP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
                Socket ClientSender = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
                try
                {
                    ClientSender.Connect(ClientIPEP);
                    Console.WriteLine("Socket connected to {0}", ClientSender.RemoteEndPoint.ToString());

                    string filename = "write location of asd.txt in debug/bin folder";

                    ClientSender.SendFile(filename);

                    byte [] msg = Encoding.ASCII.GetBytes("This is a test <stop>");
                    int bytesSent = ClientSender.Send(msg);

                    int bytesRec = ClientSender.Receive(bytes);
                    Console.WriteLine("Echoed test  = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    ClientSender.Shutdown(SocketShutdown.Both);
                    ClientSender.Close();
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("Argument Null Exception: {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("Socket Exception: {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected Exception: {0}", e.ToString());
                }
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
