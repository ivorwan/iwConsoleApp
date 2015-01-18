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
            bool p1 = false;
            bool p2 = false;


            //(p1 != null || p2 != null && (!(p1 == null && p2 == null)))


            Console.Write(((p1 | p2) & (!p1 & !p2)).ToString());
            p1 = false;
            p2 = true;
            Console.Write(((p1 | p2) & (!p1 & !p2)).ToString());

            p1 = true;
            p2 = false;
            Console.Write(((p1 | p2) & (!p1 & !p2)).ToString());

            p1 = true;
            p2 = true;
            Console.Write(((p1 | p2) & (!p1 & !p2)).ToString());
            Console.ReadKey();





            //string test = null;

            ArraysAndStrings q1 = new ArraysAndStrings();

            foreach (var method in q1.GetType().GetMethods())
            {
                method.Invoke(q1, null);
            }

            /*
            LINQExamples ex = new LINQExamples();
            List<dynamic> result = ex.CountCharsInStringDynamic("iwdwds");
             
            Console.WriteLine(result[0].Key);
             */

            Console.ReadKey();

        }
    }
}
