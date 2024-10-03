using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TcpServer
{
    public class ServerJson
    {

        private const int Port = 10;

        public void Start()
        {

            TcpListener Server = new TcpListener(Port);
            Server.Start();

            Console.WriteLine("Ready and running pls enter the follow: 'method', 'random', 'num1', 'num2'");




        }

        public void DoClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream()) { AutoFlush = true };

            try
            {
                string firstCommand = sr.ReadLine();
                Console.WriteLine($"Command recived {firstCommand}");

                JsonObj obj = JsonSerializer.Deserialize<JsonObj>(firstCommand);


                Console.WriteLine($"Here is what we made{obj}");

                if (obj.Method == "ADD")
                {
                    obj.result = obj.Num1 + obj.Num2;
                }
                else if (obj.Method == "SUBTRACT")
                {
                    obj.result = obj.Num1 - obj.Num2;
                }

                string respone = JsonSerializer.Serialize(obj);
                sw.WriteLine(respone );
                Console.WriteLine("sent");

            }
            catch (Exception ex)
            {
                throw new ArgumentException($"sorry invalid input: {ex}");
            }
            finally { socket.Close(); }

            
        }


    }
}
