using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    public class MinMaxHeap
    {
        public PriorityQueue min;
        public PriorityQueueMax max;

        public MinMaxHeap(int size)
        {
            min = new PriorityQueue(size);
            max = new PriorityQueueMax(size);
        }

        public void Insert(int data)
        {
            // fetch index at which element is added in min Heap
            var index = min.Enqueue(data, 1);

            // add element to MaxHeap with index of that element in MinHeap 
            // and also update the MinHeap element value with index from MaxHeap
            min._arr[index].Value = max.Enqueue(data, index);
            
        }

        public int FindMin() => min.GetHighestPriority().Priorty;
        public int FindMax() => max.GetHighestPriority().Priorty;

        public void DeleteMin()
        {
            var maxArrindex = min._arr[0].Value;
            min.Delete_iThIndex(0);
            max.Delete_iThIndex(maxArrindex);
        }
        public void DeleteMax()
        {
            var minArrindex = max._arr[0].Value;
            max.Delete_iThIndex(0);
            min.Delete_iThIndex(minArrindex);
        }

        public void Print()
        {
            Console.Write("Min Heap is :\t");
            for (int i = 0; i < min.Count; i++)
                Console.Write($" {min._arr[i].Key}");

            Console.Write("\nMax Heap is :\t");
            for (int i = 0; i < max.Count; i++)
                Console.Write($" {max._arr[i].Key}");

            Console.WriteLine();
        }
    }
}
