using System;
using System.Data;
using System.Data.Sql;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 fuyi = Int32.MinValue;
            var fuyibytes = BitConverter.GetBytes(fuyi);

            Int32 zhengshu = 64;
            var zhengshubytes = BitConverter.GetBytes(zhengshu);


            Console.WriteLine("Hello World!");


            var bytes = BitConverter.GetBytes(float.MaxValue);

            var bits = new BitArray(bytes);

            

            Console.ReadLine();
        }
    }
}
