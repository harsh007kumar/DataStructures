using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tries
{
    class StartPoint
    {
        static void Main(string[] args)
        {
            Trie t = new Trie();
            t.Insert("habc");
            t.Insert("harsh");
            t.SearchPrefix("ab");   // expected False
            t.SearchPrefix("hab");  // expected True
            t.SearchWord("harsh"); // expected True
            t.DeleteWord("habc");
            t.DeleteWord("harsh");
            t.Insert("book");
            t.Insert("books");
            t.DeleteWord("books");
            t.SearchWord("book");    // expected True
            Console.ReadKey();
        }
    }
}
