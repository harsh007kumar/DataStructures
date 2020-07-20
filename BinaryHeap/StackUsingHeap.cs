using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    class StackUsingHeap
    {
        public PriorityQueue pq;        // Min-Heap Implementation
        public int Count = 0;
        public StackUsingHeap(int size) => pq = new PriorityQueue(size);    // Const

        /// <summary>
        /// Time Complexity O(logn) || Space Complexity O(1)
        /// </summary>
        /// <param name="data"></param>
        public void Push(int data) => pq.Enqueue(-(Count++), data);

        public int Size() => Count + 1;

        /// <summary>
        /// Time Complexity O(logn) required to Heapify Queue after Removing Root || Space Complexity O(1)
        /// </summary>
        /// <returns></returns>
        public int Pop()
        {
            var top = Count < 0 ? -1 : pq.ExtractHighest().Value;
            Count--;
            return top;
        }

        /// <summary>
        /// Time Complexity O(1) || Space Complexity O(1)
        /// </summary>
        /// <returns></returns>
        public int Peek()
        {
            var top = Count == 0 ? -1 : pq.GetHighestPriority().Value;
            Count--;
            return top;
        }
    }
}
