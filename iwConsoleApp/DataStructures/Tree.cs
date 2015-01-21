using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iwConsoleApp.DataStructures
{

    public class TreeNode
    {
        public string Data;
        public TreeNode Left;
        public TreeNode Right;

        public TreeNode(string data)
        {
            Data = data;
        }
        public TreeNode(string data, TreeNode left, TreeNode right)
        {
            Data = data;
            AddChildren(left, right);
        }

        public void AddChildren(TreeNode left, TreeNode right) {
            Left = left;
            Right = right;
        }
    }

    public class Tree 
    {
        public TreeNode Root { get; set; }

        public Tree()
        {
            Root = new TreeNode("initialized with root");
        }
        public Tree(string data)
        {
            Root = new TreeNode(data);
        }
        public TreeNode DepthFirstSearch(TreeNode node, string data) {
            // starts with root
            Console.WriteLine("scanning ... " + node.Data);
            if (node.Data == data)
                return node;
            else {
                if (node.Left != null)
                {
                    var left = DepthFirstSearch(node.Left, data);
                    if (left != null)
                        return left;
                }

                if (node.Right != null)
                {

                    var right = DepthFirstSearch(node.Right, data);
                    if (right != null)
                        return right;
                }
            }
            return null;
        }
    }
}
