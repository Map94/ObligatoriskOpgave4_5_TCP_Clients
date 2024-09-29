using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ObligatoriskOpgave4_5_TCP_Clients
{
    public class JsonClient
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
                    string command = Console.ReadLine();

                    Console.WriteLine("Type Number1:");
                    string inputNumber1 = Console.ReadLine();
                    int number1 = int.Parse(inputNumber1);

                    Console.WriteLine("Type Number2::");
                    string inputNumber2 = Console.ReadLine();
                    int number2 = int.Parse(inputNumber2);

                    // Send Values to Client
                    JsonCommand commandObj = new JsonCommand()
                    {
                        Method = command,
                        Number1 = number1,
                        Number2 = number2
                    };

                    string commandAsJson = JsonSerializer.Serialize<JsonCommand>(commandObj);

                    Console.WriteLine(commandAsJson);

                    writer.WriteLine(commandAsJson);
                    writer.Flush();
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
