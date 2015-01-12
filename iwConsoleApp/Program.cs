using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace iwConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //string test = null;

            LINQExamples ex = new LINQExamples();
            List<dynamic> result = ex.CountCharsInStringDynamic("iwdwds");
            Console.WriteLine(result[0].Key);
            Console.ReadKey();

        }
    }
}
