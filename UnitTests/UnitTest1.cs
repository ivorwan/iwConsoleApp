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

        [TestMethod]
        public void TestFindNthToLast()
        {
            ListNode node = new ListNode("n1");
            node.AppendToLast("n2");
            node.AppendToLast("n3");
            node.AppendToLast("n4");
            node.AppendToLast("n5");
            node.AppendToLast("n6");
            node.AppendToLast("n7");
            
            LinkedList list = new LinkedList();
            var p = list.FindNthToLast(node, 2);

            Assert.AreEqual(p.Data, "n5");

            p = list.FindNthToLast(node, 1);
            Assert.AreEqual(p.Data, "n6");

            p = list.FindNthToLast(node, 5);
            Assert.AreEqual(p.Data, "n2");

            p = list.FindNthToLast(node, 0);
            Assert.AreEqual(p.Data, "n7");

        }


        [TestMethod]
        public void TestSkip()
        {
            ListNode node = new ListNode("n1");
            node.AppendToLast("n2");
            node.AppendToLast("n3");
            node.AppendToLast("n4");
            node.AppendToLast("n5");
            node.AppendToLast("n6");
            node.AppendToLast("n7");

            var p = node.Skip(2);
            Assert.AreEqual(p.Data, "n3");

            p = p.Skip(3);
            Assert.AreEqual(p.Data, "n6");
            
            p = p.Skip(5);
            Assert.AreEqual(p.Data, "n7");

        }

        [TestMethod]
        public void TestLinkedDupe()
        {
            ListNode node = new ListNode("n1");
            node.AppendToLast("n2");
            node.AppendToLast("n3");
            node.AppendToLast("n2");
            node.AppendToLast("n4");
            node.AppendToLast("n1");
            node.AppendToLast("n5");

            Assert.AreEqual(node.ToString(), "n1 -> n2 -> n3 -> n2 -> n4 -> n1 -> n5");
            Console.WriteLine();

            LinkedList list = new LinkedList();
            list.RemoveDuplicates(node);
            Assert.AreEqual(node.ToString(), "n1 -> n2 -> n3 -> n4 -> n5");
        }
    }
}
