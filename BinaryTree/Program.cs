using System;

namespace BinaryTree
{
    class Node
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

    class binaryTree
    {
        public Node top;
        public binaryTree() { top = null; }

        public void AddElement(ref Node head, int data)
        {
            if (head == null)
                head = new Node(data);
            else if (data < head.Data)
                AddElement(ref head.Left, data);
            else
                AddElement(ref head.Right, data);
        }

        public void DeleteElement(Node head, int data)
        {
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
        public void FindElement(Node head,int data)
        {
            if (head == null)
                Console.WriteLine($"Element {data} does not exists");
            else if (head.Data == data)
                Console.WriteLine($"Element {data} exists");
            else if (data < head.Data)
                FindElement(head.Left, data);
            else
                FindElement(head.Right, data);
        }

    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            binaryTree bt = new binaryTree();
            // Adding elements to tree
            bt.AddElement(ref bt.top,5);
            bt.AddElement(ref bt.top,10);
            bt.AddElement(ref bt.top,20);
            bt.AddElement(ref bt.top,4);
            bt.AddElement(ref bt.top,9);
            bt.AddElement(ref bt.top,1);
            bt.AddElement(ref bt.top,7);

            // Searching for given element in tree
            bt.FindElement(bt.top,7);

            Console.WriteLine("\n\nIn Order Traversal (Left, Root, Right) ");
            InOrderTraversal(ref bt.top);
            Console.WriteLine("\n\nPre Order Traversal (Root, Left, Right) ");
            PreOrderTraversal(ref bt.top);
            Console.WriteLine("\n\nPost Order Traversal (Left, Right, Root) ");
            PostOrderTraversal(ref bt.top);

            Console.ReadLine();
        }
        static void InOrderTraversal(ref Node current)
        {
            if (current == null)
                return;
            
            if (current.Left != null)
                InOrderTraversal(ref current.Left);

            Console.Write($" {current.Data}");

            if (current.Right != null)
                InOrderTraversal(ref current.Right);
        }

        static void PreOrderTraversal(ref Node current)
        {
            if (current == null)
                return;
            Console.Write($" {current.Data}");

            if (current.Left != null)
                PreOrderTraversal(ref current.Left);

            if (current.Right != null)
                PreOrderTraversal(ref current.Right);
        }
        static void PostOrderTraversal(ref Node current)
        {
            if (current == null)
                return;

            if (current.Left != null)
                PostOrderTraversal(ref current.Left);

            if (current.Right != null)
                PostOrderTraversal(ref current.Right);

            Console.Write($" {current.Data}");
        }
    }
}
