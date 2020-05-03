using System;

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
        public Node top;
        public BinarySearchTree() { top = null; }

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

        public void DeleteElement(ref Node head, int data)
        {
            throw new NotImplementedException();
            /* NOT IMPLEMENTED YET
            if (head == null)
                Console.WriteLine($"Element {data} doesnt exists");
            else if (head.Data == data)
            {
                //
            }
            else if (data < head.Data)
                DeleteElement(head.Left, data);
            else
                DeleteElement(head.Right, data);
                */
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            BinarySearchTree bt = new BinarySearchTree();
            // Adding elements to tree
            bt.AddElement(ref bt.top, 5);
            bt.AddElement(ref bt.top, 10);
            bt.AddElement(ref bt.top, 20);
            bt.AddElement(ref bt.top, 4);
            bt.AddElement(ref bt.top, 9);
            bt.AddElement(ref bt.top, 1);
            bt.AddElement(ref bt.top, 7);

            // Searching for given element in tree
            bt.CheckElementExists(bt.top, 7);
            // Find Node for given element in tree
            Node find = bt.FindElementNode(bt.top, 7);

            Console.WriteLine("\n\nIn Order Traversal (Left, Root, Right) ");
            DFS.InOrderTraversal(bt.top);
            Console.WriteLine("\n\nPre Order Traversal (Root, Left, Right) ");
            DFS.PreOrderTraversal(bt.top);
            Console.WriteLine("\n\nPost Order Traversal (Left, Right, Root) ");
            DFS.PostOrderTraversal(bt.top);

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
}
