﻿using System;
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
            bt.CheckElementExists(bt.Top, 7);

            // Find and return Node for given element in tree
            Node find = bt.FindElementNode(bt.Top, 7);

            Console.WriteLine();
            // Deleting a Node in Tree
            bt.DeleteElement(ref bt.Top, 5);
        }

        public static void DepthFirstTraversals()
        {
            TreeUtility.Print("Depth First Traversals");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            
            Console.Write("\nIn Order Traversal (Left, Root, Right) :\t");
            TreeUtility.DFS.InOrderTraversal(bt.Top);
            TreeUtility.DFS.InOrderTraveral_Iterative(bt.Top);

            Console.Write("\nPre Order Traversal (Root, Left, Right) :\t");
            TreeUtility.DFS.PreOrderTraversal(bt.Top);
            TreeUtility.DFS.PreOrderTraveral_Iterative(bt.Top);
            
            Console.Write("\nPost Order Traversal (Left, Right, Root) :\t");
            TreeUtility.DFS.PostOrderTraversal(bt.Top);
            TreeUtility.DFS.PostOrderTraversal_Iterartive(bt.Top);
        }

        public static void BreadthFirstTraversals()
        {
            TreeUtility.Print("Breadth First Traversals");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();

            TreeUtility.BFS.BreadthFirstTraversal(bt.Top);
            TreeUtility.BFS.LevelOrderTraversal(bt.Top);
        }

        public static void AuxillaryFunctionOnBinaryTree()
        {
            TreeUtility.Print("Auxillary Func : SizeOfTree() || HeightOfTree() || LevelWithMaxSum() || LeastCommonAnscestor()");

            BinarySearchTree bt = TreeUtility.GetBinaryTree();

            Console.WriteLine($"Size of Tree (Recursive): '{TreeUtility.SizeOfTree(bt.Top)}'");
            Console.WriteLine($"Size of Tree (Iterative): '{TreeUtility.SizeOfTree_Iterative(bt.Top)}'");

            Console.WriteLine($"Height of Tree : '{TreeUtility.HeightOfTree(bt.Top)}'");

            // level with max sum of nodes in the Binary Tree
            int maxSum = -1;
            var level = TreeUtility.LevelWithMaxSum(ref maxSum, bt.Top);
            Console.WriteLine($"The Level : {level} has the max sum : {maxSum} in the Tree");
        }

        public static void FindFindLeastCommonAnscestor()
        {
            TreeUtility.Print("Finding Least Common Anscestor for pair of Nodes in Binary Search Tree");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            int c1 = 10, c2 = 20;
            // Iterative Method
            Console.WriteLine("Iterative Method");
            TreeUtility.FindLeastCommonAnscestor(bt.Top, c1, c2);

            // Recursive Method
            Console.WriteLine("Recursive Method");
            c1 = 4;
            Console.WriteLine($" LCA for '{c1}' and '{c2}' is : '{TreeUtility.FindLCA_Recursive(bt.Top, c1, c2)}'");
            
        }

        public static void FindMaxInBinaryTree()
        {
            TreeUtility.Print("Problem - 1 & 2 Give an algorithm for finding maximum element in binary tree(p. 241)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            // Iterative Method
            Console.WriteLine($" Max element in Binary Tree is (Recursive Func) :\t {TreeUtility.MaxElementInBinaryTree_Recursive(bt.Top)}");
            // Recursive Method
            Console.WriteLine($" Max element in Binary Tree is (Iterative Func) :\t {TreeUtility.MaxElementInBinaryTree_Iterative(bt.Top)}");
        }

        public static void SearchElementInBinaryTree()
        {
            TreeUtility.Print("Problem - 3 & 4 Give an algorithm for searching an element in binary tree.(p. 242)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();
            // Recursive Method
            Console.WriteLine($" Searching element in Binary Tree is (Recursive Func) :\t {TreeUtility.SearchElementInBinaryTree_Recursive(bt.Top, 99)}");
            // Iterative Method
            Console.WriteLine($" Searching element in Binary Tree is (Iterative Func) :\t {TreeUtility.SearchElementInBinaryTree_Iterative(bt.Top, 10)}");

        }
    
        public static void LevelOrderInReverse()
        {
            TreeUtility.Print("Problem - 8 Give an algorithm for printing the level order data in reverse order.(p. 245)");
            BinarySearchTree bt = TreeUtility.GetBinaryTree();


            TreeUtility.BFS.LevelOrderTraversal(bt.Top);
            TreeUtility.BFS.LevelOrderReverse(bt.Top);
            
        }
    }
}