using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp
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
                throw new ArgumentException("must be >= 1");

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
    }
}
