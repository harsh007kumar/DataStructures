﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
    public static class TreeUtility
    {
        /// <summary>
        /// Depth First Search Techniques for Binary Tree
        /// </summary>
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
                    while (current == null && st.Count > 0)
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
        /// Breadth First Search Technique for Binary Tree
        /// </summary>
        public static class BFS
        {
            public static void LevelOrderTraversal(Node current)
            {
                //Step1 : Create empty Queue
                Queue<Node> q = new Queue<Node>();

                //Step2: assign root node to temp variable
                Node temp = current;

                Console.Write("\n Breadth First Traversal/Level Order Traversal :\t");

                //Step3: loop until temp==null
                while (temp != null)
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

            /// <summary>
            /// Same as above method LevelOrderTraversal() with minor difference in while loop
            /// </summary>
            /// <param name="current"></param>
            public static void BreadthFirstTraversal(Node current)
            {
                if (current == null) return;

                //Step1 : Create empty Queue
                Queue<Node> q = new Queue<Node>();

                //Step2: add root node to Queue
                q.Enqueue(current);

                Console.Write("\n Breadth First Traversal/Level Order Traversal :\t");
                //Step3: loop until queue is not empty
                while (q.Count > 0)
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

            public static void LevelOrderReverse(Node root)
            {
                if (root == null) return;
                Queue<Node> q = new Queue<Node>();
                Stack<Node> st = new Stack<Node>();
                q.Enqueue(root);
                Console.Write(" Print the Nodes of the tree in reverse level order :\t");
                while(q.Count>0)
                {
                    var temp = q.Dequeue();
                    st.Push(temp);

                    if (temp.Left != null)
                        q.Enqueue(temp.Left);
                    if (temp.Right != null)
                        q.Enqueue(temp.Right);
                }
                while(st.Count>0)
                    Console.Write($" {st.Pop().Data}");
            }
        }




        /// <summary>
        /// Function which count no of items in left + right subtree + 1 and return TOTAL_NO_OF_NODES in Binary Tree
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int SizeOfTree(Node head) => (head == null) ? 0 : SizeOfTree(head.Left) + SizeOfTree(head.Right) + 1;

        public static int SizeOfTree_Iterative(Node root)
        {
            if (root == null) return 0;
            int size = 0;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while(q.Count>0)
            {
                var temp = q.Dequeue();
                size++;
                if (temp.Left != null)
                    q.Enqueue(temp.Left);
                if (temp.Right != null)
                    q.Enqueue(temp.Right);
            }
            return size;
        }

        /// <summary>
        /// Function which return height of binary tree (i.e, max depth of an leaf node in tree)
        /// Here, -1 indicats just Tree Empty
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int HeightOfTree(Node head)
        {
            if (head == null) return -1;
            int ht = 0;                         // Tree with one node has height of 0
            int left = HeightOfTree(head.Left) + 1;
            int right = HeightOfTree(head.Right) + 1;
            ht = left > right ? left : right;
            return ht;
        }

        /// <summary>
        /// Calculate the sum at each level in the tree and returns the Level and updates the max (ref variable with the actual max sum)
        /// </summary>
        /// <param name="max"></param>
        /// <param name="head"></param>
        /// <returns></returns>
        public static int LevelWithMaxSum(ref int max, Node head)
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

        public static void Print(string input) => Console.WriteLine($"\n========= {input} =========");

        /// <summary>
        /// return Binary Search Tree Object after adding couple of Nodes
        /// </summary>
        /// <returns></returns>
        public static BinarySearchTree GetBinaryTree()
        {
            var BST = new BinarySearchTree();
            BST.AddElement(ref BST.Top, 5);
            BST.AddElement(ref BST.Top, 10);
            BST.AddElement(ref BST.Top, 20);
            BST.AddElement(ref BST.Top, 4);
            BST.AddElement(ref BST.Top, 9);
            BST.AddElement(ref BST.Top, 1);
            BST.AddElement(ref BST.Top, 7);
            
            return BST;
        }

        /// <summary>
        /// Time Complexity O(n) || Space O(n) for system call stack
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxElementInBinaryTree_Recursive(Node root)
        {
            if (root == null) return -1;
            var left = MaxElementInBinaryTree_Recursive(root.Left);
            var right = MaxElementInBinaryTree_Recursive(root.Right);

            var maxChild = left > right ? left : right;
            return root.Data > maxChild ? root.Data : maxChild;
        }

        public static int MaxElementInBinaryTree_Iterative(Node root)
        {
            if (root == null) return -1;
            int max = -1;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while(q.Count>0)
            {
                var temp = q.Dequeue();
                max = max < temp.Data ? temp.Data : max;
                if(temp.Left!=null)
                    q.Enqueue(temp.Left);
                if(temp.Right!=null)
                    q.Enqueue(temp.Right);
            }
            return max;
        }

        public static bool SearchElementInBinaryTree_Recursive(Node root, int data)
        {
            if (root == null) return false;

            bool elementFound = false;
            if (root.Data == data)
                elementFound = true;
            else if (SearchElementInBinaryTree_Recursive(root.Left, data) != false)
                elementFound = true;
            else if (SearchElementInBinaryTree_Recursive(root.Right, data) != false)
                elementFound = true;

            return elementFound;
        }

        public static bool SearchElementInBinaryTree_Iterative(Node root, int data)
        {
            if (root == null) return false;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            while(q.Count>0)
            {
                var temp = q.Dequeue();
                if (temp.Data == data) 
                    return true;
                if(temp.Left!=null)
                    q.Enqueue(temp.Left);
                if (temp.Right!= null) 
                    q.Enqueue(temp.Right);
            }
            return false;
        }
    }
}