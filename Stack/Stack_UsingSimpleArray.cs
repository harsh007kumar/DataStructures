using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    /// <summary>
    /// Simple array based implementation [UsingSimpleArray]
    /// Karumanchi, Narasimha.Data Structures and Algorithms Made Easy: Data Structures and Algorithmic Puzzles(p. 166). Kindle Edition.
    /// </summary>
    public class Stack
    {
        protected int[] arr;
        protected int Top { get; set; }
        protected int Capacity { get; set; }

        public Stack(int size)
        {
            arr = new int[size];
            Capacity = size;
            Top = -1;               // Indicates Stack Empty
        }

        /// <summary>
        /// Inserts new element on the top of stack(if not Full).
        /// </summary>
        /// <param name="data"></param>
        public void Push(int data)
        {
            if (Top == Capacity - 1) throw new Exception("STACK OVERFLOW EXCEPTION : Stack is Full");
            arr[++Top] = data;      // Increment the Top by 1 and insert data at new Top
        }

        /// <summary>
        /// Removes and returns the last inserted element from the stack.
        /// </summary>
        /// <returns></returns>
        public int Pop() => (Top > 0) ? arr[Top--] : throw new Exception("STACK UNDERFLOW EXCEPTION : Stack is Empty");

        public int Peek() => (Top > 0) ? arr[Top] : throw new Exception("STACK UNDERFLOW EXCEPTION : Stack is Empty");

        public int Size() => (Top < 0) ? 0 : Top + 1;

        public bool isEmptyStack() => Top < 0;

        public bool isFullStack() => Top < Capacity - 1;

        public void PrintStack()
        {
            for (int i = 0; i < Capacity; i++)
                Console.Write($"{arr[i]} || ");
        }
    }
}
