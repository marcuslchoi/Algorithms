using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AppKit;
using Foundation;

namespace Algorithms
{
    public partial class ViewController : NSViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public void TreeStuff()
        {
            var tree = new BinarySearchTree();
            tree.Insert(9);
            tree.Insert(4);
            tree.Insert(6);
            tree.Insert(20);
            tree.Insert(170);
            tree.Insert(15);
            tree.Insert(1);

            Debug.WriteLine(tree.MaxDepth(tree.root));
            //tree.Insert(100);
            //tree.Insert(50);
            //tree.Insert(70);
            //tree.Insert(40);
            //tree.Insert(43); //subtree
            //tree.Insert(42);
            //tree.Insert(44);
            //tree.Lookup(6);
            //tree.Remove(20);
            //var queue = new Queue<TreeNode>();
            //queue.Enqueue(tree.root);
            //var values = tree.BfsRecursive(queue, new List<int>());

            var math = new MyMath();
            math.MyPow(23.4, -6);

            var values = tree.DFSInOrder();
            foreach (var num in values)
            {
                Debug.Write(num + " ");
            }


            //        9
            //   4        20
            // 1   6   15    170

            //DFS

            //left, data, right: data is the parent node
            //ldr
            //InOrder - 1,4,5,9,15,20,170
            //dlr
            //PreOrder - 9,4,1,6,20,15,170
            //lrd
            //PostOrder - 1,6,4,15,20,170,9


        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var leet = new LeetcodeApp();
            var list = new DoublyLinkedList(1);
            list.Append(2);
            list.Append(3);
            list.Append(5);
            list.Append(7);
            var node = LeetcodeApp.FindKthToLastNode(list, 4);

            var r = 2;
            var c = 3;
            var maze = new int[2, 3] { { 1,2,3},{4,5,6 }};
            var numways = LeetcodeApp.WaysToNavigateMaze(maze, r, c);

            //var permutations = leet.Permute(new int[] { 1, 2, 3 }, null);
            var perms = leet.GetPerms("123");
            LeetcodeApp.NextPermutation(new int[] { 3,2,1});

            Debug.WriteLine("kth: " + LeetcodeApp.KthGrammar(4,1));

            var str = "aeifjlkjaasdfsdytvka";
            Debug.WriteLine("longest substring of "+str+": "+LeetcodeApp.LengthOfLongestSubstring(str));

            var list1 = new DoublyLinkedList(4);
            list1.Append(6);
            list1.Append(7);
            list1.Append(8);

            var list2 = new DoublyLinkedList(7);
            list2.Append(2);
            list2.Append(9);
            list2.Append(8);
            list2.Append(4);
            list2.Append(3);

            var sum = LeetcodeApp.AddTwoNumbersR(list1.Head, list2.Head);

            var arr = new int[] { 3, 5, -1, 8, 12 };
            //Debug.WriteLine(SortingOps.ArrayAddition(arr));

            //var math = new MyMath();
            //Debug.WriteLine(math.GetFibonacciNumber(7));
            //Debug.WriteLine(GetFibonacciNumber(5));

            //Debug.WriteLine(StringOperations.ReverseStringRecursive("fokie a doogie"));

            //var nums = new List<int> { 0,3,2,8,5,9,6,4,7,1,8 };
            //var numsSorted = SortingOps.MergeSort(nums);
            //foreach (int num in numsSorted)
            //{
            //    Debug.WriteLine(num);
            //}
            TreeStuff();
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
    }
}
