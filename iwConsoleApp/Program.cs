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

            
            Tree tree = new Tree("1");
            tree.Root.AddChildren(
                new TreeNode("2",
                    new TreeNode("3",
                        new TreeNode("4"),
                        new TreeNode("5")),
                    new TreeNode("6")),
                new TreeNode("8",
                    new TreeNode("9",
                        new TreeNode("10"),
                        new TreeNode("11")),
                    new TreeNode("12")));

            var resultNode = tree.DepthFirstSearch(tree.Root, "10");
            if (resultNode != null)
                Console.WriteLine("Found: " + resultNode.Data);
            else
                Console.WriteLine("Not Found");


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
