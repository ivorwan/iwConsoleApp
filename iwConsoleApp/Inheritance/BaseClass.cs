using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Inheritance
{
    public class BaseClass
    {
        public void Write1()
        {
            Console.WriteLine("Base Write1");
        }

        public virtual void Write2()
        {
            Console.WriteLine("Base Write2");
        }
    }

    public class DerivedClass : BaseClass
    {
        public new void Write1()
        {
            Console.WriteLine("Derived Write1");
        }

        public override void Write2()
        {
            Console.WriteLine("Derived Write2");
        }
    }
}
