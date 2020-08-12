using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TernarySearchTree
{
    public class TSTNode
    {
        public char data;
        public bool isEndOfWord;
        public TSTNode left, equal, right;

        public TSTNode(char character)
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
        public TSTNode root;
        public TernarySearchTree() => root = null;

        /// <summary>
        /// Recursive function which inserts new word in 'Ternary Search Tree'
        /// Time Complexity O(L), L = length of the word to be inserted || Space O(1)
        /// </summary>
        /// <param name="current"></param>
        /// <param name="newWord"></param>
        /// <param name="index"></param>
        public void Insert(ref TSTNode current, string newWord, int index = 0)
        {
            if (index >= newWord.Length) return;

            if (current == null)                                // add new node with required character
                current = new TSTNode(newWord[index]);
            if (newWord[index] == current.data)                 // required character already present insert next character in sequence
                Insert(ref current.equal, newWord, index + 1);
            else if (newWord[index] < current.data)             // required character smaller(ASCII value) insert onto left side
                Insert(ref current.left, newWord, index);
            else if (newWord[index] > current.data)             // required character bigger(ASCII value) insert on right side
                Insert(ref current.right, newWord, index);      

            if (index + 1 == newWord.Length)                    // once newWord is inserted, mark the last node 'isEndOfWord' = true
            {
                current.isEndOfWord = true;
                Console.WriteLine($" NewWord Added \t'{newWord}'");
            }
        }

        /// <summary>
        /// Function to search a given prefix in TST || Time O(L), L = length of prefix || Space O(1)
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool SearchPrefix(string prefix)
        {
            // if current node == null return false || no word matching given Prefix exists
            // traverse thru each character and match it with node
            // a) matches go down
            // b) less than node data go left
            // c) larger than node data go right
            // reached end of prefix return true
            Console.Write($"Searching for Prefix '{prefix}' in Ternary Search Tree \t");
            var temp = root;
            var index = 0;
            while (index < prefix.Length)
            {
                if (temp == null) return false;                 // no word matching prefix exists
                else if (temp.data == prefix[index])
                {
                    temp = temp.equal; 
                    index++;                                    // increment index only match word match
                }
                else if (prefix[index] < temp.data)
                    temp = temp.left;
                else if (prefix[index] > temp.data)
                    temp = temp.right;
            }
            Console.WriteLine($" Prefix Match \t'{prefix}' exists in Trie");
            return true;
        }

        /// <summary>
        /// Function to search a given word in TST || Time O(L), L = length of word || Space O(1)
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool SearchWord(string word)
        {
            Console.Write($"Searching for Word '{word}' in Ternary Search Tree \t");
            var temp = root;
            TSTNode previous = null;
            var index = 0;
            while (index < word.Length)
            {
                previous = temp;
                if (temp == null) return false;                // no word matching prefix exists
                else if (temp.data == word[index])
                { temp = temp.equal; index++; }
                else if (word[index] < temp.data)
                    temp = temp.left;
                else if (word[index] > temp.data)
                    temp = temp.right;
            }
            if (previous.isEndOfWord) Console.WriteLine($" Word Match \t'{word}' found in Trie");
            return previous.isEndOfWord;
        }

        // https://youtu.be/bWaXdxEHaag?list=PLGeS6vuu3w_mM_n2nRYlgrI85nUMrfOK_
        /// <summary>
        /// Traverse and print all the words in lexographical order in Ternary Search Tree
        /// Time O(N), N = no of nodes in TST || Space O(1)
        /// </summary>
        /// <param name="current"></param>
        /// <param name="lastWord"></param>
        public void TraverseTST(TSTNode current, string lastWord = null)
        {
            // if current node is null return
            // if isEndOFString == True, print 'lastWord + current.data'
            // a) call Traverse(current.left,lastWord)
            // b) call Traverse(current.equal,lastWord + current.data)
            // c) call Traverse(current.right,lastWord)
            if (current == null) return;
            if (current.isEndOfWord == true)
                Console.Write($" {lastWord + current.data} >>");

            // traverse left subtree
            TraverseTST(current.left, lastWord);
            // traverse equal subtree
            TraverseTST(current.equal, lastWord + current.data);
            // traverse right subtree
            TraverseTST(current.right, lastWord);
        }

        /// <summary>
        /// Time Complexity O(N), N is no of nodes in TST || Auxillary Space O(1) || Recursion Stack Space O(height of TST)
        /// </summary>
        /// <param name="current"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public int LengthOfLargestWord(TSTNode current, int length = 0)
        {
            if (current == null) return 0;
            
            var leftLongest = LengthOfLargestWord(current.left, length);    // since we searching to smaller character we don't count current node character for largest word
            var middleLongest = LengthOfLargestWord(current.equal, length) + 1;
            var rtLongest = LengthOfLargestWord(current.right, length);     // since we searching to larger character we don't count current node character for largest word
            return Math.Max(leftLongest, Math.Max(middleLongest, rtLongest));
        }
    }
}
