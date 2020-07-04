using System;
using System.Collections.Generic;
using System.Security;

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
                Console.WriteLine($"Node with data :\t{data} is Deleted/Moved");
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
        public int FindInOrderSuccessor(Node head) => head.Left != null ? FindInOrderSuccessor(head.Left) : head.Data;

        /// <summary>
        /// Element which comes right before, in InOrderTraversal (one on immediate left)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int FindInOrderPredeccessor(Node head) => head.Right != null ? FindInOrderPredeccessor(head.Right) : head.Data;

        public int SizeOfTree(Node head)
        {
            if (head == null) return 0;
            int size = 1;                       // Since Head is not null
            size += SizeOfTree(head.Left);      // add size of left tree
            size += SizeOfTree(head.Right);     // add size of right tree
            return size;
        }

        public int HeightOfTree(Node head)
        {
            if (head == null) return-1;
            int ht = 0;                         // Tree with one node has height of 0
            int left = HeightOfTree(head.Left)+1;
            int right = HeightOfTree(head.Right)+1;
            ht = left > right ? left : right;
            return ht;
        }

        public int LevelWithMaxSum(ref int max,Node head)
        {
            int level = -1;
            if (head == null) return level;

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(head);
            Node n = null;
            int currLevel = -1;
            while (q.Count>0)
            {
                currLevel++;
                q.Enqueue(n);   // adding null Node to mark the end of the level
                int currentMax = 0;
                while (q.Count >0)  // Traversing at Level 'n'
                {
                    Node curr = q.Dequeue();
                    if (curr == null)
                        break;

                    currentMax += curr.Data;
                    if (curr.Left != null)
                        q.Enqueue(curr.Left);
                    if (curr.Right != null)
                        q.Enqueue(curr.Right);
                    
                }
                if (max < currentMax)
                {
                    level = currLevel;
                    max = currentMax;
                }
            }
            return level;
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

            Console.WriteLine();
            BFS.BreadthFirstTraversal(bt.Top);
            bt.DeleteElement(ref bt.Top, 5);
            BFS.LevelOrderTraversal(bt.Top);
            Console.WriteLine($"\n Size of Tree : '{bt.SizeOfTree(bt.Top)}'");
            Console.WriteLine($"\n Height of Tree : '{bt.HeightOfTree(bt.Top)}'");
            int getSum = -1;
            var level = bt.LevelWithMaxSum(ref getSum, bt.Top);
            Console.WriteLine($"\n The Level : {level} has the max sum : {getSum} in the Tree");
            Console.ReadKey();
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
            Console.WriteLine();
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
            Console.WriteLine();
        }
    }
}
