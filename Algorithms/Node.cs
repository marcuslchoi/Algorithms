using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;
namespace Algorithms
{
    public class Node
    {
        public int Data { get; set; }
        public Node Next { get; set; }
        public Node Previous { get; set; }

        public Node()
        {

        }

        public Node(int val)
        {
            this.Data = val;
            this.Next = null;
            this.Previous = null;
        }
    }

    public class TreeNode
    {
        public int Data { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int val)
        {
            this.Data = val;

        }
    }
}
