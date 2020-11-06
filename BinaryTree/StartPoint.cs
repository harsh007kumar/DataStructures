using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Cryptography;
using LinkedList;

namespace BinaryTree
{

    class StartPoint
    {
        public static void Main(string[] args)
        {
            SearchInsertDeleteOperationOnBinarySearchTree();
            DepthFirstTraversals();
            BreadthFirstTraversals();
            AuxillaryFunctionOnBinaryTree();
            FindFindLeastCommonAnscestorInBinarySearchTree();
            FindMaxInBinaryTree();
            SearchElementInBinaryTree();
            LevelOrderInReverse();
            CheckTwoBinaryTreesIdentical();
            FindDiameterOfBinaryTree();
            PrintAllPathsFromRootToLeafNodesInBinaryTree();
            ForGivenSUMCheckIfPathExists();
            ConverTreeIntoMirror();
            FindLCAInBinaryTree();
            ConstructBinaryTreeFromItsInOrderAndPreOrderTraversals();
            PrintingAllAnscestorsOfAnNodeInBinaryTree();
            PrintBinaryTreeInZigZagOrder();
            CalculateVerticalSumInBinaryTree();
            ContructTreeFromPreOrderTraversalWhereInternalNodeisIandLeafNodeisL();
            FindSumOfAllInGenericTree();
            BuildExpressionTreeFromPostFixExpression();
            ConvertBinaryTreeToDoublyLinkedList_InPlace();
            ConvertSortedDoublyLinkedListToBalancedBST_InPlace();
            ConvertingArrayToBST();
            ConvertSingleSortedLinkedListToBalanceBST_BottomUpApporach();
            FindKthSmallestElementInBST();
            FindCielAndFloorInBST();
            FindIntersectionOfTwoBSTs();
            PrintAllElementsInBSTInANRange();
            NoOfBSTPossilbe();
            InsertionInAVL();
            AlgoToGenerateFullBinaryTree();
            ConstructMinimalAVLTreeOfHeightH_Plus_CheckIsAVLTree();
            ClosetElementToGivenKeyInBST();
            RemoveHalf_RemoveLeafs_RemoveNodesNotInRange();
            ConnectAdjacentNodesInBinaryTree();
            CalculateMaxPathSumInBinaryTree();
            SerializeDeserializeBinaryTree();
            BinarySearchTreeIterator();
            Console.ReadKey();
        }

        /// <summary>
        /// GFG https://www.geeksforgeeks.org/binary-search-tree-set-1-search-and-insertion/
        /// </summary>
        public static void SearchInsertDeleteOperationOnBinarySearchTree()
        {
            TreeUtility.Print("Search, Insert & Delete Operation on a Binary Search Tree");
            // Inserting Elements
            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();

            // Checking existance for given element in tree
            bst.CheckElementExists(bst.root, 7);

            // Find and return Node for given element in tree
            Node find = bst.FindElementNode_Recursive(bst.root, 7);
            find = bst.FindElementNode_Iterative(7);

            // Deleting a Node in Tree
            TreeUtility.DFS.InOrderTraversal(bst.root);
            Console.WriteLine();
            bst.DeleteElement(ref bst.root, 5);
        }

        public static void DepthFirstTraversals()
        {
            TreeUtility.Print("Depth First Traversals");

            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();

            Console.Write("\nIn Order Traversal (Left, Root, Right) :\t");
            TreeUtility.DFS.InOrderTraversal(bt.root);
            TreeUtility.DFS.InOrderTraversal_Iterative(bt.root);

            Console.Write("\nPre Order Traversal (Root, Left, Right) :\t");
            TreeUtility.DFS.PreOrderTraversal(bt.root);
            TreeUtility.DFS.PreOrderTraversal_Iterative(bt.root);

            Console.Write("\nPost Order Traversal (Left, Right, Root) :\t");
            TreeUtility.DFS.PostOrderTraversal(bt.root);
            TreeUtility.DFS.PostOrderTraversal_Iterartive(bt.root);
        }

