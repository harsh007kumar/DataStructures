﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinaryTree
{
    public class Node
    {
        public int Data { get; set; }
        public Node Left;
        public Node Right;
        public int Height { get; set; }     // Used only with AVL Tree

        public Node(int no)
        {
            Data = no;
            Left = Right = null;
            Height = 0;
        }
    }

    public class NodeGeneric
    {
        public int Data { get; set; }
        public NodeGeneric FirstChild;
        public NodeGeneric NextSibling;
        public NodeGeneric(int data)        // Parameterized Constructor
        { Data = data; FirstChild = NextSibling = null; }
    }

    public class NodeThreaded
    {
        public int Data { get; set; }
        public int LTag { get; set; }
        public int RTag { get; set; }
        public NodeThreaded Left;
        public NodeThreaded Right;

        public NodeThreaded(int data)
        {
            Data = data;
            LTag = RTag = 0;        // 0 Signifies its leaf Node
            Left = Right = null;
        }
    }

    public class NodeWithNext : Node
    {
        public NodeWithNext Next;       // Point to next node at same level
        public NodeWithNext(int data) : base(data) => Next = null;
    }

    public class BinaryTree
    {
        public Node root;
        public BinaryTree() => root = null;

        public static void AddToLeft(ref Node current, int data)
        {
            if (current == null) Console.WriteLine("Cannot add left child to an null parent node");
            else current.Left = new Node(data);
        }

        public static void AddToRight(ref Node current, int data)
        {
            if (current == null) Console.WriteLine("Cannot add right child to an null parent node");
            else current.Right = new Node(data);
        }

        public static void AddRoot(ref Node current, int data) => current = new Node(data);
    }

    public class BinarySearchTree
    {

        public Node root;
        public BinarySearchTree() { root = null; }

        /// <summary>
        /// Adds data to BST, data is already there then we can simply neglect and come out
        /// Time Complexity: O(n), in worst case (when BST is a left/right skew tree)
        /// </summary>
        /// <param name="root"></param>
        /// <param name="data"></param>
        public void AddElement(ref Node root, int data)
        {
            if (root == null)
                root = new Node(data);
            else if (data < root.Data)
                AddElement(ref root.Left, data);
            else if (data > root.Data)
                AddElement(ref root.Right, data);
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

        public Node FindElementNode_Recursive(Node head, int data)
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
                elementFoundAtNode = FindElementNode_Recursive(head.Left, data);
            else if (data > head.Data)
                elementFoundAtNode = FindElementNode_Recursive(head.Right, data);

            return elementFoundAtNode;
        }

        public Node FindElementNode_Iterative(int data)
        {
            if (root == null) return null;
            
            var temp = root;
            while(temp!=null)
            {
                if (temp.Data == data)
                    return root;
                if (data < temp.Data)
                    temp = temp.Left;
                else
                    temp = temp.Right;
            }
            return null;
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
                {   // replace the node data with its inorder successor(i.e, InOrderPredecessor of Root->Right) and call DeleteElement() on node being copied.
                    head.Data = FindInOrderPredeccessor(head.Right);
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
        public int FindInOrderPredeccessor(Node head) => head.Left != null ? FindInOrderPredeccessor(head.Left) : head.Data;

        /// <summary>
        /// Element which comes right before, in InOrderTraversal (one on immediate left)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int FindInOrderSuccessor(Node head) => head.Right != null ? FindInOrderSuccessor(head.Right) : head.Data;


        /// <summary>
        /// Time Complexity: O(n), in worst case (when BST is a left skew tree) || Space Complexity: O(n), for recursive stack.(p. 299)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int FindMin_Recursive(Node head)
        {
            int min = -1;
            if (head == null) return min;
            else if (head.Left == null)
                min = head.Data;
            else
                min = FindMin_Recursive(head.Left);
            return min;
        }

        /// <summary>
        /// Time Complexity: O(n), in worst case (when BST is a left skew tree) || Space O(1)
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public int FindMin_Iterative(Node head)
        {
            if (head == null) return -1;
            while (head.Left != null)
                head = head.Left;
            return head.Data;
        }
    }

    public class GenericTree
    {
        public NodeGeneric root;
        public GenericTree() => root = null;

        public static void AddRoot(ref NodeGeneric current, int data) => current = new NodeGeneric(data);
        public static void AddChild(ref NodeGeneric root, int data)
        {
            var newChild = new NodeGeneric(data);
            if (root.FirstChild == null)
                root.FirstChild = newChild;
            else
            {
                var lastSibiling = root.FirstChild;
                while (lastSibiling.NextSibling != null)
                    lastSibiling = lastSibiling.NextSibling;
                lastSibiling.NextSibling = newChild;
            }
        }
    }

    public class AVLTree : BinarySearchTree
    {
        public AVLTree() : base() { }

        /// <summary>
        /// Left Left InBalance (Right Rotation)(p. 328) || Time Complexity: O(1). Space Complexity: O(1).
        /// </summary>
        /// <param name="X"></param>
        public Node Right_Rotation(Node unBalancedNode)
        {
            Node middle = unBalancedNode.Left;
            unBalancedNode.Left = middle.Right;
            middle.Right = unBalancedNode;

            unBalancedNode.Height = 1 + Math.Max(Height(unBalancedNode.Left), Height(unBalancedNode.Right));
            middle.Height = 1 + Math.Max(Height(middle.Left), unBalancedNode.Height);
            return middle;
        }

        /// <summary>
        /// Right Right InBalance (Left Rotation)(p. 329) || Time Complexity: O(1). Space Complexity: O(1).
        /// </summary>
        /// <param name="unBalancedNode"></param>
        /// <returns></returns>
        public Node Left_Rotation(Node unBalancedNode)
        {
            Node middle = unBalancedNode.Right;
            unBalancedNode.Right = middle.Left;
            middle.Left = unBalancedNode;

            unBalancedNode.Height = 1 + Math.Max(Height(unBalancedNode.Left), Height(unBalancedNode.Right));
            middle.Height = 1 + Math.Max(Height(middle.Right), unBalancedNode.Height);
            return middle;
        }

        /// <summary>
        /// Left Right InBalance || Time Complexity: O(1). Space Complexity: O(1).
        /// </summary>
        /// <param name="unBalancedNode"></param>
        /// <returns></returns>
        public Node LeftRight_Rotation(Node unBalancedNode)
        {
            unBalancedNode.Left = Left_Rotation(unBalancedNode.Left);
            return Right_Rotation(unBalancedNode);
        }

        /// <summary>
        /// Right Left InBalance || Time Complexity: O(1). Space Complexity: O(1).
        /// </summary>
        /// <param name="unBalancedNode"></param>
        /// <returns></returns>
        public Node RightLeft_Rotation(Node unBalancedNode)
        {
            unBalancedNode.Right = Right_Rotation(unBalancedNode.Right);
            return Left_Rotation(unBalancedNode);
        }

        /// <summary>
        /// Time Complexity O(n)
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        protected int HeightOfTree(Node current) => (current == null) ? 0 : 1 + Math.Max(HeightOfTree(current.Left), HeightOfTree(current.Right));

        protected int Height(Node current) => current == null ? 0 : current.Height;

        /// <summary>
        /// AVL tree is balanced if balance factor lies in { -1, 0, 1}
        /// If balance > 1 left heavy || if balance < -1 right heavy
        /// Time Complexity: O(logn) || Space Complexity: O(logn)
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        protected int GetBalance(Node current) => HeightOfTree(current.Left) - HeightOfTree(current.Right);

        /// <summary>
        /// Insertion in Balanced BST gurantees Time Complexity O(Logn) || Space Complexity O(H), where H = height of tree = Logn 
        /// </summary>
        /// <param name="root"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public new Node AddElement(ref Node root, int data)
        {
            if (root == null)
                root = new Node(data);                                                  // By default height is set a 0
            else if (data < root.Data)
                AddElement(ref root.Left, data);
            else if (data > root.Data)
                AddElement(ref root.Right, data);
            
            var balanceFactor = Height(root.Left) - Height(root.Right);
            if (balanceFactor > 1)              // left heavy
                // findout : Left-Left Inbalance or Left-Right Inbalance
                root = (data < root.Left.Data) ? Right_Rotation(root) : LeftRight_Rotation(root);
            else if (balanceFactor < -1)        // rt heavy
                // findout : Right-Right Inbalance or Right-Left Inbalance
                root = (data > root.Right.Data) ? Left_Rotation(root) : RightLeft_Rotation(root);
            
            root.Height = 1 + Math.Max(Height(root.Left), Height(root.Right));          // update Height of root

            return root;
        }
    }
}
