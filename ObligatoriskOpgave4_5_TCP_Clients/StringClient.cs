using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave4_5_TCP_Clients
{
    public class StringClient
    {
        public string ip = "127.0.0.1";
        public int port = 8888;

        public void run()
        {
            Console.WriteLine("TCP Client");
            Console.WriteLine($"Connecting: {ip}:{port}");

            bool keepSending = true;

            try
            {
                TcpClient socket = new TcpClient(ip, port);

                Console.WriteLine($"Connected successfully to {ip}:{port}");

                NetworkStream ns = socket.GetStream();
                StreamReader reader = new StreamReader(ns);
                StreamWriter writer = new StreamWriter(ns);

                while (keepSending)
                {
                    Console.WriteLine("Type Command:");

                    string message = Console.ReadLine();

                    writer.WriteLine(message);
                    writer.Flush();

                    string response = reader.ReadLine();

                    // correct response continue.
                    if (response == "Input numbers")
                    {
                        Console.WriteLine("type Number1:");
                        string number1 = Console.ReadLine();

                        Console.WriteLine("Type Number2::");
                        string number2 = Console.ReadLine();

                        // Sends the typed numbers to the  Client
                        writer.WriteLine($"{number1} {number2}");
                        writer.Flush();

                        response = reader.ReadLine();
                    }

                    Console.WriteLine(response);

                    if (message.ToLower() == "stop")
                    {
                        keepSending = false;
                    }
                }

                socket.Close();

            }
            catch (SocketException ex)
            {
                Console.WriteLine($"No server found on {ip}:{port}");
            }
        }
    }
}
