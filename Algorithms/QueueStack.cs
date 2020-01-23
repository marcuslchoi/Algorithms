using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
//using AppKit;
//using Foundation;

namespace Algorithms
{
    public class QueueStack //queue built with stacks
    {
        Stack stack1 = new Stack();
        Stack stack2 = new Stack();

        public void Enqueue(int val)
        {
            if (stack1.IsEmpty())
            {
                this.MoveFromStackToStack(stack2, stack1);
            }
            stack1.Push(val);
        }

        private void MoveFromStackToStack(Stack fromStack, Stack toStack)
        {
            while (fromStack.Peek() != null)
            {
                var topOfFromStack = fromStack.Peek();
                toStack.Push(topOfFromStack.Data);
                fromStack.Pop();
            }
        }

        public void Dequeue()
        {
            if (stack2.IsEmpty())
            {
                this.MoveFromStackToStack(stack1, stack2);
            }
            stack2.Pop();
        }

        public void Peek()
        {
            if (stack2.IsEmpty())
            {
                this.MoveFromStackToStack(stack1, stack2);
            }
            stack2.Peek();
        }

        public void PrintList()
        {
            if (stack2.IsEmpty())
            {
                MoveFromStackToStack(stack1, stack2);
            }

            Node currentNode = stack2.top;
            var theList = new List<int>();
            while (currentNode != null)
            {
                theList.Add(currentNode.Data);
                currentNode = currentNode.Next;
            }
            foreach (var val in theList)
            {
                Debug.Write(val + ", ");
            }
            Debug.WriteLine(" ");
        }
    }

    public class Queue
    {
        Node first;
        Node last;
        int length;

        public void PrintList()
        {
            Node currentNode = this.first;
            var theList = new List<int>();
            while (currentNode != null)
            {
                theList.Add(currentNode.Data);
                currentNode = currentNode.Next;
            }
            foreach (var val in theList)
            {
                Debug.Write(val + ", ");
            }
            Debug.WriteLine(" ");
        }

        public void Peek()
        {
            Debug.WriteLine(this.first.Data);
        }

        public void Enqueue(int val)
        {
            var newNode = new Node(val);
            if (length == 0)
            {
                first = newNode;
            }
            else
            {
                var oldLast = this.last;
                oldLast.Next = newNode;
            }
            last = newNode;
            this.length++;
        }

        public void Dequeue()
        {
            if (length == 0)
            {
                return;
            }

            if (length == 1)
            {
                this.first = null;
                this.last = null;
            }
            else //if (length > 0)
            {
                var newFirst = this.first.Next;
                //this.first.Next = null;
                this.first = newFirst;
            }
            length--;
        }
    }

    public class StackArr
    {
        //private Node top;
        //private Node bottom;
        private List<int> list;
        private int length;

        public void Push(int val)
        {
            if (this.length == 0)
            {
                this.list = new List<int>();
            }
            list.Add(val);
            this.length++;
        }

        public void Pop()
        {
            if (this.length > 0)
            {
                list.RemoveAt(this.length - 1);
                this.length--;
            }
        }

        public void Peek()
        {
            Debug.WriteLine(list.Last());
        }

    }

    public class Stack
    {
        public Node top;
        public Node bottom;
        private int length;

        public bool IsEmpty()
        {
            return this.length == 0;
        }

        public void Push(int val)
        {
            var newNode = new Node(val);

            if (this.length == 0)
            {
                this.top = newNode;
                this.bottom = newNode;
            }
            else
            {
                var oldTop = this.top;
                this.top = newNode;
                newNode.Next = oldTop;
            }
            this.length++;
        }

        public void Pop()
        {
            if (length == 0)
            { return; }

            if (this.top == this.bottom)
            {
                this.bottom = null; //last value popped
            }

            var newTop = this.top.Next;
            //this.top.Next = null;
            this.top = newTop;
            this.length--;
        }

        public Node Peek()
        {
            if (this.top != null)
            {
                //Debug.WriteLine(this.top.Data);
            }
            return this.top;
        }

        public void PrintList()
        {
            Node currentNode = this.top;
            var theList = new List<int>();
            while (currentNode != null)
            {
                theList.Add(currentNode.Data);
                currentNode = currentNode.Next;
            }
            foreach (var val in theList)
            {
                Debug.Write(val + ", ");
            }
            Debug.WriteLine(" ");
        }
    }
}
