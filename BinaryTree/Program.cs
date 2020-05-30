using System;
using System.Collections.Generic;

namespace Tree
{
    // GFG https://www.geeksforgeeks.org/binary-search-tree-set-1-search-and-insertion/
    public class Node
    {
        public int Data { get; set; }
        public Node Left;
        public Node Right;

        public Node(int no)
        {
            Data = no;
            Left = Right = null;
        }
    }

    public class BinarySearchTree
    {
        public Node Top;
        public BinarySearchTree() { Top = null; }

        public void AddElement(ref Node head, int data)
        {
            if (head == null)
                head = new Node(data);
            else if (data < head.Data)
                AddElement(ref head.Left, data);
            else
                AddElement(ref head.Right, data);
        }

        public void CheckElementExists(Node head, int data)
        {
            if (head == null)
                Console.WriteLine($"Element {data} doesn't exists");
            else if (head.Data == data)
                Console.WriteLine($"Element {data} Found");
            else if (data < head.Data)
                CheckElementExists(head.Left, data);
            else if (data > head.Data)
                CheckElementExists(head.Right, data);
        }

        public Node FindElementNode(Node head, int data)
        {
            Node elementFoundAtNode = null;
            if (head == null)
                Console.WriteLine($"Element {data} does not exists");
            else if (head.Data == data)
            {
                Console.WriteLine($"Element {data} exists");
                elementFoundAtNode = head;
            }
            else if (data < head.Data)
                elementFoundAtNode = FindElementNode(head.Left, data);
            else
                elementFoundAtNode = FindElementNode(head.Right, data);

            return elementFoundAtNode;
        }

        public Node DeleteElement(ref Node head, int data)
        {
            if (head == null)
            {
                Console.WriteLine($"Element {data} doesnt exists");
                return head;
            }
            else if (head.Data == data)
            {
                if (head.Left == null)
                    return head.Right;          // as left node is null, return right which make be an node or null either way parent node is removed
                else if (head.Right == null)
                    return head.Left;           // as right is null return left node which may have an value or node either way parents node gets deleted.
                else
                {   // replace the node data with its inorder successor and call DeleteElement() on node being copied.
                    head.Data = FindInOrderSuccessor(head.Right);
                    head.Right = DeleteElement(ref head.Right, head.Data);
                    return head;
                }
            }
            else if (data < head.Data)
            {
                head.Left = DeleteElement(ref head.Left, data);
                return head;
            }
            else
            {
                head.Right = DeleteElement(ref head.Right, data);
                return head;
            }
        }

        /// <summary>
        /// Element which comes right after, in InOrderTraversal (one on immediate right)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int FindInOrderSuccessor(Node head)
        {
            return head.Left != null ? FindInOrderSuccessor(head.Left) : head.Data;
        }
        /// <summary>
        /// Element which comes right before, in InOrderTraversal (one on immediate left)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int FindInOrderPredeccessor(Node head)
        {
            return head.Right != null ? FindInOrderPredeccessor(head.Right) : head.Data;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            BinarySearchTree bt = new BinarySearchTree();
            // Adding elements to tree
            bt.AddElement(ref bt.Top, 5);
            bt.AddElement(ref bt.Top, 10);
            bt.AddElement(ref bt.Top, 20);
            bt.AddElement(ref bt.Top, 4);
            bt.AddElement(ref bt.Top, 9);
            bt.AddElement(ref bt.Top, 1);
            bt.AddElement(ref bt.Top, 7);
            BFS.BreadthFirstTraversal(bt.Top);

            // Searching for given element in tree
            bt.CheckElementExists(bt.Top, 7);
            // Find Node for given element in tree
            Node find = bt.FindElementNode(bt.Top, 7);

            Console.WriteLine("\n\nIn Order Traversal (Left, Root, Right) ");
            DFS.InOrderTraversal(bt.Top);
            Console.WriteLine("\n\nPre Order Traversal (Root, Left, Right) ");
            DFS.PreOrderTraversal(bt.Top);
            Console.WriteLine("\n\nPost Order Traversal (Left, Right, Root) ");
            DFS.PostOrderTraversal(bt.Top);

            Console.WriteLine("\n\n");
            bt.DeleteElement(ref bt.Top, 5);
            DFS.InOrderTraversal(bt.Top);
            BFS.LevelOrderTraversal(bt.Top);
            Console.ReadLine();
        }
    }

    public static class DFS
    {
        /// <summary>
        /// Left || Root || Right
        /// </summary>
        /// <param name="current"></param>
        public static void InOrderTraversal(Node current)
        {
            if (current == null)
                return;
            
            if (current.Left != null)
                InOrderTraversal(current.Left);

            Console.Write($" {current.Data}");

            if (current.Right != null)
                InOrderTraversal(current.Right);
        }
        /// <summary>
        /// Root || Left || Right
        /// </summary>
        /// <param name="current"></param>
        public static void PreOrderTraversal(Node current)
        {
            if (current == null)
                return;
            Console.Write($" {current.Data}");

            if (current.Left != null)
                PreOrderTraversal(current.Left);

            if (current.Right != null)
                PreOrderTraversal(current.Right);
        }
        /// <summary>
        /// Left || Right || Root
        /// </summary>
        /// <param name="current"></param>
        public static void PostOrderTraversal(Node current)
        {
            if (current == null)
                return;

            if (current.Left != null)
                PostOrderTraversal(current.Left);

            if (current.Right != null)
                PostOrderTraversal(current.Right);

            Console.Write($" {current.Data}");
        }
    }

    /// <summary>
    /// https://www.geeksforgeeks.org/level-order-tree-traversal/ Breadth First Traversal or Level Order Tree Traversal
    /// Using Queue's || Time Complexcity O(n)
    /// </summary>
    public static class BFS
    {
        public static void LevelOrderTraversal(Node current)
        {
            //Step1 : Create empty Queue
            Queue<Node> q = new Queue<Node>();
            
            //Step2: assign root node to temp variable
            Node temp = current;

            Console.WriteLine("\n Breadth First Traversal/Level Order Traversal");

            //Step3: loop until temp==null
            while(temp!=null)
            {
                Console.Write($" {temp.Data}");                                                  // Print parent node data
                if (temp.Left != null)
                    q.Enqueue(temp.Left);                                                       // Push Left child in Queue
                if (temp.Right != null)
                    q.Enqueue(temp.Right);                                                      // Push Right child in Queue
                temp = q.Count > 0 ? q.Dequeue() : null;
            }
        }

        // Same as above method LevelOrderTraversal() with minor difference in while loop
        public static void BreadthFirstTraversal(Node current)
        {
            if (current == null) return;

            //Step1 : Create empty Queue
            Queue<Node> q = new Queue<Node>();

            //Step2: add root node to Queue
            q.Enqueue(current);
            
            Console.WriteLine("\n Breadth First Traversal/Level Order Traversal");
            //Step3: loop until queue is not empty
            while (q.Count >0)
            {
                Node temp = q.Dequeue();
                Console.Write($" {temp.Data}");                                                  // Print parent node data
                if (temp.Left != null)
                    q.Enqueue(temp.Left);                                                       // Push Left child in Queue
                if (temp.Right != null)
                    q.Enqueue(temp.Right);                                                      // Push Right child in Queue
            }
        }
    }
}
