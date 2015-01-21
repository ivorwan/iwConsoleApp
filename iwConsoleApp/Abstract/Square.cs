using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Abstract
{
    public class Square : AShape
    {
        public double Side { get; set; }
        public override double Area()
        {
            return Side * Side;
        }

        public override string GetColor()
        {
            return "#cccccc";
        }
    }
}
