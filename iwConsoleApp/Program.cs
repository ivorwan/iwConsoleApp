﻿using iwConsoleApp.Inheritance;
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

            BaseClass bc = new DerivedClass();
            bc.Write1();
            bc.Write2();


            DerivedClass dc = new DerivedClass();
            dc.Write1();
            dc.Write2();
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
