using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TernarySearchTree
{
    public class TNTNode
    {
        public char data;
        public bool isEndOfWord;
        public TNTNode left, equal, right;

        public TNTNode(char character)
        {
            data = character;
            left = equal = right = null;
        }
    }

    /// <summary>
    /// It combines the memory efficiency of BSTs and the search time efficiency of tries.
    /// https://www.geeksforgeeks.org/ternary-search-tree/
    /// </summary>
    public class TernarySearchTree
    {
        public TNTNode root;
        public TernarySearchTree() => root = null;

        /// <summary>
        /// Recursive function which inserts new word in 'Ternary Search Tree'
        /// Time Complexity O(L), L = length of the word to be inserted || Space O(1)
        /// </summary>
        /// <param name="current"></param>
        /// <param name="newWord"></param>
        /// <param name="index"></param>
        public void Insert(ref TNTNode current, string newWord, int index = 0)
        {
            if (index >= newWord.Length) return;

            if (current == null)
                current = new TNTNode(newWord[index]);
            if (newWord[index] == current.data)
                Insert(ref current.equal, newWord, index + 1);
            else if (newWord[index] < current.data)
                Insert(ref current.left, newWord, index);
            else if (newWord[index] > current.data)
                Insert(ref current.right, newWord, index);

            if (index + 1 == newWord.Length)                    // newWord is inserted
            {
                current.isEndOfWord = true;
                Console.WriteLine($" NewWord Added \t'{newWord}'");
            }
        }
    }
}
