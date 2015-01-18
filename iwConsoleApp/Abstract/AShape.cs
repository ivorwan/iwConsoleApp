using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Abstract
{
   public abstract class AShape
    {
       public virtual string Color { get; set; }
       public abstract double Area();
       public virtual string GetColor()
       {
           return "ffffff";
       }
    }
}
