using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tries
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> children;
        public bool isEndOfWord;
        public TrieNode() => children = new Dictionary<char, TrieNode>();
    }

    // also called digital tree or prefix tree
    /// <summary>
    /// Tries are an extremely special and useful data-structure that are based on the prefix of a string.
    /// They are used to represent the “Retrieval” of data and thus the name Trie.
    /// A Trie is a special data structure used to store strings that can be visualized like a graph.
    /// It consists of nodes and edges
    /// </summary>
    public class Trie
    {
        public TrieNode root;
        public Trie() => root = new TrieNode();

        /// <summary>
        /// Insert new word in the Trie DataStructure || Time O(L), L = length of the new word || Space O(1)
        /// </summary>
        /// <param name="newWord"></param>
        public void Insert(string newWord)
        {
            var temp = root;
            foreach(var ch in newWord.ToCharArray())
            {
                if (!temp.children.ContainsKey(ch))             // given character not present in childrens dictonary
                    temp.children.Add(ch, new TrieNode());
                temp = temp.children[ch];                       // navigate to Dictonary/Map of its childen
            }
            temp.isEndOfWord = true;                            // set true to mark end of the entire work
            Console.WriteLine($" NewWord Added \t'{newWord}'");
        }

        /// <summary>
        /// returns True if given prefix matches prefix of any word in Trie || Time O(L), L = length of prefix || Space O(1)
        /// Ex- if we have 'abcd' and we search for 'a' or 'ab' we should get 'true'
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool SearchPrefix(string prefix)
        {
            var temp = root;
            foreach(char ch in prefix.ToCharArray())
            {
                if (temp.children.ContainsKey(ch))
                    temp = temp.children[ch];
                else
                    return false;
            }
            Console.WriteLine($" Prefix Match \t'{prefix}' exists in Trie");
            return true;
        }

        /// <summary>
        /// returns True if given word matches excatly in Trie || Time O(L), L = length of prefix || Space O(1)
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public bool SearchWord(string word)
        {
            var temp = root;
            foreach (char ch in word.ToCharArray())
            {
                if (temp.children.ContainsKey(ch))
                    temp = temp.children[ch];
                else
                    return false;
            }
            Console.WriteLine($" Word Match \t'{word}' found in Trie");
            return temp.isEndOfWord;
        }

        public void DeleteWord(string word)
        {
            if (word == null || word == "" || word.Length < 1) return;
            Console.WriteLine($" Deleting \t'{word}' (if present in Trie)");
            DeleteRecursive(root, word.ToCharArray(), 0);
        }

        /// <summary>
        /// Time Complexity O(L), L = length of the word being deleted
        /// </summary>
        /// <param name="current"></param>
        /// <param name="charArr"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        protected bool DeleteRecursive(TrieNode current, char[] charArr, int index)
        {
            if (index < charArr.Length)
            {
                var ch = charArr[index];
                if (current.children.ContainsKey(ch))
                    if (DeleteRecursive(current.children[ch], charArr, index + 1))  // if current character child count is 0
                        current.children.Remove(ch);                                // delete child mapping
            }
            else if (index == charArr.Length && current.isEndOfWord)
                current.isEndOfWord = false;
            return current.children.Count == 0 && !current.isEndOfWord;
        }
    }
}
