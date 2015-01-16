using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iwConsoleApp;
using System.Collections.Generic;
using System.Reflection;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestLINQExamples()
        {
            LINQExamples ex = new LINQExamples();
            var result = ex.CountCharsInString("iwdwds");

            Assert.AreEqual(result.Count, 4);
            Assert.AreEqual(result[0].Key, "i");
            
        }

        [TestMethod]
        public void TestLINQExamplesDynamic()
        {
            LINQExamples ex = new LINQExamples();
            List<dynamic> result = ex.CountCharsInStringDynamic("iwdwds");

            Assert.AreEqual(result.Count, 4);
            Assert.AreEqual(result[0].Key,  "i");

        }



        [TestMethod]
        public void TestXml()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "UnitTests.Data.XMLFile.xml";

            LINQExamples ex = new LINQExamples();
            List<Offer> offers;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                offers = ex.ReadXml(stream, "domain2");
            }

            Assert.AreEqual(offers[0].OfferId, 3);
            Assert.AreEqual(offers[1].Url, "http://url4.com");
            //offers[0].OfferId
        }
    }
}
