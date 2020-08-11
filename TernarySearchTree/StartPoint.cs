using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TernarySearchTree
{
    class StartPoint
    {
        static void Main(string[] args)
        {
            // GFG https://www.geeksforgeeks.org/ternary-search-tree/
            // Video explains how it reads data https://youtu.be/bWaXdxEHaag?list=PLGeS6vuu3w_mM_n2nRYlgrI85nUMrfOK_
            TernarySearchTree t = new TernarySearchTree();
            string[] words = { "books", "book", "boobs", "cat", "cats", "bug", "up" };

            // Inserting words in Ternary Search Tree
            foreach (var word in words)
                t.Insert(ref t.root, word);

            // Searching in Ternary Search Tree
            foreach (var word in words)
            {
                t.SearchPrefix(word);
                t.SearchWord(word);
            }
            t.SearchWord("harsh");

            // traverse and print all words in TNT
            Console.WriteLine($"\nPrinting Dictonary/All Words in 'Ternary Search Tree' in Lexographical Order :");
            t.TraverseTNT(t.root);

            Console.ReadKey();
        }
    }
}
