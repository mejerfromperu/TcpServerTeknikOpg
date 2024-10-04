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
                
            Console.WriteLine("op and running");


            while (true)
            {
                // Accept client connection
                TcpClient socket = Server.AcceptTcpClient();
                Console.WriteLine("Connected! Ready and running pls enter the follow: 'method', 'random', 'num1', 'num2'");
                Task.Run(() => { DoClient(socket); });
            }

        }

        public void DoClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream()) { AutoFlush = true };

            try
            {
                string jsonString = sr.ReadLine();

                // Deserialize the JSON into a simple object
                JsonObj obj = JsonSerializer.Deserialize<JsonObj>(jsonString);

                // plsu og minus
                if (obj.Method == "ADD")
                {
                    obj.result = obj.Num1 + obj.Num2;
                }
                else if (obj.Method == "SUBTRACT")
                {
                    obj.result = obj.Num1 - obj.Num2;
                }



                string response = JsonSerializer.Serialize(new { result = obj.result });
                sw.WriteLine(response);

                
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"sorry invalid input: {ex}");
            }
            finally { socket.Close(); }

            
        }

        private void SendResponse(StreamWriter sw, string status, double result)
        {
            var response = new { status = status, result = result };
            string jsonResponse = JsonSerializer.Serialize(response);
            sw.WriteLine(jsonResponse);
        }


    }
}
