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
            TernarySearchTree t = new TernarySearchTree();
            string[] words = { "books", "book", "boobs", "cat", "cats", "bug", "up" };
            foreach (var word in words)
                t.Insert(ref t.root, word);
            Console.ReadKey();
        }
    }
}
