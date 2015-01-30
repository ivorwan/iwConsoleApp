using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Math
{
    public static class BinaryConverter
    {
        public static string Convert(int num)
        {
            string binaryResult = "";
            int quotient = num / 2;

            if (quotient == 0)
                binaryResult += num % 2;
            else
                while (quotient > 0)
                {
                    binaryResult += num % 2;
                    quotient = num / 2;
                    num = quotient;
                }

            return string.Join("", binaryResult.Reverse());
        }
    }
}
