using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    public class Node
    {
        public int Priorty { get; set; }
        public int Key { get => Priorty; set { Priorty = value; } }     // added extra property with Key name as its more intutiate to fetch data by Key instead of Priority

        public int Value { get; set; }
        public Node(int priority, int value)
        {
            Priorty = priority;
            Value = value;
        }

        public static void Swap(ref Node n1, ref Node n2)
        {
            var temp = n1;
            n1 = n2;
            n2 = temp;
        }
    }

    /// <summary>
    /// Highest Priority in this implementation is int.MinValue, so lesser the value Higher the priority
    /// </summary>
    public class PriorityQueue
    {
        public Node[] _arr;
        public int Count = 0;
        public int TotalCapacity { get => _arr.Length; }
        public PriorityQueue(int size) => _arr = new Node[size];

        /// <summary>
        /// Time Complexity O(Logn) as we travel from leaf to root node || Space Complexity O(1)
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="value"></param>
        public int Enqueue(int priority, int value)
        {
            int lastIndex = Count++;
            _arr[lastIndex] = new Node(priority, value);
            // 'Percolate-Up' Operation
            while (lastIndex > 0 && _arr[lastIndex].Priorty < _arr[HeapUtility.Parent(lastIndex)].Priorty)
            {
                Node.Swap(ref _arr[lastIndex], ref _arr[HeapUtility.Parent(lastIndex)]);
                lastIndex = HeapUtility.Parent(lastIndex);
            }
            return lastIndex;
        }

        public Node GetHighestPriority() => Count == 0 ? null : _arr[0];

        public Node ExtractHighest()
        {
            if (Count < 1) return null;

            var Top = _arr[0];              // Fetch Highest Priority
            _arr[0] = _arr[Count - 1];      // Assign last Node to Top
            Count--;                        // Decrease count
            // Heapify : to Mainting Queue integrity
            Heapify();
            return Top;
        }

        /// <summary>
        /// Time Complexity O(LogN), Logn = Height of Tree || Space Complexity O(1)
        /// </summary>
        /// <param name="index"></param>
        public void Heapify(int index = 0)
        {
            while (index < Count)   // Instead of while we can call Heapify again after Swapping on higherP to make Heapify Recursive from Iterative
            {
                int left = HeapUtility.LeftChild(index), rt = HeapUtility.RightChild(index);
                int maxPriority = index;
                if (left < Count && _arr[left].Priorty < _arr[index].Priorty)
                    maxPriority = left;
                if (rt < Count && _arr[rt].Priorty < _arr[maxPriority].Priorty)
                    maxPriority = rt;

                if (maxPriority != index)
                {
                    Node.Swap(ref _arr[maxPriority], ref _arr[index]);
                    index = maxPriority;
                }
                else break;
            }
        }

        public void Delete_iThIndex(int index)
        {
            if (index >= Count) return;
            Console.WriteLine($" Deleting Node '{_arr[index]}' present at Index : {index}");
            _arr[index] = _arr[Count - 1];          // replace current node with last element in array
            Count--;                                // decrease count
            Heapify(index);                         // call Heapify on current Node to maintain Integrety from current Node to all the way down
        }
    }

    public class PriorityQueueMax : PriorityQueue
    {
        public PriorityQueueMax(int size) : base(size) { }

        public new int Enqueue(int priority, int value)
        {
            int lastIndex = Count++;
            _arr[lastIndex] = new Node(priority, value);
            // 'Percolate-Up' Operation
            while (lastIndex > 0 && _arr[lastIndex].Priorty > _arr[HeapUtility.Parent(lastIndex)].Priorty)
            {
                Node.Swap(ref _arr[lastIndex], ref _arr[HeapUtility.Parent(lastIndex)]);
                lastIndex = HeapUtility.Parent(lastIndex);
            }
            return lastIndex;
        }

        public new void Heapify(int index = 0)
        {
            while (index < Count)   // Instead of while we can call Heapify again after Swapping on higherP to make Heapify Recursive from Iterative
            {
                int left = HeapUtility.LeftChild(index), rt = HeapUtility.RightChild(index);
                int maxPriority = index;
                if (left < Count && _arr[left].Priorty > _arr[index].Priorty)
                    maxPriority = left;
                if (rt < Count && _arr[rt].Priorty > _arr[maxPriority].Priorty)
                    maxPriority = rt;

                if (maxPriority != index)
                {
                    Node.Swap(ref _arr[maxPriority], ref _arr[index]);
                    index = maxPriority;
                }
                else break;
            }
        }

    }
}
