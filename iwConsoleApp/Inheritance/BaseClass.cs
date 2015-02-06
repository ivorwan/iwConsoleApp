using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.Inheritance
{
    public class BaseClass
    {
        public string Write()
        {
            Console.WriteLine("Base Write1");
            return "Base Write1";
        }

        public virtual string WriteVirtual()
        {
            Console.WriteLine("Base Write2");
            return "Base Write2";
        }

        
    }

    public class DerivedClass : BaseClass
    {
        public new string Write()
        {
            Console.WriteLine("Derived Write1");
            return "Derived Write1";
        }

        public override string WriteVirtual()
        {
            Console.WriteLine("Derived Write2");
            return "Derived Write2";
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



    public abstract class Person
    {
        protected string name;
        public Person(string name)
        {
            this.name = name;
        }
        public abstract string GetName();

        public virtual string GetNameVirtual()
        {
            return name; ;
        }

        public virtual string GetNameVirtualWithOverride()
        {
            return name; ;
        }
    }

    public class Student : Person
    {

        public Student(string name) : base(name)
        {
        }
        public override string GetName()
        {
            return name;
        }

        public override string GetNameVirtualWithOverride()
        {
            return "[Student] " + base.GetNameVirtualWithOverride();
        }
    }
}
