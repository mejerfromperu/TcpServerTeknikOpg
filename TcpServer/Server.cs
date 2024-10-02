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


                Task.Run(() => { DoClient(socket); });
                
            }

        }

        public void DoClient(TcpClient socket)
        {
            StreamReader sr = new StreamReader(socket.GetStream());
            StreamWriter sw = new StreamWriter(socket.GetStream());


            string l = sr.ReadLine();

            Console.WriteLine("Modtaget besked: " + l);
            sw.WriteLine(l);
        }



    }
}
