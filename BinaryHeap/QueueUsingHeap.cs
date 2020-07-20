using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    public class QueueUsingHeap
    {
        public PriorityQueue pq;        // Min-Heap Implementation
        public int Count = 0;
        public QueueUsingHeap(int size) => pq = new PriorityQueue(size);    // Constructor

        /// <summary>
        /// Time Complexity O(logn) || Space Complexity O(1)
        /// Can use the current system time instead of Count (to avoid overflow). The implementation based on this can be given as pq.Enqueue(DateTime.UtcNow, data);
        /// </summary>
        /// <param name="data"></param>
        public void EnQueue(int data) => pq.Enqueue(Count++, data);

        public int Size() => Count + 1;

        /// <summary>
        /// Time Complexity O(logn) required to Heapify Queue after Removing Root || Space Complexity O(1)
        /// </summary>
        /// <returns></returns>
        public int DeQueue() 
        {
            var front = Count < 0 ? -1 : pq.ExtractHighest().Value;
            Count--;
            return front;
        }

        /// <summary>
        /// Time Complexity O(1) || Space Complexity O(1)
        /// </summary>
        /// <returns></returns>
        public int Front() => pq.GetHighestPriority().Value;
    }
}
