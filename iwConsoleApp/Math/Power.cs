using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Math
{
    public class Power
    {
        public Power()
        {

        }

        public int Calculate(int x, int y)
        {
            int twoToExp = x;
            int exp = 1;
            int total = 1;
            if (y == 0) return 1;

            while (exp <= y)
            {
                if ((exp & y) > 0)
                    total = total * twoToExp;
                exp = exp << 1;
                twoToExp = twoToExp * twoToExp;

            }
            return total;
        }
    }
}
