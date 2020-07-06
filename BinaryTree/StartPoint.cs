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
            SearchInsertDeleteOperationOnBinaryTree();
            DepthFirstTraversals();
            BreadthFirstTraversals();
            AuxillaryFunctionOnBinaryTree();
            
            Console.ReadKey();
        }

        /// <summary>
        /// GFG https://www.geeksforgeeks.org/binary-search-tree-set-1-search-and-insertion/
        /// </summary>
        public static void SearchInsertDeleteOperationOnBinaryTree()
        {
            TreeUtility.Print("Search, Insert & Delete Operation on a Binary Tree");
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

            Console.WriteLine($"\n Size of Tree : '{TreeUtility.SizeOfTree(bt.Top)}'");

            Console.WriteLine($"\n Height of Tree : '{TreeUtility.HeightOfTree(bt.Top)}'");

            // level with max sum of nodes in the tree
            int maxSum = -1;
                var level = TreeUtility.LevelWithMaxSum(ref maxSum, bt.Top);
                Console.WriteLine($"\n The Level : {level} has the max sum : {maxSum} in the Tree");

            // Finding Least Common Anscestor for pair of Nodes in Tree
            int c1 = 10, c2 = 20;

                // Iterative Method
                TreeUtility.FindLeastCommonAnscestor(bt.Top, c1, c2);

                // Recursive Method
                c1 = 4;
                Console.WriteLine($" LCA for '{c1}' and '{c2}' is : '{TreeUtility.FindLCA_Recursive(bt.Top, c1, c2)}'");

        }
    }
}
