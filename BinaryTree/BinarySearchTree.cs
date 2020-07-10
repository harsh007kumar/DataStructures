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

        public void AddElement(ref Node root, int data)
        {
            if (root == null)
                root = new Node(data);
            else if (data < root.Data)
                AddElement(ref root.Left, data);
            else
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
