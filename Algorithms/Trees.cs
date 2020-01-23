using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;

namespace Algorithms
{
    public class BinarySearchTree
    {
        public TreeNode root;

        public void Remove(int val)
        {

            //get current node and parent node
            var currentNode = this.root;
            TreeNode parentNode = null;
            while (true)
            {
                bool smaller = currentNode.Data > val;
                bool bigger = currentNode.Data < val;

                if (smaller)
                {
                    parentNode = currentNode;
                    currentNode = currentNode.Left;
                }
                else if (bigger)
                {
                    parentNode = currentNode;
                    currentNode = currentNode.Right;
                }
                else
                {
                    //found
                    break;
                }
            }

            bool hasLeftChild = currentNode.Left != null;
            bool hasRightChild = currentNode.Right != null;

            bool parentNodeIsNull = parentNode == null; //todo removing root

            bool isParentLeft = currentNode.Data < parentNode.Data;
            //bool isParentRight = currentNode.Data > parentNode.Data;

            //node is a leaf
            if (!hasLeftChild && !hasRightChild)
            {
                if (isParentLeft)
                {
                    parentNode.Left = null;
                }
                else
                {
                    parentNode.Right = null;
                }
            }
            else if (hasLeftChild && hasRightChild) //node has 2 children
            {
                //leftmost of right child's children takes deleted node's place
                //leftmost's parent, left and right becomes deleted node's parent, left and right
                //leftmost's subtree becomes its parent's left subtree

                var leftChild = currentNode.Left;
                var rightChild = currentNode.Right;
                TreeNode leftMostParent = null;
                TreeNode leftmost = null;

                bool findingLeftmost = rightChild.Left != null;

                //get the leftmost and its parent
                if (findingLeftmost)
                {
                    leftMostParent = rightChild;
                    leftmost = rightChild.Left;
                    while (true)
                    {
                        if (leftmost.Left != null)
                        {
                            leftMostParent = leftmost;
                            leftmost = leftmost.Left;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    leftmost = rightChild;
                }

                //set parent
                if (isParentLeft)
                {
                    parentNode.Left = leftmost;
                }
                else
                {
                    parentNode.Right = leftmost;
                }

                //get leftmost's subtree
                var leftmostSubtreeTop = leftmost.Right;

                //set children
                leftmost.Left = leftChild;
                leftmost.Right = rightChild;

                //cut old connection, replace with leftmost's subtree
                if (leftMostParent != null)
                {
                    leftMostParent.Left = leftmostSubtreeTop;
                }
            }
            else //node has only 1 child
            {
                var currentChild = hasLeftChild ? currentNode.Left : currentNode.Right;
                if (isParentLeft)
                {
                    //currentNode's child becomes parent's left child
                    parentNode.Left = currentChild;
                }
                else
                {
                    //currentNode's child becomes parent's right child
                    parentNode.Right = currentChild;
                }
            }
        }

        public void Insert(int val)
        {
            var newNode = new TreeNode(val);
            if (root == null)
            {
                root = newNode;
            }
            else
            {
                var currentNode = this.root;
                while (currentNode != null)
                {
                    var right = currentNode.Right;
                    var left = currentNode.Left;
                    if (val > currentNode.Data)
                    {
                        if (right == null)
                        {
                            currentNode.Right = newNode;
                            break;
                        }
                        else
                        {
                            currentNode = right;
                        }
                    }
                    else if (val < currentNode.Data)
                    {
                        if (left == null)
                        {
                            currentNode.Left = newNode;
                            break;
                        }
                        else
                        {
                            currentNode = left;
                        }
                    }
                    else
                    {
                        //node already exists!!
                        break;
                    }
                }
            }
        }

        public TreeNode Lookup(int val)
        {
            Debug.WriteLine("looking up value " + val);
            var currentNode = this.root;
            var currentRow = 1;
            while (currentNode != null)
            {
                var right = currentNode.Right;
                var left = currentNode.Left;
                if (val > currentNode.Data)
                {
                    currentRow++;
                    currentNode = right;
                }
                else if (val < currentNode.Data)
                {
                    currentRow++;
                    currentNode = left;
                }
                else
                {
                    Debug.WriteLine("Value is " + currentNode.Data + ", row is " + currentRow);
                    return currentNode;
                }
            }
            return null;
        }

        public List<int> BreadthFirstSearch()
        {
            
            var nodeQueue = new Queue<TreeNode>();
            nodeQueue.Enqueue(this.root);
            var values = new List<int>();
            //return this.BreadthFirstSearch(nodeQueue, values);
            return this.BfsRecursive(nodeQueue, values);
        }

        //use a queue to hold nodes in order
        public List<int> BreadthFirstSearch(Queue<TreeNode> nodeQueue, List<int> values)
        {
            //var currentNode = this.root;
            //var nodeQueue = new Queue<TreeNode>();
            //var values = new List<int>();

            //nodeQueue.Enqueue(currentNode);

            while (nodeQueue.Count > 0)
            {
                var currentNode = nodeQueue.Dequeue();
                values.Add(currentNode.Data);
                if (currentNode.Left != null)
                {
                    nodeQueue.Enqueue(currentNode.Left);
                }

                if (currentNode.Right != null)
                {
                    nodeQueue.Enqueue(currentNode.Right);
                }
            }
            //foreach (var num in values)
            //{
            //    Debug.Write(num+" ");
            //}
            return values;
        }

        public List<int> BfsRecursive(Queue<TreeNode> nodeQueue, List<int> values)
        {
            if (nodeQueue.Count == 0)
            {
                return values;
            }

            //var values = new List<int>();
            var firstNode = nodeQueue.Dequeue();

            if (firstNode.Left != null || firstNode.Right != null)
            {
                this.currentDepth++;
                if (firstNode.Left != null)
                {
                    nodeQueue.Enqueue(firstNode.Left);
                }

                if (firstNode.Right != null)
                {
                    nodeQueue.Enqueue(firstNode.Right);
                }
            }

            var firstVal = firstNode.Data;
            values.Add(firstVal);
            return BfsRecursive(nodeQueue, values);

            //        9
            //   4        20
            // 1   6   15    170
        }

        //recursive max depth of tree
        public int MaxDepthR(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }
            var leftMax = MaxDepthR(root.Left);
            var rightMax = MaxDepthR(root.Right);
            var maxDepth = Math.Max(leftMax, rightMax) + 1;
            return maxDepth;
        }

        public int MaxDepth(TreeNode root)
        {
            if (root == null) { return 0; }
            var nodeQueue = new Queue<TreeNode>();
            nodeQueue.Enqueue(root);

            int lastNodeCount = nodeQueue.Count;
            int depth = 0;

            while (nodeQueue.Count > 0)
            {
                depth++;
                int currentNodeCount = 0;
                //for each depth level, add the node's children to the queue
                //and remove the parent nodes from the queue
                for (int i = 0; i < lastNodeCount; i++)
                {
                    var currNode = nodeQueue.Dequeue();
                    var left = currNode.Left;
                    var right = currNode.Right;
                    if (left != null)
                    {
                        nodeQueue.Enqueue(left);
                        currentNodeCount++;
                    }
                    if (right != null)
                    {
                        nodeQueue.Enqueue(right);
                        currentNodeCount++;
                    }
                }
                lastNodeCount = currentNodeCount;
            }
            return depth;
        }

        public List<int> DFSInOrder()
        {
            return this.TraverseInOrder(this.root, new List<int>());
        }

        int currentDepth = 1;
        private List<int> TraverseInOrder(TreeNode node, List<int> nums)
        {
            if (node.Left != null)
            {
                this.TraverseInOrder(node.Left, nums);
            }

            nums.Add(node.Data);
            
            if (node.Right != null)
            {
                this.TraverseInOrder(node.Right, nums);
            }
            //nums.Add(node.Data);
            return nums;
        }

        public List<int> DFSPreOrder()
        {
            return this.TraversePreOrder(this.root, new List<int>());
        }

        private List<int> TraversePreOrder(TreeNode node, List<int> nums)
        {
            return null;
        }

        public List<int> DFSPostOrder()
        {
            return this.TraversePostOrder(this.root, new List<int>());
        }

        private List<int> TraversePostOrder(TreeNode node, List<int> nums)
        {
            return null;
        }

        public void Traverse()
        {
            //todo use recursion
        }
    }
}
