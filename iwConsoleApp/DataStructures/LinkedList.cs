using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.DataStructures
{
    public class ListNode
    {
        public string Data { get; set; }
        public ListNode Next { get; set; }
        public ListNode(string data)
        {
            Data = data;
        }
        public void AppendToLast(ListNode node)
        {
            var p = this;

            while (p.Next != null)
            {
                p = p.Next;
            }
            p.Next = node;
        }

        public void AppendToLast(string data)
        {
            AppendToLast(new ListNode(data));
        }


        /// <summary>
        /// skips n nodes. If there are more than n nodes ahead, skips to the last one
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public ListNode Skip(int n)
        {
            int i = 0;
            ListNode p = this;
            while (p.Next != null && i++ < n){
                p = p.Next;
            }
            return p;
        }

        public override string ToString()
        {
            var p = this;
            string output = p.Data;
            while (p.Next != null)
            {
                p = p.Next;
                output += " -> " + p.Data;
            }
            return output;
        }
    }

    public class LinkedList
    {
        /// <summary>
        /// Write code to remove duplicates from an unsorted linked list
        /// </summary>
        /// <param name="list"></param>
        public void RemoveDuplicates(ListNode list)
        {
            var p = list;
            while (p.Next != null)
            {
                var subp = p;
                var data = p.Data;
                while (subp.Next != null)
                {
                    if (subp.Next.Data == data)
                    {
                        // remove dupe
                        subp.Next = subp.Next.Next;
                    }
                    else
                        subp = subp.Next;
                }
                p = p.Next;
            }
        }


        /// <summary>
        /// Implement an algorithm to find the nth to last element of a singly linked list
        /// </summary>
        /// <param name="list"></param>
        public ListNode FindNthToLast(ListNode list, int n)
        {

            if (n < 0)
                throw new ArgumentException("must be >= 0");

            var p1 = list;
            ListNode p2 = null;
            
            while (p1 != null)
            {
                p2 = p1.Skip(n);
                if (p2.Next == null)
                    return p1;
                p1 = p1.Next;
            }
            return null ;
        }
        /// <summary>
        /// You have two numbers represented by a linked list, 
        /// where each node contains a sin- 
        /// gle digit The digits are stored in reverse order, 
        /// such that the 1’s digit is at the head of the list 
        /// Write a function that adds the two numbers and returns the sum as a linked list
        /// EXAMPLE
        /// Input: (3 -> 1 -> 5), (5 -> 9 -> 2) Output: 8 -> 0 -> 8
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public ListNode AddOps(ListNode op1, ListNode op2)
        {
            var p1 = op1;
            var p2 = op2;
            int carry = 0;
            ListNode result = null;
            while (p1 != null || p2 != null)
            {
                int r1 = 0;
                if (p1 != null)
                    r1 = Convert.ToInt32(p1.Data);

                int r2 = 0;
                if (p2 != null)
                    r2 = Convert.ToInt32(p2.Data);
                int r = r1 + r2 + carry;
                if (result == null)
                    result = new ListNode((r % 10).ToString());
                else
                    result.AppendToLast((r % 10).ToString());
                if (r >= 10)
                    carry = r/10;
                else
                    carry = 0;
                if (p1 != null)
                    p1 = p1.Next;
                if (p2 != null)
                    p2 = p2.Next;
            }
            if (carry > 0)
                result.AppendToLast(carry.ToString());

            return result;
        }
    }
}
