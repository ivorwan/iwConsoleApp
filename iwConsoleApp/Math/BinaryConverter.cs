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
            binaryResult += num % 2;
            int temp = quotient;
            //if (quotient == 0)
            //    binaryResult += num % 2;
            //else
                while (quotient > 0)
                {
                    temp = quotient;
                    binaryResult = (temp % 2).ToString() + binaryResult;
                    quotient = temp / 2;
                    
                }

            //return string.Join("", binaryResult.Reverse());
                return binaryResult;
        }
    }
}
