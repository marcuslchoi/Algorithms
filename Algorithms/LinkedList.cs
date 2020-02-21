using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;

namespace Algorithms
{
    public class DoublyLinkedList
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }
        public int Length { get; set; }

        //each link has an int value, and points to another link
        public DoublyLinkedList(int val)
        {
            this.Head = new Node(val);
            this.Tail = this.Head;
            this.Length = 1;
        }

        //assumes singly linked list, lists sorted
        public Node MergeTwoLists(Node l1, Node l2)
        {
            //dumb cases
            if (l1 == null)
                return l2;
            else if (l2 == null)
                return l1;

            Node smallerHead;
            Node temp;

            if (l1.Data < l2.Data)
            {
                smallerHead = l1;
                temp = l2;
            }
            else
            {
                smallerHead = l2;
                temp = l1;
            }

            Node currentNode = smallerHead;

            while (currentNode != null)
            {
                var next = currentNode.Next;
                if (next == null)
                {
                    break;
                }

                if (next.Data <= temp.Data)
                {
                    currentNode = currentNode.Next;
                }
                else //assign temp to currentNode's next
                {
                    currentNode.Next = temp;
                    temp = next;
                }
            }
            currentNode.Next = temp;
            return smallerHead;
        }

        public void Append(int val)
        {
            var newNode = new Node(val);
            var oldTail = this.Tail;
            oldTail.Next = newNode;
            newNode.Previous = oldTail;
            this.Tail = newNode;
            this.Length++;
        }

        public void Prepend(int val)
        {
            var newNode = new Node(val);
            var oldHead = this.Head;
            newNode.Next = oldHead;
            oldHead.Previous = newNode;
            this.Head = newNode;
            this.Length++;
        }

        public void PrintList()
        {
            Node currentNode = this.Head;
            var theList = new List<int>();
            while (currentNode != null)
            {
                theList.Add(currentNode.Data);
                currentNode = currentNode.Next;
            }
            Debug.WriteLine("printing linked list");
            foreach (var val in theList)
            {
                Debug.Write(val + ", ");
            }
            Debug.WriteLine(" ");
        }

        public void PrintHeadAndTailVals()
        {
            Debug.WriteLine("Head, Tail: " + this.Head.Data + ", " + this.Tail.Data);
        }

        public void Insert(int index, int val)
        {
            if (index >= this.Length || index <= 0)
            {
                return;
            }

            var nodeBeforeIndex = this.GetNode(index - 1);
            var nodeAtIndex = nodeBeforeIndex.Next;
            var newNode = new Node(val);
            newNode.Previous = nodeBeforeIndex;
            nodeBeforeIndex.Next = newNode;
            newNode.Next = nodeAtIndex;
            nodeAtIndex.Previous = newNode;

            this.Length++;
        }

        private Node GetNode(int index)
        {
            if (index < 0 || index >= this.Length)
            {
                return null;
            }
            bool goForward = true;// index < this.Length / 2;

            Node nodeAtIndex;
            if (goForward)
            {
                nodeAtIndex = this.Head;
                for (int i = 0; i < index; i++)
                {
                    nodeAtIndex = nodeAtIndex.Next;
                }
            }
            else
            {
                nodeAtIndex = this.Tail;
                for (int i = this.Length - 1; i > index; i--)
                {
                    nodeAtIndex = nodeAtIndex.Previous;
                }
            }

            return nodeAtIndex;
        }

        public void Remove(int index)
        {

            var nodeAtIndex = this.GetNode(index);
            Node nodeBeforeIndex = null;
            if (nodeAtIndex != this.Head)
            {
                nodeBeforeIndex = this.GetNode(index - 1);
            }
            else
            {
                //removing head
                this.Head = nodeAtIndex.Next;
                return;
            }

            var nodeAfterIndex = nodeAtIndex.Next;
            if (nodeAfterIndex != null)
            {
                nodeBeforeIndex.Next = nodeAfterIndex;
                nodeAfterIndex.Previous = nodeBeforeIndex;
            }
            else
            {
                //removing tail
                this.Tail = nodeBeforeIndex;
                nodeBeforeIndex.Next = null;
            }

            this.Length--;
        }

        //recursive
        //https://www.youtube.com/watch?v=K7J3nCeRC80&list=PL2_aWCzGMAwI3W_JlcBbtYTwiQSsOTa6P&index=10
        public void ReversePrintR(Node node)
        {
            if (node == null)
            {
                return;
            }
            this.ReversePrintR(node.Next);
            Debug.WriteLine("reverse print: " + node.Data);
        }

        //recursive
        //https://www.youtube.com/watch?v=KYH83T4q6Vs
        public void ReverseR(Node p)
        {
            //old tail becomes head
            //if (p.Next == null) //if tail is not set
            if(p == this.Tail)
            {
                this.Head = p;
                return;
            }

            this.ReverseR(p.Next);
            var q = p.Next;
            q.Next = p;
            p.Next = null; //p is the new temp tail
            this.Tail = p; //optional
        }

        public void Reverse()
        {
            //go in reverse, make current point to previous
            for (int i = this.Length - 1; i > 0; i--)
            {
                var curr = this.GetNode(i);
                var prev = this.GetNode(i - 1);
                curr.Next = prev;
            }

            //swap head and tail, make sure tail's next is null
            var oldHead = this.Head;
            this.Head = this.Tail;
            this.Tail = oldHead;
            this.Tail.Next = null;
        }
    }
}
