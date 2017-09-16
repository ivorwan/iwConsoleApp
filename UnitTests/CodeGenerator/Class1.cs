﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CodeGenerator
{
    [TestClass]
    class Class1
    {
        [TestMethod]
        public void Test()
        {

            string fn = @"location.Trim().Split(',')[{0}].Trim()";

            Func<string, string> fnState = iwConsoleApp.CodeGenerator.CodeGenerator.ToFunc<string, string>(String.Format(fn, "1"), "location");
            Func<string, string> fnCity = iwConsoleApp.CodeGenerator.CodeGenerator.ToFunc<string, string>(String.Format(fn, "0"), "location");


            string state = fnState("Los Angeles, CA");
            string city = fnCity("Los Angeles, CA");

            Assert.AreEqual(state, "CA");
            Assert.AreEqual(city, "Los Angeles");
            
        }
    }
}
