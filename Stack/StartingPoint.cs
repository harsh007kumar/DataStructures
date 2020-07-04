using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class StartingPoint
    {
        static void Main(string[] args)
        {
            Stack sOb = new Stack(10);
            for (int i = 10; i < 20; i++)
                sOb.Push(i);
            
            Console.WriteLine($"Size of current Stack is : {sOb.Size()}");
            Console.WriteLine($"Top Element(Peek) in Stack returned : {sOb.Peek()}");
            Console.WriteLine($"Is Stack Full : {sOb.IsFullStack()}");
            Console.WriteLine($"Is Stack Empty : {sOb.IsEmptyStack()}");
            Console.WriteLine("Printing Stack from Bottom To Top\n");
            sOb.PrintStack();

            Console.ReadKey();
        }
    }
}