        public static void BreadthFirstTraversals()
        {
            TreeUtility.Print("Breadth First Traversals");

            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();

            TreeUtility.BFS.BreadthFirstTraversal(bt.root);
            TreeUtility.BFS.LevelOrderTraversal(bt.root);
        }

        public static void AuxillaryFunctionOnBinaryTree()
        {
            TreeUtility.Print("Auxillary Func : SizeOfTree() || HeightOfTree() || LevelWithMaxSum() || LeastCommonAnscestor()");

            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();

            Console.WriteLine($"Size of Tree (Recursive): '{TreeUtility.SizeOfTree(bst.root)}'");
            Console.WriteLine($"Size of Tree (Iterative): '{TreeUtility.SizeOfTree_Iterative(bst.root)}'");

            Console.WriteLine($"Height of Tree (Recursive) :\t'{TreeUtility.HeightOfTree(bst.root)}'");
            Console.WriteLine($"Height of Tree (Iterative) :\t'{TreeUtility.HeightOfTree_Iterative(bst.root)}'");

            // level with max sum of nodes in the Binary Tree
            int maxSum = -1;
            var level = TreeUtility.LevelWithMaxSum(ref maxSum, bst.root);
            Console.WriteLine($"The Level : {level} has the max sum : {maxSum} in the Tree");

            // Deepest Node in the Tree
            TreeUtility.DeepestNodeInTree(bst.root);

            // Min Value in Binary Search Tree
            Console.WriteLine($"Min Value in Tree (Recursive) :\t'{bst.FindMin_Recursive(bst.root)}'");
            Console.WriteLine($"Min Value in Tree (Iterative) :\t'{bst.FindMin_Iterative(bst.root)}'");

            // Check BST or not
            Console.WriteLine($"Checking if is tree is BST (Recursive) :\t'{TreeUtility.CheckIfBST(bst.root)}'");
        }

        public static void FindFindLeastCommonAnscestorInBinarySearchTree()
        {
            TreeUtility.Print("Finding Least Common Anscestor for pair of Nodes in Binary Search Tree");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            int c1 = 10, c2 = 20;
            // Iterative Method
            Console.WriteLine("Iterative Method");
            TreeUtility.FindLeastCommonAnscestor_InBinarySearchTree(bt.root, c1, c2);

            // Recursive Method
            Console.WriteLine("Recursive Method");
            c1 = 4;
            Console.WriteLine($" LCA for '{c1}' and '{c2}' is : '{TreeUtility.FindLCA_Recursive_InBinarySearchTree(bt.root, c1, c2)}'");

        }

