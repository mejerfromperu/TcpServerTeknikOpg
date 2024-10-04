using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    public class JsonObj
    {

        public JsonObj()
        {
            result = 0;
        }

        public string Method { get; set; }

        public double Num1 { get; set; }

        public double Num2 { get; set; }

        public double result { get; set; }


    }
}
