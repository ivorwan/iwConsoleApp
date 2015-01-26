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

        public string Method1(string p1, int p2)
        {
            return "";
        }

        public string Method1(string p1, int p2, int p3)
        {
            return "";
        }

        public string Method1(int p2, string p1)
        {
            return "";
        }
       

    }
}
