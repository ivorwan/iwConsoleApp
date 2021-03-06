﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iwConsoleApp;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using iwConsoleApp.DataStructures;
using iwConsoleApp.Math;
using iwConsoleApp.Inheritance;
using iwConsoleApp.Search;

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
        public void TestAddOps()
        {
            ListNode op1 = new ListNode("3");
            op1.AppendToLast("1");
            op1.AppendToLast("5");

            ListNode op2 = new ListNode("5");
            op2.AppendToLast("9");
            op2.AppendToLast("2");

            LinkedList list = new LinkedList();


            var result = list.AddOps(op1, op2);
            Assert.AreEqual(result.ToString(), "8 -> 0 -> 8");

            op1 = new ListNode("0");
            op1.AppendToLast("5");
            op1.AppendToLast("7");

            op2 = new ListNode("4");
            op2.AppendToLast("7");
            op2.AppendToLast("4");
            op2.AppendToLast("9");

            result = list.AddOps(op1, op2);
            Assert.AreEqual(result.ToString(), "4 -> 2 -> 2 -> 0 -> 1");
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


        [TestMethod]
        public void TestBinaryConverter()
        {
            Assert.AreEqual(BinaryConverter.Convert(1), "1");
            Assert.AreEqual(BinaryConverter.Convert(3), "11");
            Assert.AreEqual(BinaryConverter.Convert(10), "1010");
            Assert.AreEqual(BinaryConverter.Convert(11), "1011");
            Assert.AreEqual(BinaryConverter.Convert(15), "1111");
            Assert.AreEqual(BinaryConverter.Convert(25), "11001");

        }

        [TestMethod]
        public void TestInheritance()
        {
            BaseClass bc = new BaseClass();
            Assert.AreEqual(bc.Write(), "Base Write1");
            Assert.AreEqual(bc.WriteVirtual(), "Base Write2");

            DerivedClass dc = new DerivedClass();
            Assert.AreEqual(dc.Write(), "Derived Write1");
            Assert.AreEqual(dc.WriteVirtual(), "Derived Write2");

            BaseClass dc2 = new DerivedClass();
            Assert.AreEqual(dc2.Write(), "Base Write1");
            Assert.AreEqual(dc2.WriteVirtual(), "Derived Write2");
            // ----------

            Person student = new Student("John");
            Assert.AreEqual(student.GetName(), "John");
            Assert.AreEqual(student.GetNameVirtual(), "John");
            Assert.AreEqual(student.GetNameVirtualWithOverride(), "[Student] John");


        }


        [TestMethod]
        public void TestStack()
        {
            Stack stack = new Stack();
            stack.Push(new StackNode("1"));
            Assert.AreEqual(stack.Head.ToString(), "1");

            stack.Push(new StackNode("2"));
            Assert.AreEqual(stack.Head.ToString(), "2 -> 1");

            stack.Push(new StackNode("3"));
            Assert.AreEqual(stack.Head.ToString(), "3 -> 2 -> 1");

            StackNode popNode = stack.Pop();
            Assert.AreEqual(stack.Head.ToString(), "2 -> 1");
            Assert.AreEqual(popNode.ToString(), "3");


        }

        [TestMethod]
        public void TestPriceIsRight()
        {
            int[] guesses = new int[10];
            guesses[0] = 2;
            guesses[1] = 1;
            guesses[2] = 7;
            guesses[3] = 9;
            guesses[4] = 5;
            guesses[5] = 6;
            guesses[6] = 3;
            guesses[7] = 10;
            guesses[8] = 13;
            guesses[9] = 12;

            var pir = new PriceIsRight();
            Assert.AreEqual(pir.Find(guesses, 11), 7);

            Assert.AreEqual(pir.Find(guesses, 8), 2);
            Assert.AreEqual(pir.Find(guesses, 12), 9);



            int[] sortedGuesses = new int[10];
            guesses[0] = 1;
            guesses[1] = 2;
            guesses[2] = 3;
            guesses[3] = 5;
            guesses[4] = 6;
            guesses[5] = 7;
            guesses[6] = 9;
            guesses[7] = 10;
            guesses[8] = 12;
            guesses[9] = 13;

            Assert.AreEqual(pir.Find2(guesses, 11), 7);

            Assert.AreEqual(pir.Find2(guesses, 8), 5);
            Assert.AreEqual(pir.Find2(guesses, 12), 8);

        }

        [TestMethod]
        public void TestPower()
        {
            Power pw = new Power();

            Assert.AreEqual(pw.Calculate(5, 0), 1);
            Assert.AreEqual(pw.Calculate(5, 1), 5);
            Assert.AreEqual(pw.Calculate(5, 2), 25);
            Assert.AreEqual(pw.Calculate(5, 3), 125);
            Assert.AreEqual(pw.Calculate(5, 4), 625);
            Assert.AreEqual(pw.Calculate(5, 10), 9765625);
            


        }

        [TestMethod]
        public void TestPemut()
        {
            ArraysAndStrings svc = new ArraysAndStrings();
            List<string> r1 = svc.Perm("a");
            Assert.IsTrue(r1.Contains("a"));

            List<string> r2 = svc.Perm("ab");
            Assert.IsTrue(r2.Contains("ab"));
            Assert.IsTrue(r2.Contains("ba"));

            List<string> r3 = svc.Perm("abc");
            Assert.IsTrue(r3.Contains("a"));
            Assert.IsTrue(r3.Contains("ab"));
            Assert.IsTrue(r3.Contains("ac"));
            Assert.IsTrue(r3.Contains("abc"));
            Assert.IsTrue(r3.Contains("acb"));
            Assert.IsTrue(r3.Contains("b"));
            Assert.IsTrue(r3.Contains("ba"));
            Assert.IsTrue(r3.Contains("bc"));
            Assert.IsTrue(r3.Contains("bac"));
            Assert.IsTrue(r3.Contains("bca"));
            Assert.IsTrue(r3.Contains("c"));
            Assert.IsTrue(r3.Contains("ca"));
            Assert.IsTrue(r3.Contains("cb"));
            Assert.IsTrue(r3.Contains("cab"));
            Assert.IsTrue(r3.Contains("cba"));





        }

        [TestMethod]
        public void TestConstructorSequences()
        {
            DerivedClass dv = new DerivedClass();
            Assert.AreEqual(dv.Message, ">>BaseClass>>DerivedClass");
        }
    }
}
