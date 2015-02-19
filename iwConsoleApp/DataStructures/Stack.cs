using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.DataStructures
{
    public class StackNode 
    {
        public string Data { get; set; }
        public StackNode(string data)
        {
            Data = data;
        }
        public StackNode Next { get; set; }

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

    public class Stack
    {
        public StackNode Head { get; set; }

        public void Push(StackNode node)
        {
            node.Next = Head;
            Head = node;
        }

        public StackNode Pop()
        {
            if (Head == null)
                return null;

            StackNode node = Head;
            node.Next = null;

            Head = Head.Next;
            return node;
        }

    }
}
