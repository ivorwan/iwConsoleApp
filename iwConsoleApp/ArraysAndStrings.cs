using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp
{
    public class ArraysAndStrings
    {
        /// <summary>
        /// Implement an algorithm to determine if a string has all unique characters. 
        /// What if you can not use additional data structures?
        /// </summary>
        public void Question1() 
        { 
            string str = "iwsdds";
            bool dup = false;


            // solution 1
            for (int i = 0; i< str.Length; i++)
            {
                if (str.IndexOf(str[i], i + 1) >= 0)
                {
                    dup = true;
                    break;
                }
            }
            Console.WriteLine(dup);
           
            // solution 2
            var result = str.GroupBy(c => c).Select(g => new {Key = g.Key, Count = g.Count()}).Where(k => k.Count > 1).ToList();

            // solution 3.
            // can we create array? is that considered a data structure?
            bool[] bits = new bool[256];

            for (int i = 0; i < str.Length; i++)
            {
                if (bits[(int)str[i]])
                {
                    dup = false;
                    break;
                }
                else
                {
                    bits[(int)str[i]] = true;
                }
            }
        
        }

        /// <summary>
        /// Write code to reverse a C-Style String. 
        /// (C-String means that “abcd” is represented as five characters, including the null character.)
        /// </summary>
        public void Question2()
        {
            string str = "abcdefg";
            StringBuilder sb = new StringBuilder();

            for (int i = str.Length-1; i >= 0; i--){
                sb.Append(str[i]);
            }

            string result = sb.ToString();
        }



        /// <summary>
        ///  Design an algorithm to print all permutations of a string   
        ///  For simplicity, assume all characters are unique 
        /// </summary>
        public void Question3()
        {
            string str = "abc";
            var x = Perm(str);


        }
        /// <summary>
        /// returns all permutations for str
        /// eg:
        /// P(a) -> a
        /// P(ab) -> ab, ba
        /// P(abc) -> a, b, c, ab, ba, ac, ca, bc, cb, abc, acb, bca, bac, cab, cba
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<string> Perm(string str)
        {


            
            List<string> ret = new List<string>();
            if (str.Length == 1)
            {
                ret.Add(str);
            }
            else
            {

                //string ch = str.Substring(0, str.Length - 1);
                //string c = str.Substring(str.Length - 1);
                for (int i = 0; i <= str.Length - 1; i++)
                {
                    string ch = str.Substring(i, 1);
                    string others = str.Substring(0, i) + str.Substring(i + 1);
                    ret.Add(ch);
                    foreach (string o in Perm(others))
                    {
                        ret.Add(ch + o);
                    }
                    
                    
                }

            }
            return ret;
            
        }
    }
}
