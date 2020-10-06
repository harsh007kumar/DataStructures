using System;
using System.Collections.Generic;

namespace BinaryTree
{
    public class BSTIterator
    {
        Stack<Node> st = new Stack<Node>();                 // used to store next set of inorder Nodes i.e. Root

        public BSTIterator(Node root)
        {
            while (root != null)
            { st.Push(root); root = root.Left; }
        }

        // Return the next smallest number // Time O(1) in avg (each Node is added and removed from Stack max twice) || Space O(H), H = height of BST
        public int Next()
        {
            var result = st.Pop();                          // get stack one from of top
            var current = result.Right;
            while (current != null)
            { st.Push(current); current = current.Left; }
            return result.Data;
        }

        // Returns whether we have a next smallest number // Time O(1)
        public bool HasNext() => st.Count > 0;
    }
}
