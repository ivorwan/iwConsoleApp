using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Search
{
    public class PriceIsRight
    {
        public int Find(int[] guesses, int price)
        {
            int correcIndex = -1;

            for (int i = 0; i <= guesses.Length - 1; i++)
            {
                if (guesses[i] <= price)
                {
                    if (correcIndex < 0)
                        correcIndex = 0;

                    //correcIndex = i;
                    if (guesses[i] >= guesses[correcIndex])
                        correcIndex = i;
                }
            }

            return correcIndex;

        }

        public int Find2(int[] guesses, int price)
        {
            Array.Sort(guesses);

            int m = guesses.Length / 2;
            int l = 0;
            int u = guesses.Length - 1;

            while (u - l > 1)
            {
                if (price > guesses[m])
                {
                    l = m;
                    m = l + (u - l) / 2;
                }
                else if (price < guesses[m])
                {
                    u = m;
                    m = l + (u - l) / 2;
                }
                else
                {
                    u = m;
                    l = m;
                }
            }
            return m;
        }
    }
}
