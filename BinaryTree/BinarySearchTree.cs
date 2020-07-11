using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTree
{
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
            LTag = RTag = 0;
            Left = Right = null;
        }
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
}