        public static void FindMaxInBinaryTree()
        {
            TreeUtility.Print("Problem - 1 & 2 Give an algorithm for finding maximum element in binary tree(p. 241)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            // Iterative Method
            Console.WriteLine($" Max element in Binary Tree is (Recursive Func) :\t {TreeUtility.MaxElementInBinaryTree_Recursive(bt.root)}");
            // Recursive Method
            Console.WriteLine($" Max element in Binary Tree is (Iterative Func) :\t {TreeUtility.MaxElementInBinaryTree_Iterative(bt.root)}");
        }

        public static void SearchElementInBinaryTree()
        {
            TreeUtility.Print("Problem - 3 & 4 Give an algorithm for searching an element in binary tree.(p. 242)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            // Recursive Method
            Console.WriteLine($" Searching element in Binary Tree is (Recursive Func) :\t {TreeUtility.SearchElementInBinaryTree_Recursive(bt.root, 99)}");
            // Iterative Method
            Console.WriteLine($" Searching element in Binary Tree is (Iterative Func) :\t {TreeUtility.SearchElementInBinaryTree_Iterative(bt.root, 10)}");

        }

        public static void LevelOrderInReverse()
        {
            TreeUtility.Print("Problem - 8 Give an algorithm for printing the level order data in reverse order.(p. 245)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();


            TreeUtility.BFS.LevelOrderTraversal(bt.root);
            TreeUtility.BFS.LevelOrderReverse(bt.root);

        }

        public static void CheckTwoBinaryTreesIdentical()
        {
            TreeUtility.Print("Problem - 17 Given two binary trees, return true if they are structurally identical.(p. 250)");

            BinarySearchTree bt1 = TreeUtility.GetBinarySearchTree();
            BinarySearchTree bt2 = TreeUtility.GetBinarySearchTree();
            //bt2.AddElement(ref bt2.root,111);
            Console.WriteLine($" Given two binary trees are identicial or not returned :\t{TreeUtility.BinaryTreesIdentical(bt1.root, bt2.root) }");

        }

        public static void FindDiameterOfBinaryTree()
        {
            TreeUtility.Print("Problem - 18 Give an algorithm for finding the diameter of the binary tree.(p. 251)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            int diameter = TreeUtility.DiameterOfBinaryTree(bt.root);
            Console.WriteLine($"Diameter of the Binary Tree is :\t{diameter}\tcalculate in O(n^2) time");
            int treeHeight = 0;
            diameter = TreeUtility.DiameterOfBinaryTreeInOn(bt.root, ref treeHeight);
            Console.WriteLine($"Diameter of the Binary Tree is :\t{diameter}\tcalculate in O(n) time");
        }

        public static void PrintAllPathsFromRootToLeafNodesInBinaryTree()
        {
            TreeUtility.Print("Problem - 20 Given a binary tree, print out all its root - to - leaf paths.(p. 254)");

            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            Console.WriteLine();
            var len = TreeUtility.HeightOfTree(bt.root);
            Node[] arr = new Node[len];                 // creating array of size = height of tree
            TreeUtility.PrintRootToLeafPaths(bt.root, arr, -1);
        }

        public static void ForGivenSUMCheckIfPathExists()
        {
            TreeUtility.Print("Problem - 21 Give an algorithm for checking the existence of path with given sum.That means, given a sum, check whether there exists a path from root to any of the nodes.(p. 255)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            int checkForPathWithSUM = 10;

            Console.WriteLine($" Path with given sum :\t{checkForPathWithSUM}, Exists : {TreeUtility.CheckIfPathwithGivenSumExists(bt.root, checkForPathWithSUM)}");
        }

        public static void ConverTreeIntoMirror()
        {
            TreeUtility.Print("Problem - 24 Give an algorithm for converting a tree to its mirror.(p. 256)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            TreeUtility.BFS.LevelOrderTraversal(bt.root);
            TreeUtility.ConvertTreeToItsMirror(bt.root);
            TreeUtility.BFS.LevelOrderTraversal(bt.root);


            TreeUtility.Print("Problem - 25 Given two trees, give an algorithm for checking whether they are mirrors of each other.(p. 257)");
            BinarySearchTree mirror = TreeUtility.GetBinarySearchTree();
            //mirror.AddElement(ref bt.root, 123);
            Console.WriteLine($" Tree are mirror : {TreeUtility.CheckGivenTreesAreMirror(mirror.root, bt.root)}");

        }

        public static void FindLCAInBinaryTree()
        {
            TreeUtility.Print("Problem - 26 Give an algorithm for finding LCA (Least Common Ancestor) of two nodes in a Binary Tree.(p. 257)");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            int n1 = 10, n2 = 17;
            var LCA = TreeUtility.FindLCA_Recursive_InBinaryTree(bt.root, n1, n2);
            Console.WriteLine($" LCA of {n1} {n2} is :\t{LCA.Data}");
        }

        public static void ConstructBinaryTreeFromItsInOrderAndPreOrderTraversals()
        {
            TreeUtility.Print("Problem - 27 Give an algorithm for constructing binary tree from given Inorder and Preorder traversals.(p. 257)");
            var inOrder = "DBEAFC";
            var preOrder = "ABDECF";
            Console.WriteLine($" In Order :\t{inOrder}");
            Console.WriteLine($" Pre Order :\t{preOrder}");

            BinaryTree bt = TreeUtility.BuildTree(inOrder.ToCharArray(), preOrder.ToCharArray());
            LevelOrderTraversal(bt.root);
            void LevelOrderTraversal(Node current)
            {
                //Step1 : Create empty Queue
                Queue<Node> q = new Queue<Node>();

                //Step2: assign root node to temp variable
                Node temp = current;

                Console.Write(" Level Order :\t");

                //Step3: loop until temp==null
                while (temp != null)
                {
                    Console.Write($"{(char)temp.Data}");                                           // Print parent node data
                    if (temp.Left != null)
                        q.Enqueue(temp.Left);                                                       // Push Left child in Queue
                    if (temp.Right != null)
                        q.Enqueue(temp.Right);                                                      // Push Right child in Queue
                    temp = q.Count > 0 ? q.Dequeue() : null;
                }
                Console.WriteLine();
            }
        }

        public static void PrintingAllAnscestorsOfAnNodeInBinaryTree()
        {
            TreeUtility.Print("Problem - 29 Give an algorithm for printing all the ancestors of a node in a Binary tree.(p. 261)");
            BinaryTree bt = TreeUtility.GetBinaryTree();
            TreeUtility.BFS.LevelOrderTraversal(bt.root);
            Console.Write($"Printing Anscestor of a Node '7' in Binary Tree are :\t");
            TreeUtility.PrintAllAnscestorsInBinaryTree(bt.root, 7);
        }

        public static void PrintBinaryTreeInZigZagOrder()
        {
            TreeUtility.Print("Problem - 30 Zigzag Tree Traversal: Give an algorithm to traverse a binary tree in Zigzag order.(p. 262)");
            BinaryTree bt = TreeUtility.GetBinaryTree();
            TreeUtility.BFS.LevelOrderTraversal(bt.root);
            TreeUtility.BFS.ZigZagTraversal(bt.root);
        }

        public static void CalculateVerticalSumInBinaryTree()
        {
            TreeUtility.Print("Problem - 31 Give an algorithm for finding the vertical sum of a binary tree.(p. 263)");
            BinaryTree bt = TreeUtility.GetBinaryTree();
            Dictionary<int, int> dt = new Dictionary<int, int>();
            TreeUtility.VerticalSumInBinaryTree(bt.root, 0, ref dt);
            TreeUtility.DFS.InOrderTraversal_Iterative(bt.root);
            Console.WriteLine("Vertical sum of a binary tree");
            foreach (var col in dt)
                Console.Write($"|| Column {col.Key} Value {col.Value} ");
        }

        public static void ContructTreeFromPreOrderTraversalWhereInternalNodeisIandLeafNodeisL()
        {
            TreeUtility.Print("Problem - 33 Given a tree with a special property where leaves are represented with ‘L’ and internal node with ‘I’." +
                "Also, assume that each node has either 0 or 2 children.Given preorder traversal of this tree, construct the tree.(p. 265)");
            string preOrder = "ILILL";
            Console.WriteLine($" Provided Pre-Order\t{preOrder}");
            BinaryTree bt = new BinaryTree();
            int startFrom = 0;

            ConvertFromPreOrder(ref bt.root, preOrder.ToCharArray(), ref startFrom);

            Console.Write(" Pre-Order of generated Tree :\t");
            PreOrderTraversal(bt.root);
            Console.WriteLine("\n");
            #region Local Func
            void ConvertFromPreOrder(ref Node root, char[] arr, ref int index)
            {
                if (index >= arr.Length) return;
                root = new Node(arr[index++]);
                if (arr[index - 1] == 'L') return;
                ConvertFromPreOrder(ref root.Left, arr, ref index);
                ConvertFromPreOrder(ref root.Right, arr, ref index);
            }
            void PreOrderTraversal(Node current)
            {
                if (current == null) return;
                Console.Write($"{(char)current.Data}");
                PreOrderTraversal(current.Left);
                PreOrderTraversal(current.Right);
            }
            #endregion
        }

        public static void FindSumOfAllInGenericTree()
        {
            TreeUtility.Print("Problem - 36 Given a generic tree, give an algorithm for finding the sum of all the elements of the tree.(p. 271)");
            GenericTree gt = TreeUtility.GenericTree_Operations.GetGenericTree();
            var sum = TreeUtility.GenericTree_Operations.FindSum(gt.root);
            Console.WriteLine($"Sum of all Node in Generic-Tree(N-ary Tree) is :\t{sum}");

            // CheckIfTwoGenericTreesAreIsoMorphic
            TreeUtility.Print("Problem - 42 Given two generic trees how do we check whether the trees are isomorphic to each other or not? (p. 275)");
            GenericTree gt2 = TreeUtility.GenericTree_Operations.GetGenericTree();
            var isISO = TreeUtility.GenericTree_Operations.IsISOMorphic(gt.root, gt2.root);
            Console.WriteLine($"Given Two Trees are Iso-Morphic : {isISO}");
        }

        public static void BuildExpressionTreeFromPostFixExpression()
        {
            TreeUtility.Print("Building Expression Tree from Postfix Expression(p. 289)");
            string postFixExpression = "ABC*+D/";
            Console.WriteLine($"Provided PostFix Expression :\t{postFixExpression}");
            BinaryTree bt = TreeUtility.GenerateExpressionTreeFromPostFix(postFixExpression.ToCharArray());
            Console.Write($"PostFix Expression of Expression Tree :\t");
            PostFix(bt.root);
            // local function
            void PostFix(Node root)
            {
                if (root == null) return;
                PostFix(root.Left);
                PostFix(root.Right);
                Console.Write($" {(char)root.Data}");
            }
            Console.WriteLine();
        }

        public static void ConvertBinaryTreeToDoublyLinkedList_InPlace()
        {
            TreeUtility.Print("Problem - 54 Give an algorithm for converting BST to circular DLL with space complexity O(1)(p.313)");
            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(bst.root);

            // Convert BST to DoublyLinkedList
            var head = TreeUtility.ConvertBSTToCircularLinkedList(ref bst.root);
            TreeUtility.PrintLinkedListFromHeadToLast(head);
            TreeUtility.PrintLinkedListFromLastToHead(head.Left);

            // Efficient Approach || Divide and Conquer Approach
            // GFG https://www.geeksforgeeks.org/convert-a-binary-tree-to-a-circular-doubly-link-list/
            BinaryTree bt = TreeUtility.GetBinaryTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(bt.root);
            
            head = TreeUtility.ConvertBinaryTreeToCircularLinkedList(bt.root);
            TreeUtility.PrintLinkedListFromHeadToLast(head);
            TreeUtility.PrintLinkedListFromLastToHead(head.Left);
        }

        public static void ConvertSortedDoublyLinkedListToBalancedBST_InPlace()
        {
            TreeUtility.Print("Problem - 56 Given a sorted doubly linked list, give an algorithm for converting it into balanced binary search tree.(p. 314)");
            DoublyLinkedList dll = LinkedListUtility.GetDoublyLinkledList();
            dll.PrintFromStart();

            var root = TreeUtility.ConvertSortedDoublyLinkedListToBalancedBST(dll.Head);
            Console.Write("\nPrint Binary Tree in InOrder Traversal:\t\t");
            LinkedListUtility.PrintTreeInOrderTraversal(root);
            Console.Write("\n\nPrint Binary Tree in PreOrder Traversal:\t");
            LinkedListUtility.PrintTreePreOrderTraversal(root);
            Console.WriteLine();
        }

        public static void ConvertingArrayToBST()
        {
            TreeUtility.Print("Problem - 57 Given a sorted array, give an algorithm for converting the array to BST.(pp. 314 - 315)");
            int[] arr = new int[51];
            Console.Write("\nPrinting Sorted Array from Start :\t");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = i * 10;
                Console.Write($" {arr[i]}");
            }

            var root = TreeUtility.GenerateBalancedBSTFromSortedArray(arr, 0, arr.Length - 1);
            Console.Write("\n\nPrinting Balanced BST in PreOrder Traversal (Root || Left || Right) :\t");
            TreeUtility.DFS.PreOrderTraversal(root);
            Console.WriteLine();
        }

        public static void ConvertSingleSortedLinkedListToBalanceBST_BottomUpApporach()
        {
            TreeUtility.Print("Problem - 58 Given a singly linked list where elements are sorted in ascending order, convert it to a height balanced BST.(p. 315)");
            SinglyLinkedList sll = LinkedList.LinkedListUtility.GetSinglyLinkedList();
            LinkedList.LinkedListUtility.PrintContent(sll.Head);
            var root = TreeUtility.ConvertSortedSinglyLinkedListToHeightBalancedBST(ref sll.Head, sll.Count);
            TreeUtility.DFS.PreOrderTraversal_Iterative(root);
        }

        public static void FindKthSmallestElementInBST()
        {
            TreeUtility.Print("Problem - 60 Give an algorithm for finding the kth smallest element in BST.(p. 317)");
            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(bst.root);

            int k = 4, noOfElementRead = 0;
            Node kNode = TreeUtility.KthSmallestElementInBST(bst.root, k, ref noOfElementRead);

            if (kNode != null) Console.WriteLine($" K : {k}th smallest element in above BST is : {kNode.Data}");
        }

        public static void FindCielAndFloorInBST()
        {
            TreeUtility.Print("Problem - 61 Floor and ceiling : in BST (p. 317)");
            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(bst.root);

            int data = 16, ciel = -1, floor = -1;
            TreeUtility.CielAndFloorInBST(bst.root, data, ref ciel, ref floor);

            Console.WriteLine($" In abvove BST for data:{data} \tCiel: {ciel} \tFloor: {floor}");
        }

        /// <summary>
        /// GFG https://www.geeksforgeeks.org/print-common-nodes-in-two-binary-search-trees/
        /// Problem - 62 Give an algorithm for finding the union and intersection of BSTs.(p. 319)
        /// </summary>
        public static void FindIntersectionOfTwoBSTs()
        {
            TreeUtility.Print("Print Common Nodes in Two Binary Search Trees");
            BinarySearchTree bst1 = TreeUtility.GetBinarySearchTree();
            BinarySearchTree bst2 = TreeUtility.GetBinarySearchTree();

            var num = bst2.FindInOrderSuccessor(bst2.root.Right);
            bst2.DeleteElement(ref bst2.root, num);

            TreeUtility.DFS.InOrderTraversal_Iterative(bst1.root);
            TreeUtility.DFS.InOrderTraversal_Iterative(bst2.root);
            TreeUtility.CommonNodesInBSTs(bst1.root, bst2.root);
        }

        public static void PrintAllElementsInBSTInANRange()
        {
            TreeUtility.Print("Problem - 65 Given a BST and two numbers K1 and K2, give an algorithm for printing all the elements of BST in the range K1 and K2.(p. 320)");
            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(bst.root);
            int k1 = 4, k2 = 11;
            Console.Write($" Printing elements in above BST which are in the range [{k1}...{k2}]");
            TreeUtility.RangePrinterInBST(bst.root, k1, k2);
            Console.WriteLine();
        }

        public static void  NoOfBSTPossilbe()
        {
            TreeUtility.Print("Problem - 71 For the key values 1... n, how many structurally unique BSTs are possible that store those keys.(p. 322)");
            int nUniqueKeys = 3;
            Console.WriteLine($" No of Structurally possible BST with {nUniqueKeys} Keys :\t {TreeUtility.NoOfPossilbeBST(nUniqueKeys)}");
        }

        public static void InsertionInAVL()
        {
            TreeUtility.Print("Insertion into an AVL tree(p. 333)");
            AVLTree avl = TreeUtility.GetAVLTree();
            TreeUtility.BFS.BreadthFirstTraversal(avl.root);
        }

        public static void AlgoToGenerateFullBinaryTree()
        {
            TreeUtility.Print("Problem - 73 Given a height h, give an algorithm for generating the HB(0).(p. 334)");
            int height = 3, count = 0;
            var Node = TreeUtility.GenerateHightBalancedTree(height, ref count);
            Console.WriteLine($" For given Height = {height} below fully binary tree is created which has {count} Nodes in total");
            TreeUtility.DFS.InOrderTraversal_Iterative(Node);

            TreeUtility.Print("Problem - 74 Is there any alternative way of create Balance Binary Tree(p. 335)");
            int start = 1, end = 7;
            Console.WriteLine($" For given range {start} .. {end} below Balance Tree is generated");
            Node = TreeUtility.GenerateHightBalancedTree_WhenRangeIsProvided(start, end);
            TreeUtility.DFS.InOrderTraversal_Iterative(Node);
        }

        public static void ConstructMinimalAVLTreeOfHeightH_Plus_CheckIsAVLTree()
        { 
            TreeUtility.Print("Problem - 75 Construct minimal AVL trees of height 0,1,2,3,4, and 5.What is the number of nodes in a minimal AVL tree of height 6 ? (p. 335)");
            int height = 3, count = 0;
            var Node = TreeUtility.GenerateMinimalAVLTreeWithHeight(height, ref count);
            Console.WriteLine($" For given Height = {height} below minimal AVL tree is created which has {count} Nodes in total");
            TreeUtility.DFS.InOrderTraversal_Iterative(Node);

            TreeUtility.Print("Problem - 76 For Problem-73, how many different shapes can there be of a minimal AVL tree of height h? (p. 336)");
            Console.WriteLine($" For given Height = {height}\tNo of different minimal AVL tree is that can exists are {TreeUtility.NoOfDiffMinAVLTree(height)}");

            TreeUtility.Print("Problem - 77 Given a binary search tree, check whether it is an AVL tree or not? (p. 337)");
            TreeUtility.BFS.BreadthFirstTraversal(Node);
            Console.WriteLine($" Above BST is AVL Tree : {TreeUtility.isAVLTree(Node)}");
            Console.WriteLine($" Above BST is AVL Tree Alternate Approach : {TreeUtility.isAVLTree_Alternate(Node) != -1}");


            TreeUtility.Print("Problem - 79 Count the number of nodes in the range[a, b].(p. 338)");
            int a = 3, b = 9;
            var avl = TreeUtility.GetAVLTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(avl.root);
            count = TreeUtility.countNoOfNodesInRangeAToB(avl.root, a, b);
            Console.WriteLine($" No of Nodes in the Range [{a}...{b}] in above tree are : {count}");
        }

        public static void ClosetElementToGivenKeyInBST()
        {
            TreeUtility.Print("Problem - 82 Given a BST and a key, find the element in the BST which is closest to the given key.(p. 342)");
            BinarySearchTree bst = TreeUtility.GetBinarySearchTree();
            TreeUtility.DFS.InOrderTraversal_Iterative(bst.root);
            int k = 3, closetNode = int.MaxValue;

            TreeUtility.ClosetElementToKInBinaryTree(bst.root, k, ref closetNode);
            Console.WriteLine($" Closet Node to no '{k}' in above Binary tree is : {closetNode}");

            TreeUtility.Print("Problem - 83 For Problem-82, can we solve it using the recursive approach? (p. 343)");
            Console.WriteLine($" Closet Node to no '{k}' in above BST is : {TreeUtility.ClosetElementToKInBST(bst.root, k)}");
        }

        public static void RemoveHalf_RemoveLeafs_RemoveNodesNotInRange()
        {

            TreeUtility.Print("Problem - 85 Given a binary tree, how do you remove all the half nodes(which have only one child) ? Note that we should not touch leaves.(p. 345)");
            int count = 10;
            var root = TreeUtility.GenerateMinimalAVLTreeWithHeight(4, ref count);
            TreeUtility.BFS.BreadthFirstTraversal(root);
            Console.WriteLine("\n Remove HalfNodes(with single child) InBinaryTree");
            TreeUtility.RemoveHalfNodesInBinaryTree(root);
            TreeUtility.BFS.BreadthFirstTraversal(root);

            TreeUtility.Print("Problem - 86 Given a binary tree, how do you remove its leaves? (p. 346)");
            count = 10;
            root = TreeUtility.GenerateMinimalAVLTreeWithHeight(4, ref count);
            TreeUtility.BFS.BreadthFirstTraversal(root);
            Console.WriteLine("\n Removing Leaf Nodes");
            TreeUtility.RemoveLeafNodes(root);
            TreeUtility.BFS.BreadthFirstTraversal(root);

            TreeUtility.Print("Problem - 87 Given a BST and two integers(minimum and maximum integers) as parameters, how do you remove(prune) elements that are not within that range?(p. 346)");
            root = TreeUtility.GenerateHightBalancedTree_WhenRangeIsProvided(10, 20);
            TreeUtility.DFS.InOrderTraversal_Iterative(root);
            int a = 12, b = 17;
            Console.WriteLine($"\n Removing Element Not b/w the Range [{a}...{b}]");
            TreeUtility.RemoveElementNotInRangeAToBInBST(ref root, a, b);
            TreeUtility.DFS.InOrderTraversal_Iterative(root);
        }

        public static void ConnectAdjacentNodesInBinaryTree()
        {
            // https://leetcode.com/problems/populating-next-right-pointers-in-each-node/
            TreeUtility.Print("Problem - 88 Given a binary tree, how do you connect all the adjacent nodes at the same level?" +
                " Assume that given binary tree has next pointer along with left and right(p. 348)");
            #region Create BinaryTree
            var bt = new NodeWithNext(1);
            bt.Left = new NodeWithNext(2);
            bt.Right = new NodeWithNext(3);
            bt.Left.Left = new NodeWithNext(4);
            bt.Left.Right = new NodeWithNext(5);
            bt.Right.Left = new NodeWithNext(6);
            bt.Right.Right = new NodeWithNext(7);
            #endregion
            Console.WriteLine("\n Connecting Nodes At Same Level In BinaryTree");
            // Iterative Solution
            //TreeUtility.ConnectNodesAtSameLevelInBinaryTree(ref bt);
            // Recursive Solution
            TreeUtility.ConnectNodesNextInBinaryTree(bt);

            TreeUtility.DFS.InOrderTraversal_Iterative(bt);
        }

        public static void CalculateMaxPathSumInBinaryTree()
        {
            TreeUtility.Print("Problem - 92 Given a binary tree, find the maximum path sum. The path may start and end at any node in the tree.(p. 351)");
            BinaryTree bt = TreeUtility.GetBinaryTree();
            int treeMax = int.MinValue;
            var sum = TreeUtility.MaxPathSumInBinaryTree(bt.root, ref treeMax);
            TreeUtility.DFS.InOrderTraversal_Iterative(bt.root);
            Console.WriteLine($"Maximum sum of any path in above BinaryTree is :\t'{Math.Max(sum,treeMax)}'");
        }

        public static void SerializeDeserializeBinaryTree()
        {
            TreeUtility.Print("Serialize - Deserialize BinaryTree");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            Console.Write(" Original Tree: \t");
            TreeUtility.DFS.InOrderTraversal(bt.root);

            Console.Write("\n\n Serialized Tree: \t");
            var serialized = new Queue<int>();
            TreeUtility.SerializeTree(bt.root, serialized);
            serialized.Print();

            Console.Write("\n DeSerialized Tree: \t");
            var DeSerializedTreeRoot = TreeUtility.DeSerializeTree(serialized);
            TreeUtility.DFS.InOrderTraversal(DeSerializedTreeRoot);
        }

        public static void BinarySearchTreeIterator()
        {
            // https://leetcode.com/problems/binary-search-tree-iterator/
            TreeUtility.Print("173. Binary Search Tree Iterator");
            BinarySearchTree bt = TreeUtility.GetBinarySearchTree();
            Console.Write(" Original Tree: \t");
            TreeUtility.DFS.InOrderTraversal(bt.root);

            var iterator = new BSTIterator(bt.root);
            Console.Write($"\n Printing 'Next' in BST using created Iterator [if(HasNext()) Print(Next()) ]: ");
            while (iterator.HasNext())
                Console.Write($" {iterator.Next()}");
            Console.WriteLine();
        }
    }
}
