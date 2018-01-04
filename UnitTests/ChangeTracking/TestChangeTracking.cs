using iwConsoleApp.ChangeTracking;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.ChangeTracking
{
    [TestClass]
    public class TestChangeTracking
    {
        public TestChangeTracking()
        {

        }
        [TestMethod]
        public void Test_ChangeTracking()
        {
            Employee emp = new Employee();
            emp.Id = 123;

            emp.PersonData = new PersonData();
            emp.PersonData.NameData = new NameData();
            emp.PersonData.NameData.FirstName = "eita";
            emp.PersonData.NameData.LastName = "porra";

            var ct = emp.ChangeTracking;
            //ct.Add("blah");
            Assert.IsTrue(ct.Contains(Employee.FIRST_NAME_PATH));
            Assert.IsTrue(ct.Contains(Employee.LAST_NAME_PATH));

            emp.PersonData.NameData.MiddleName = "middle";
            Assert.IsTrue(ct.Contains(Employee.MIDDLE_NAME_PATH));

        }
    }
}
