using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class Server
    {

        private const int Port = 7;

        public void Start()
        {
            TcpListener server = new TcpListener(Port);
            server.Start();

            Console.WriteLine("you are ready to type: ");


            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Console.WriteLine("client connected.");

                Task.Run(() => { DoClient(socket); });
                
            }

        }

        public void DoClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream()) { AutoFlush = true};
            try
            {
                string method = sr.ReadLine();
                Console.WriteLine($"recived command {method}");
                // step 2
                if (method == "ADD")
                {
                    sw.WriteLine("pls give 2 numbers to be added");
                }
                else if (method == "SUBTRACT")
                {
                    sw.WriteLine("pls give 2 numbers that should be subtracted ");
                }
                else
                {
                    sw.WriteLine("??????????");
                    return;
                }

                // stepp 3
                string numbers = sr.ReadLine();
                Console.WriteLine($"recived numbers: {numbers}");
                string[] parts = numbers.Split(' ');
                if (parts.Length != 2 || !int.TryParse(parts[0], out int num1) || !int.TryParse(parts[1], out int num2))
                {
                    sw.WriteLine("invalid input, has to be 2 numbers seperated with space");
                    return;
                }
                int result = numbers == "ADD" ? num1 + num2 : num1 - num2;
                sw.WriteLine($"result: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error 404: {ex.Message}");
            }
            finally
            {
                socket.Close();
            }
            
        }



    }
}
