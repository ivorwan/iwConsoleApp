using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp
{
    public class ListNode
    {
        public object Data { get; set; }
        public ListNode Nex { get; set; }
    }

    public class LinkedList
    {
        public ListNode Root { get; set; }
        private ListNode _last;
    }
}
