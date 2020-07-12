using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LinkedList;
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

            public static void PreOrderTraversal_Iterative(Node current)
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

            public static void InOrderTraversal_Iterative(Node current)
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

            /// <summary>
            /// Time Complexity O(n) || Space Complexity O(n)
            /// </summary>
            /// <param name="root"></param>
            public static void ZigZagTraversal(Node root)
            {
                if (root == null) return;
                Stack<Node> currlevel = new Stack<Node>();
                Stack<Node> nextLevel = new Stack<Node>();
                bool leftToRight = false;
                currlevel.Push(null);
                currlevel.Push(root);
                Console.Write($" Printing Tree in Zig-Zag order :\t");
                while (currlevel.Count > 0)
                {
                    var temp = currlevel.Pop();
                    if (temp != null)
                    {
                        if (nextLevel.Count == 0) nextLevel.Push(null);
                        Console.Write($" {temp.Data}");
                        if (leftToRight)
                        {
                            if (temp.Right != null) nextLevel.Push(temp.Right);
                            if (temp.Left != null) nextLevel.Push(temp.Left);
                        }
                        else
                        {
                            if (temp.Left != null) nextLevel.Push(temp.Left);
                            if (temp.Right != null) nextLevel.Push(temp.Right);
                        }
                    }
                    else
                    {
                        leftToRight = !leftToRight;                 // Inverse the order
                        var swapLevel = currlevel;
                        currlevel = nextLevel;
                        nextLevel = swapLevel;
                    }
                }
                Console.WriteLine();
            }
        }

        internal static bool BinaryTreesIdentical(Node bt1, Node bt2)
        {
            if (bt1 == null && bt2 == null)
                return true;
            else if (bt1 == null || bt2 == null)
                return false;
            return (bt1.Data == bt2.Data && BinaryTreesIdentical(bt1.Left, bt2.Left) && BinaryTreesIdentical(bt1.Right, bt2.Right));
        }

        public static Node DeepestNodeInTree(Node root)
        {
            if (root == null) return root;

            //Step1 : Create empty Queue
            Queue<Node> q = new Queue<Node>();

            //Step2: add root node to Queue
            q.Enqueue(root);

            Console.Write("Deepest Node is Tree is :\t");
            Node lastNode=null;
            //Step3: loop until queue is not empty
            while (q.Count > 0)
            {
                lastNode = q.Dequeue();
                if (lastNode.Left != null)
                    q.Enqueue(lastNode.Left);                                                       // Push Left child in Queue
                if (lastNode.Right != null)
                    q.Enqueue(lastNode.Right);                                                      // Push Right child in Queue
            }
            Console.WriteLine(lastNode.Data);
            return lastNode;
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
        public static int HeightOfTree(Node head) => (head == null) ? 0 : 1 + Math.Max(HeightOfTree(head.Left), HeightOfTree(head.Right));

        public static int HeightOfTree_Iterative(Node root)
        {
            if (root == null) return 0;
            int level = 1;
            Queue<Node> q = new Queue<Node>();
            q.Enqueue(root);
            q.Enqueue(null);
            while (q.Count > 0)
            {
                var temp = q.Dequeue();
                if (temp == null)
                {
                    level++;
                    if (q.Count > 0)            // If Node present in Queue add null to mark end of level
                        q.Enqueue(null);
                }
                else
                {
                    if (temp.Left != null)
                        q.Enqueue(temp.Left);
                    if (temp.Right != null)
                        q.Enqueue(temp.Right);
                }
            }
            return level;
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
        public static int FindLeastCommonAnscestor_InBinarySearchTree(Node head, int data1, int data2)
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
            else if (data > head.Data)
                found = TracePathFromRoot(ref st, head.Right, data);
            return found;
        }

        /// <summary>
        /// Time Complexity O(n) same as FindLeastCommonAnscestor_InBinarySearchTree() but scans required is 1
        /// Space Complexity O(n) required for CallStack in System for recursion
        /// Assumption both Nodes are present in the tree
        /// </summary>
        /// <param name="head"></param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        public static int FindLCA_Recursive_InBinarySearchTree(Node head, int data1, int data2)
        {
            var LCA = -1;
            if (head == null) return LCA;                                        // Head is null return -1 to indicate no LCA exists
            LCA = head.Data;                                                    // Set LCA as root that is common anscestor for all nodes of tree
            if (data1 < head.Data && data2 < head.Data)
                LCA = FindLCA_Recursive_InBinarySearchTree(head.Left, data1, data2);               // Both child are in Left subtree
            else if (data1 > head.Data && data2 > head.Data)
                LCA = FindLCA_Recursive_InBinarySearchTree(head.Right, data1, data2);              // Both child are in Right subtree
            return LCA;
        }

        public static void Print(string input = "") => Console.WriteLine($"\n========= {input} =========");

        /// <summary>
        /// return Binary Search Tree Object after adding couple of Nodes
        /// </summary>
        /// <returns></returns>
        public static BinarySearchTree GetBinarySearchTree()
        {
            var BST = new BinarySearchTree();
            BST.AddElement(ref BST.root, 5);
            BST.AddElement(ref BST.root, 10);
            BST.AddElement(ref BST.root, 20);
            BST.AddElement(ref BST.root, 4);
            BST.AddElement(ref BST.root, 9);
            BST.AddElement(ref BST.root, 1);
            BST.AddElement(ref BST.root, 17);

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
        
        /// <summary>
        /// Time Complexity O(n^2) as we are caculating height for each sub node in tree || Space Complexity O(n) for callStack
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int DiameterOfBinaryTree(Node root)
        {
            if (root == null) return 0;
            var leftHeight = HeightOfTree(root.Left);
            var rightHeight = HeightOfTree(root.Right);

            var leftDiameter = DiameterOfBinaryTree(root.Left);
            var rightDiameter = DiameterOfBinaryTree(root.Right);

            return Math.Max(leftHeight + rightHeight + 1, Math.Max(leftDiameter, rightDiameter));
        }

        /// <summary>
        /// Time Complexicty O(n) || Space Complexity O(n) for callStack
        /// </summary>
        /// <param name="root"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int DiameterOfBinaryTreeInOn(Node root, ref int height)
        {
            if (root == null)
            {
                height = 0;
                return 0;
            }
            int lh = 0, rh = 0;
            var leftD = DiameterOfBinaryTreeInOn(root.Left, ref lh);
            var rtD = DiameterOfBinaryTreeInOn(root.Right, ref rh);
            height = (lh > rh ? lh : rh) + 1;
            return Math.Max(lh + rh + 1, Math.Max(leftD, rtD));
        }

        /// <summary>
        /// Time Complexity O(n) || Space Complexity O(h) for array
        /// where, h = height of tree
        /// </summary>
        /// <param name="root"></param>
        /// <param name="arr"></param>
        /// <param name="printTillIndex"></param>
        public static void PrintRootToLeafPaths(Node root, Node[] arr, int printTillIndex)
        {
            if (root == null) return;
            arr[++printTillIndex] =root;
            if (root.Left == null && root.Right == null)
            {
                Console.Write("\n Printing Path from Root to Leaf Node :\t");
                for (int i = 0; i <= printTillIndex; i++)
                    Console.Write($" {arr[i].Data}");
            }
            if (root.Left != null)
                PrintRootToLeafPaths(root.Left, arr, printTillIndex);
            if (root.Right != null)
                PrintRootToLeafPaths(root.Right, arr, printTillIndex);
        }

        /// <summary>
        /// Time Complexity O(n) || Space O(n) for CallStack
        /// </summary>
        /// <param name="root"></param>
        /// <param name="remainingPathSum"></param>
        /// <returns></returns>
        public static bool CheckIfPathwithGivenSumExists(Node root, int remainingPathSum)
        {
            if (root == null || root.Data > remainingPathSum) return false;
            
            if (remainingPathSum - root.Data == 0 && root.Left == null && root.Right == null)   // Check if Leaf Node & pathSum - Leaf.Data == 0 means Path is Valid
                return true;
            else if (CheckIfPathwithGivenSumExists(root.Left, remainingPathSum - root.Data))
                return true;
            else if (CheckIfPathwithGivenSumExists(root.Right, remainingPathSum - root.Data))
                return true;
            return false;
        }

        /// <summary>
        /// Time Complexity O(n) || Space O(n) for CallStack
        /// </summary>
        /// <param name="root"></param>
        public static void ConvertTreeToItsMirror(Node root)
        {
            if (root == null) return;
            Node temp = root.Left;
            root.Left = root.Right;
            root.Right = temp;
            ConvertTreeToItsMirror(root.Left);
            ConvertTreeToItsMirror(root.Right);
        }

        /// <summary>
        /// Time Complexity O(n) || Space O(n) for CallStack
        /// </summary>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static bool CheckGivenTreesAreMirror(Node n1, Node n2)
        {
            if (n1 == null && n2 == null) return true;
            if (n1 == null || n2 == null) return false;
            return (n1.Data == n2.Data && CheckGivenTreesAreMirror(n1.Left, n2.Right) && CheckGivenTreesAreMirror(n1.Right, n2.Left));
        }

        /// <summary>
        /// Time Complexity O(n) || Space O(n) for CallStack
        /// </summary>
        /// <param name="root"></param>
        /// <param name="n1"></param>
        /// <param name="n2"></param>
        /// <returns></returns>
        public static Node FindLCA_Recursive_InBinaryTree(Node root, int n1, int n2)
        {
            if (root == null) return root;
            if (root.Data == n1 || root.Data == n2) return root;        // If either n1 or n2 matches root data than current root is LCA

            Node LCA = null;
            var Left = FindLCA_Recursive_InBinaryTree(root.Left, n1, n2);
            var rt = FindLCA_Recursive_InBinaryTree(root.Right, n1, n2);
            if (Left != null && rt != null)                             // left and right both Not Null idicate pair of Node is present in both subtrees hence Root is the LCA
                LCA = root;
            else
                LCA = Left != null ? Left : rt;                         // If both pair of Nodes exist in either left or right subtree return the LCA from that subtree
            return LCA;
        }

        public static BinaryTree BuildTree(char[] inOrder, char[] preOrder)
        {
            BinaryTree bt = new BinaryTree();
            if (inOrder == null || preOrder == null || inOrder.Length != preOrder.Length) return bt;

            int len = inOrder.Length;

            int[] inArr = new int[len];
            for (int i = 0; i < len; i++)                   // loop to convert char array to int
                inArr[i] = Convert.ToInt32(inOrder[i]);

            int[] preArr = new int[len];
            for (int i = 0; i < len; i++)                   // loop to convert char array to int
                preArr[i] = Convert.ToInt32(preOrder[i]);

            int preOrderIndex = 0;

            BuildTree_Recursive(ref bt.root, inArr, preArr, 0, len - 1, ref preOrderIndex);
            return bt;
        }

        /// <summary>
        /// Time Complexity O(n^2) || Space Complexity O(n)
        /// Worst case occurs when tree is left skewed. Example Preorder and Inorder traversals for worst case are {A, B, C, D} and {D, C, B, A}.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="inOrder"></param>
        /// <param name="preOrder"></param>
        /// <param name="inOrderStart"></param>
        /// <param name="inOrderEnd"></param>
        /// <param name="preOrderIndex"></param>
        public static void BuildTree_Recursive(ref Node root, int[] inOrder, int[] preOrder, int inOrderStart, int inOrderEnd, ref int preOrderIndex)
        {
            if (inOrderStart > inOrderEnd) return;            // Means we have traversed thru all the nodes in inOrder array
            root = new Node(preOrder[preOrderIndex++]);

            // Find this Node index in InOrder array
            // or use Hashtable created before-hand storing the inOrderArray 'value as Key' and those key 'index in array as value' || Time Complexicty of Algo goes down to O(n)
            //Dictionary<int, int> dict = new Dictionary<int, int>();
            //dict.Add(inOrder[0], 0);
            //int indexInInOrder = dict[root.Data];

            int indexInInOrder = ReturnIndexWhoseDataMatches(inOrder, inOrderStart, inOrderEnd, root.Data);
            
            BuildTree_Recursive(ref root.Left, inOrder, preOrder, inOrderStart, indexInInOrder - 1, ref preOrderIndex);
            BuildTree_Recursive(ref root.Right, inOrder, preOrder, indexInInOrder + 1, inOrderEnd, ref preOrderIndex);
        }

        public static int ReturnIndexWhoseDataMatches(int[] array, int startingIndex, int endIndex, int data)
        {
            for (int i = startingIndex; i <= endIndex; i++)
                if (array[i] == data)
                    return i;
            return -1;
        }

        public static bool PrintAllAnscestorsInBinaryTree(Node root, int data)
        {
            if (root == null) return false;
            if (root.Data == data)
                return true;
            else if (PrintAllAnscestorsInBinaryTree(root.Left, data) || PrintAllAnscestorsInBinaryTree(root.Right, data))
            {
                Console.Write($">> {root.Data} ");
                return true;
            }
            return false;
        }

        /// <summary>
        /// returns Binary Tree Object after adding couple of Nodes
        /// </summary>
        /// <returns></returns>
        public static BinaryTree GetBinaryTree()
        {
            var bt = new BinaryTree();
            BinaryTree.AddRoot(ref bt.root, 1);
            BinaryTree.AddToLeft(ref bt.root, 2);
            BinaryTree.AddToRight(ref bt.root, 3);
            BinaryTree.AddToLeft(ref bt.root.Left, 4);
            BinaryTree.AddToRight(ref bt.root.Left, 5);
            BinaryTree.AddToLeft(ref bt.root.Right, 6);
            BinaryTree.AddToRight(ref bt.root.Right, 7);
            return bt;
        }

        public static void VerticalSumInBinaryTree(Node root, int column, ref Dictionary<int, int> dt)
        {
            if (root == null) return;
            VerticalSumInBinaryTree(root.Left, column - 1, ref dt);
            if (dt.ContainsKey(column))
                dt[column] += root.Data;
            else
                dt.Add(column, root.Data);
            VerticalSumInBinaryTree(root.Right, column + 1, ref dt);
        }

        public static class GenericTree_Operations
        {
            /// <summary>
            /// returns Generic Tree Object after adding couple of Nodes
            /// </summary>
            /// <returns></returns>
            public static GenericTree GetGenericTree()
            {
                var gt = new GenericTree();
                GenericTree.AddRoot(ref gt.root, 1);
                GenericTree.AddChild(ref gt.root, 2);
                GenericTree.AddChild(ref gt.root, 3);
                GenericTree.AddChild(ref gt.root, 4);
                GenericTree.AddChild(ref gt.root, 5);
                GenericTree.AddChild(ref gt.root, 6);
                GenericTree.AddChild(ref gt.root.FirstChild, 11);
                GenericTree.AddChild(ref gt.root.FirstChild, 12);
                GenericTree.AddChild(ref gt.root.FirstChild.NextSibling, 22);
                GenericTree.AddChild(ref gt.root.FirstChild.NextSibling, 23);
                return gt;
            }

            /// <summary>
            /// Time Complexity O(n) || Space Complexity O(n)
            /// </summary>
            /// <param name="root"></param>
            /// <returns></returns>
            public static int FindSum(NodeGeneric root)
            {
                if (root == null) return 0;
                return root.Data + FindSum(root.FirstChild) + FindSum(root.NextSibling);
            }
            public static bool IsISOMorphic(NodeGeneric root1, NodeGeneric root2)
            {
                if (root1 == null && root2 == null) return true;
                else if (root1 == null || root2 == null) return false;
                return IsISOMorphic(root1.FirstChild, root2.FirstChild) && IsISOMorphic(root1.NextSibling, root2.NextSibling);
            }
        }

        public static class ThreadedBinaryTree_Operation
        {
            /// <summary>
            /// Inorder Successor in Inorder Full Threaded Binary Tree(p. 283)
            /// Time Complexity O(n) || Space O(1)
            /// </summary>
            /// <param name="root"></param>
            /// <returns></returns>
            public static NodeThreaded InOrderSuccessorInThreadedBinaryTree(NodeThreaded root)
            {
                if (root.RTag == 0)                             // if RTag is 0 mean right node is empty its pointing to InOrder Successor
                    return root.Right;
                else
                {                                               // Else return the left most child in the right subtree
                    var temp = root.Right;
                    while (temp.LTag == 1)
                        temp = temp.Left;
                    return temp;
                }
            }

            /// <summary>
            /// Inorder Traversal in Inorder Threaded Binary Tree (p. 283)
            /// Karumanchi, Narasimha.Data Structures and Algorithms Made Easy: Data Structures and Algorithmic Puzzles
            /// Time Complexity O(n) || Space O(1)
            /// </summary>
            /// <param name="dummyNode"></param>
            public static void InOrderTraversalInInOrderedThreadBinaryTree(NodeThreaded dummyNode)
            {
                if (dummyNode.LTag == 0) return;                     // Tree is empty
                var temp = dummyNode.Left;                           // Fetch left child from dummy node
                // Goto very first child on the left
                while (temp.LTag != 0)
                    temp = temp.Left;

                // While we dont get back to our starting dummyNode print the elements
                while (temp != dummyNode)
                {
                    Console.Write($" {temp.Data}");
                    temp = InOrderSuccessorInThreadedBinaryTree(temp);
                }
            }

            public static NodeThreaded PreOrderSuccessorInInOrderFullyThreadedBinaryTree(NodeThreaded root)
            {
                if (root.LTag == 1) 
                    return root.Left;
                while (root.RTag != 1)
                    root = root.Right;
                return root.Right;
            }

            /// <summary>
            /// Insertion of Nodes in InOrder Threaded Binary Trees (p. 285)
            /// Karumanchi, Narasimha.Data Structures and Algorithms Made Easy: Data Structures and Algorithmic Puzzles
            /// </summary>
            /// <param name="parentNode"></param>
            /// <param name="data"></param>
            public static void InsertRightChildInInOrderThreadedBinaryTree(NodeThreaded parentNode, int data)
            {
                NodeThreaded nodeToBeInserted = new NodeThreaded(data)
                {
                    RTag = parentNode.RTag,
                    Right = parentNode.Right,
                    LTag = 0,
                    Left = parentNode
                };

                parentNode.Right = nodeToBeInserted;
                parentNode.RTag = 1;

                if (nodeToBeInserted.RTag == 1)                                 // Right Child exist of parent Node
                {
                    var leftMostNodeInRtSubtree = nodeToBeInserted.Right;
                    while (leftMostNodeInRtSubtree.LTag != 0)
                        leftMostNodeInRtSubtree = leftMostNodeInRtSubtree.Left; // fetch left most child in rt subtree
                    leftMostNodeInRtSubtree.Left = nodeToBeInserted;            // assign LeftMost child Left to newly added Node
                }
            }
        }

        public static BinaryTree GenerateExpressionTreeFromPostFix(char[] postFixArr)
        {
            var len = postFixArr.Length;
            List<char> operators = new List<char>();
            operators.Add('+');
            operators.Add('-');
            operators.Add('/');
            operators.Add('*');

            Stack<Node> st = new Stack<Node>();
            for(int i=0;i<len;i++)
            {
                if(operators.Contains(postFixArr[i]))   // operator
                {
                    var rightChild = st.Pop();  // 1st Pop will be rt
                    var leftChild = st.Pop();   // 2nd Pop will be left
                    Node op = new Node(postFixArr[i]) { Left = leftChild, Right = rightChild };
                    st.Push(op);
                }
                else                                    // operand
                    st.Push(new Node(postFixArr[i]));
            }
            return new BinaryTree() { root = st.Pop() };
        }

        public static bool CheckIfBST(Node root)
        {
            if (root == null) return true;
            bool leftCheck = true, rtCheck = true;
            if (root.Left != null && root.Left.Data > root.Data)
                leftCheck = false;
            if (root.Right != null && root.Right.Data < root.Data)
                rtCheck = false;
            return (leftCheck && rtCheck && CheckIfBST(root.Left) && CheckIfBST(root.Right));
        }

        /// <summary>
        /// Converts BST to Doubly Circular Linked List (In-Place) and returns List Head
        /// Time Complexity O(n) || Space Complexity O(n) for recursion stack
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static Node ConvertBSTToCircularLinkedList(ref Node root)
        {
            if (root == null) return root;
            Node leftSubtree = null, rtSubtree = null;
            var rootRt = root.Right;

            if (root.Left != null)      // Left Subtree Not Null
            {
                leftSubtree = ConvertBSTToCircularLinkedList(ref root.Left);
                leftSubtree.Left = root;            // Head->left = last
                root.Right = leftSubtree;           // LastNode->Right = head
                root.Left.Right = root;
            }
            
            if (rootRt != null)         // Rt Subtree Not Null
            {
                rtSubtree = ConvertBSTToCircularLinkedList(ref rootRt);

                var temp = root;
                if (root.Left != null)
                {
                    root.Right.Left = rtSubtree.Left;// PrvTree circular list Head i.e,  Head->Prv = new circular list Last Node
                    temp = root.Right;
                }
                else
                    root.Left = rtSubtree.Left;     // PrvTree circular list Head i.e,  Head->Prv = new circular list Last Node

                root.Right = rtSubtree;             // PrvTree circular list last i.e,  Last->Next = new circular List's Head

                rtSubtree.Left.Right = temp;        // new circular List's last i.e,    Last->Next = PrvTree->Head
                rtSubtree.Left = root;              // new circular List's Head i.e,    Head.Previous = PrvTree's Last Node
            }

            return leftSubtree != null ? leftSubtree : root;
        }

        /// <summary>
        /// Converts Binary Tree to Doubly Linked List via simple algo || Time Complexity O(n) || Space Complexity O(n) for recursion stack
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static Node ConvertBinaryTreeToCircularLinkedList(Node root)
        {
            if (root == null) return null;
            var leftList = ConvertBinaryTreeToCircularLinkedList(root.Left);
            var rtList = ConvertBinaryTreeToCircularLinkedList(root.Right);

            // Make Single root Node circular
            root.Left = root.Right = root;

            root = ConcatenateTwoList(leftList, root);      // Concate Left and Root
            root = ConcatenateTwoList(root, rtList);        // Concate above and Right

            return root;
        }

        /// <summary>
        /// Func which contatenates two Doubly Circular Linked List and returns head of merged list
        /// </summary>
        /// <param name="leftList"></param>
        /// <param name="rightList"></param>
        /// <returns></returns>
        public static Node ConcatenateTwoList(Node leftList, Node rightList)
        {
            if (leftList == null) return rightList;
            else if (rightList == null) return leftList;
            else
            {   // A....B              C....D
                leftList.Left.Right = rightList;    // B->Next = C
                rightList.Left.Right = leftList;    // D->Next = A
                var leftListLastNode = leftList.Left;
                leftList.Left = rightList.Left;     // A->Prv = D
                rightList.Left = leftListLastNode;  // C->Prv = B
            }
            return leftList;
        }

        public static void PrintLinkedListFromHeadToLast(Node head)
        {
            Console.Write("Printing Linked List from head to Last Node :\t");
            var temp = head;
            while (temp.Right != head)             // temp.Next!=Head
            {
                Console.Write($" >> {temp.Data}");
                temp = temp.Right;              // temp = temp.Next
            }
            Console.Write($" >> {temp.Data}\n");
        }
        public static void PrintLinkedListFromLastToHead(Node last)
        {
            Console.Write("Printing Linked List from last to head Node :\t");
            var temp = last;
            while (temp.Left != last)             // last.Previous != last
            {
                Console.Write($" >> {temp.Data}");
                temp = temp.Left;              // temp = temp.Previous
            }
            Console.Write($" >> {temp.Data}\n");
        }
    
        public static LinkedList.Node ConvertSortedDoublyLinkedListToBalancedBST(LinkedList.Node head)
        {
            if (head == null) return null;
            var middle = LinkedList.LinkedListUtility.FindMiddle(head);

            var temp = middle.prv;
            if (temp != null) temp.next = null;
            
            temp = middle.next;
            if (temp != null) temp.prv = null;
            
            middle.prv = middle.next = null;

            if (middle != head) middle.prv = ConvertSortedDoublyLinkedListToBalancedBST(head);
            if (middle != temp) middle.next = ConvertSortedDoublyLinkedListToBalancedBST(temp);

            return middle;
        }
    }
}
