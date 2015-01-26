using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Abstract
{
    public class Circle : AShape
    {
        public double Radius { get; set; }
        public override double Area()
        {
            return (3.14 * Radius * Radius);
           
        }
    }
}
