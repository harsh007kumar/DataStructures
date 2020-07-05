using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Security.Cryptography;

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
            if (head == null) return -1;
            int ht = 0;                         // Tree with one node has height of 0
            int left = HeightOfTree(head.Left) + 1;
            int right = HeightOfTree(head.Right) + 1;
            ht = left > right ? left : right;
            return ht;
        }

        public int LevelWithMaxSum(ref int max, Node head)
        {
            int level = -1;
            if (head == null) return level;

            Queue<Node> q = new Queue<Node>();
            q.Enqueue(head);
            Node nullNode = null;
            int currLevel = -1;
            while (q.Count > 0)                   // At First it might look like TIme complexity O(n*n) but we are only adding and removing each node max 1 time in Queue
            {
                currLevel++;                    // if elements are left in Queue increment current level by 1
                q.Enqueue(nullNode);            // adding null Node to mark the end of the last level
                int levelMax = 0;
                while (q.Count > 0)              // Traversing at Level 'n'
                {
                    Node curr = q.Dequeue();
                    if (curr == null)
                        break;

                    levelMax += curr.Data;
                    if (curr.Left != null)
                        q.Enqueue(curr.Left);   // add current Node->left child to Queue if it's not null
                    if (curr.Right != null)
                        q.Enqueue(curr.Right);  // add current Node->Right child to Queue if it's not null

                }
                if (levelMax > max)
                {
                    level = currLevel;
                    max = levelMax;
                }
            }
            return level;
        }

        /// <summary>
        /// Time Complexity is O(n) but requires 3 scans of the tree || Space Complexity O(n) for storing the Trace-path
        /// </summary>
        /// <param name="head"></param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        public static int FindLeastCommonAnscestor(Node head, int data1, int data2)
        {
            // Trace the path for both nodes from root to data
            Queue<int> q1 = new Queue<int>();
            Queue<int> q2 = new Queue<int>();

            if (head == null || !TracePathFromRoot(ref q1, head, data1) || !TracePathFromRoot(ref q2, head, data2))
            {
                Console.WriteLine("Can't Find LCA for pair of Nodes which don't exists in Tree");
                return -1;
            }

            
            var lastParent = q1.Dequeue();     // Storing the root Node
            q2.Dequeue();

            while (q1.Count > 0 && q2.Count > 0)
            {
                // Keep DeQueing the nodes and compare it they are same for both child than continue 
                // and if different last parent was the LCA for pair of Nodes
                var curParent1 = q1.Dequeue();
                var curParent2 = q2.Dequeue();
                if (curParent1 == curParent2)
                    lastParent = curParent1;
                else
                    break;
            }
            Console.WriteLine($" LCA for Node '{data1}' and '{data2}' is : '{lastParent}'");
            return lastParent;
        }

        /// <summary>
        /// Time Complexity O(n)
        /// </summary>
        /// <param name="st"></param>
        /// <param name="head"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool TracePathFromRoot(ref Queue<int> st, Node head, int data)
        {
            if (head == null) return false;

            bool found = false;
            st.Enqueue(head.Data);
            if (head.Data == data)
            {
                Console.WriteLine($" '{data}' Node found");
                found = true;
            }
            else if (data < head.Data)
                found = TracePathFromRoot(ref st, head.Left, data);
            else
                found = TracePathFromRoot(ref st, head.Right, data);
            return found;
        }

        /// <summary>
        /// Time Complexity O(n) same as FindLeastCommonAnscestor() but scans required is 1
        /// Space Complexity O(n) required for CallStack in System for recursion
        /// Assumption both Nodes are present in the tree
        /// </summary>
        /// <param name="head"></param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        public static int FindLCA_Recursive(Node head, int data1, int data2)
        {
            var LCA = -1;
            if (head == null) return LCA;                                        // Head is null return -1 to indicate no LCA exists
            LCA = head.Data;                                                    // Set LCA as root that is common anscestor for all nodes of tree
            if (data1 < head.Data && data2 < head.Data)
                LCA = FindLCA_Recursive(head.Left, data1, data2);               // Both child are in Left subtree
            else if (data1 > head.Data && data2 > head.Data)
                LCA = FindLCA_Recursive(head.Right, data1, data2);              // Both child are in Right subtree
            return LCA;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            BinarySearchTree bt = new BinarySearchTree();
            #region Adding elements to tree
            bt.AddElement(ref bt.Top, 5);
            bt.AddElement(ref bt.Top, 10);
            bt.AddElement(ref bt.Top, 20);
            bt.AddElement(ref bt.Top, 4);
            bt.AddElement(ref bt.Top, 9);
            bt.AddElement(ref bt.Top, 1);
            bt.AddElement(ref bt.Top, 7);
            #endregion
            // Level Order Traversal
            BFS.BreadthFirstTraversal(bt.Top);

            // Checking existance for given element in tree
            bt.CheckElementExists(bt.Top, 7);

            // Find and return Node for given element in tree
            Node find = bt.FindElementNode(bt.Top, 7);

            // Depth First Traversals
            Console.Write("\nIn Order Traversal (Left, Root, Right) :\t");
            DFS.InOrderTraversal(bt.Top);
            DFS.InOrderTraveral_Iterative(bt.Top);
            Console.Write("\nPre Order Traversal (Root, Left, Right) :\t");
            DFS.PreOrderTraversal(bt.Top);
            DFS.PreOrderTraveral_Iterative(bt.Top);
            Console.Write("\nPost Order Traversal (Left, Right, Root) :\t");
            DFS.PostOrderTraversal(bt.Top);
            DFS.PostOrderTraversal_Iterartive(bt.Top);


            Console.WriteLine();
            // Deleting a Node in Tree
            bt.DeleteElement(ref bt.Top, 5);

            BFS.LevelOrderTraversal(bt.Top);

            Console.WriteLine($"\n Size of Tree : '{bt.SizeOfTree(bt.Top)}'");
            Console.WriteLine($"\n Height of Tree : '{bt.HeightOfTree(bt.Top)}'");

            int getSum = -1, level = bt.LevelWithMaxSum(ref getSum, bt.Top);
            Console.WriteLine($"\n The Level : {level} has the max sum : {getSum} in the Tree");

            // Finding Least Common Anscestor for pair of Nodes in Tree
            int c1 = 10, c2 = 20;
            BinarySearchTree.FindLeastCommonAnscestor(bt.Top, c1, c2);
            //c1 = 4;
            Console.WriteLine($" LCA for '{c1}' and '{c2}' is : '{BinarySearchTree.FindLCA_Recursive(bt.Top, c1, c2)}'");


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
    
        public static void PreOrderTraveral_Iterative(Node current)
        {
            if (current == null) return;
            Stack<Node> st = new Stack<Node>();
            Console.Write("\nIterative Pre Order Traversal (Root, Left, Right) :\t");
            while (true)
            {
                while (current != null)
                {
                    Console.Write($" {current.Data}");
                    st.Push(current);
                    current = current.Left;
                }
                if (st.Count <= 0) break;
                current = st.Pop().Right;
            }
            Console.WriteLine();
        }

        public static void InOrderTraveral_Iterative(Node current)
        {
            if (current == null) return;
            Stack<Node> st = new Stack<Node>();
            Console.Write("\nIterative In Order Traversal (Left, Root, Right) :\t");
            while (true)
            {
                while (current != null)
                {
                    st.Push(current);
                    current = current.Left;
                }
                if (st.Count <= 0) break;
                Console.Write($" {st.Peek().Data}");
                current = st.Pop().Right;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Non-Recursive Postorder Traversal
        /// Karumanchi, Narasimha.Data Structures and Algorithms Made Easy: Data Structures and Algorithmic Puzzles(p. 239). Kindle Edition.
        /// </summary>
        /// <param name="current"></param>
        public static void PostOrderTraversal_Iterartive(Node current)
        {
            if (current == null) return;
            Stack<Node> st = new Stack<Node>();
            Console.Write("\nIterative Post Order Traversal (Left, Right, Root) :\t");
            Node prv = null;
            while (true)
            {
                while (current != null)
                {
                    st.Push(current);
                    current = current.Left;
                }
                while (current==null && st.Count>0)
                {
                    current = st.Peek();
                    if (current.Right == null || current.Right == prv)
                    {
                        Console.Write($" {current.Data}");
                        prv = st.Pop();
                        current = null;
                    }
                    else
                        current = current.Right;
                }
                if (st.Count <= 0) break;
            }
            Console.WriteLine();
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
