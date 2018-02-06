using System;

namespace BinaryTree
{
    class Tree
    {
        public int Data { get; set; }
        public Tree Left;
        public Tree Right;

        public Tree(int no)
        {
            Data = no;
            Left = Right = null;
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.ReadLine();
        }
    }
}
