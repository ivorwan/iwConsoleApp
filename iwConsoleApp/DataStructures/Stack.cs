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
    }

    public class Stack
    {
        public StackNode head { get; set; }

        public void Push(StackNode node)
        { }

        public StackNode Pop()
        {
            if (head == null)
                return null;

            head = head.Next;
            return head;
        }
    }
}
