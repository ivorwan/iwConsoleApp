using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Recursion
{
    public static class Recursion
    {
        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new ArgumentException("must b >=0");
            if (n == 0)
                return 0;
            else if (n == 1)
                return 1;
            else 
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}
