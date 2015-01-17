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

            Assert.IsTrue(result.Count == 4);
            Assert.IsTrue(result[0].Key == "i");
            
        }

        [TestMethod]
        public void TestLINQExamplesDynamic()
        {
            LINQExamples ex = new LINQExamples();
            List<dynamic> result = ex.CountCharsInStringDynamic("iwdwds");

            var x = result[0].Key;
            Assert.IsTrue(result.Count == 4);
            Assert.IsTrue(result[0].Key == "i");

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

            Assert.IsTrue(offers[0].OfferId == 3);
            Assert.IsTrue(offers[1].Url == "http://url4.com");
            //offers[0].OfferId
        }


        [TestMethod]
        public void TestDepthSearch()
        {
            Tree tree = new Tree("1");
            tree.Root.AddChildren(
                new TreeNode("2", 
                    new TreeNode("3",
                        new TreeNode("4"),
                        new TreeNode("5")),
                    new TreeNode("6")),
                new TreeNode("8",
                    new TreeNode("9",
                        new TreeNode("10"),
                        new TreeNode("11")),
                    new TreeNode("12")));

            var foundNode = tree.DepthFirstSearch(tree.Root, "10");
            Assert.IsNotNull(foundNode);
            Assert.AreEqual(foundNode.Data, "10");
            foundNode = tree.DepthFirstSearch(tree.Root, "7");
            Assert.IsNull(foundNode);

        }



    }
}
