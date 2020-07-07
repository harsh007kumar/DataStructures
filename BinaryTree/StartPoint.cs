using System;
using System.Collections.Generic;
using System.Net;
using System.Security;
using System.Security.Cryptography;

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
            FindFindLeastCommonAnscestor();
            FindMaxInBinaryTree();
            SearchElementInBinaryTree();
            LevelOrderInReverse();
            CheckTwoBinaryTreesIdentical();
            FindDiameterOfBinaryTree();
            PrintAllPathsFromRootToLeafNodesInBinaryTree();
            Console.ReadKey();
        }

        /// <summary>
        /// GFG https://www.geeksforgeeks.org/binary-search-tree-set-1-search-and-insertion/
        /// </summary>
        public static void SearchInsertDeleteOperationOnBinarySearchTree()
        {
            TreeUtility.Print("Search, Insert & Delete Operation on a Binary Search Tree");
            // Inserting Elements
            BinarySearchTree bt = TreeUtility.GetBinaryTree();

            // Checking existance for given element in tree
            bt.CheckElementExists(bt.root, 7);

            // Find and return Node for given element in tree
            Node find = bt.FindElementNode(bt.root, 7);

            Console.WriteLine();
            // Deleting a Node in Tree
            bt.DeleteElement(ref bt.root, 5);
        }

        public static void DepthFirstTraversals()
        {
            TreeUtility.Print("Depth First Traversals");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            
            Console.Write("\nIn Order Traversal (Left, Root, Right) :\t");
            TreeUtility.DFS.InOrderTraversal(bt.root);
            TreeUtility.DFS.InOrderTraveral_Iterative(bt.root);

            Console.Write("\nPre Order Traversal (Root, Left, Right) :\t");
            TreeUtility.DFS.PreOrderTraversal(bt.root);
            TreeUtility.DFS.PreOrderTraveral_Iterative(bt.root);
            
            Console.Write("\nPost Order Traversal (Left, Right, Root) :\t");
            TreeUtility.DFS.PostOrderTraversal(bt.root);
            TreeUtility.DFS.PostOrderTraversal_Iterartive(bt.root);
        }

        public static void BreadthFirstTraversals()
        {
            TreeUtility.Print("Breadth First Traversals");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();

            TreeUtility.BFS.BreadthFirstTraversal(bt.root);
            TreeUtility.BFS.LevelOrderTraversal(bt.root);
        }

        public static void AuxillaryFunctionOnBinaryTree()
        {
            TreeUtility.Print("Auxillary Func : SizeOfTree() || HeightOfTree() || LevelWithMaxSum() || LeastCommonAnscestor()");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();

            Console.WriteLine($"Size of Tree (Recursive): '{TreeUtility.SizeOfTree(bt.root)}'");
            Console.WriteLine($"Size of Tree (Iterative): '{TreeUtility.SizeOfTree_Iterative(bt.root)}'");

            Console.WriteLine($"Height of Tree (Recursive) :\t'{TreeUtility.HeightOfTree(bt.root)}'");
            Console.WriteLine($"Height of Tree (Iterative) :\t'{TreeUtility.HeightOfTree_Iterative(bt.root)}'");

            // level with max sum of nodes in the Binary Tree
            int maxSum = -1;
            var level = TreeUtility.LevelWithMaxSum(ref maxSum, bt.root);
            Console.WriteLine($"The Level : {level} has the max sum : {maxSum} in the Tree");

            // Deepest Node in the Tree
            TreeUtility.DeepestNodeInTree(bt.root);
        }

        public static void FindFindLeastCommonAnscestor()
        {
            TreeUtility.Print("Finding Least Common Anscestor for pair of Nodes in Binary Search Tree");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            int c1 = 10, c2 = 20;
            // Iterative Method
            Console.WriteLine("Iterative Method");
            TreeUtility.FindLeastCommonAnscestor(bt.root, c1, c2);

            // Recursive Method
            Console.WriteLine("Recursive Method");
            c1 = 4;
            Console.WriteLine($" LCA for '{c1}' and '{c2}' is : '{TreeUtility.FindLCA_Recursive(bt.root, c1, c2)}'");
            
        }

        public static void FindMaxInBinaryTree()
        {
            TreeUtility.Print("Problem - 1 & 2 Give an algorithm for finding maximum element in binary tree(p. 241)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            // Iterative Method
            Console.WriteLine($" Max element in Binary Tree is (Recursive Func) :\t {TreeUtility.MaxElementInBinaryTree_Recursive(bt.root)}");
            // Recursive Method
            Console.WriteLine($" Max element in Binary Tree is (Iterative Func) :\t {TreeUtility.MaxElementInBinaryTree_Iterative(bt.root)}");
        }

        public static void SearchElementInBinaryTree()
        {
            TreeUtility.Print("Problem - 3 & 4 Give an algorithm for searching an element in binary tree.(p. 242)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            // Recursive Method
            Console.WriteLine($" Searching element in Binary Tree is (Recursive Func) :\t {TreeUtility.SearchElementInBinaryTree_Recursive(bt.root, 99)}");
            // Iterative Method
            Console.WriteLine($" Searching element in Binary Tree is (Iterative Func) :\t {TreeUtility.SearchElementInBinaryTree_Iterative(bt.root, 10)}");

        }
    
        public static void LevelOrderInReverse()
        {
            TreeUtility.Print("Problem - 8 Give an algorithm for printing the level order data in reverse order.(p. 245)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();


            TreeUtility.BFS.LevelOrderTraversal(bt.root);
            TreeUtility.BFS.LevelOrderReverse(bt.root);
            
        }
    
        public static void CheckTwoBinaryTreesIdentical()
        {
            TreeUtility.Print("Problem - 17 Given two binary trees, return true if they are structurally identical.(p. 250)");

            BinarySearchTree bt1 = TreeUtility.GetBinaryTree();
            BinarySearchTree bt2 = TreeUtility.GetBinaryTree();
            //bt2.AddElement(ref bt2.root,111);
            Console.WriteLine($" Given two binary trees are identicial or not returned :\t{TreeUtility.BinaryTreesIdentical(bt1.root, bt2.root) }");
            
        }
    
        public static void FindDiameterOfBinaryTree()
        {
            TreeUtility.Print("Problem - 18 Give an algorithm for finding the diameter of the binary tree.(p. 251)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            int diameter = TreeUtility.DiameterOfBinaryTree(bt.root);
            Console.WriteLine($"Diameter of the Binary Tree is :\t{diameter}\tcalculate in O(n^2) time");
            int treeHeight = 0;
            diameter = TreeUtility.DiameterOfBinaryTreeInOn(bt.root, ref treeHeight);
            Console.WriteLine($"Diameter of the Binary Tree is :\t{diameter}\tcalculate in O(n) time");
        }

        public static void PrintAllPathsFromRootToLeafNodesInBinaryTree()
        {
            TreeUtility.Print("Problem - 20 Given a binary tree, print out all its root - to - leaf paths.(p. 254)");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            Console.WriteLine();
            var len = TreeUtility.HeightOfTree(bt.root);
            Node[] arr = new Node[len];                 // creating array of size = height of tree
            TreeUtility.PrintRootToLeafPaths(bt.root, arr, -1);
        }
    }
}
