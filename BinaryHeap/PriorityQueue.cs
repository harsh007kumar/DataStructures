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

        /// <summary>
        /// Added extra property with Key name as its more intutiate to fetch data by Key instead of Priority
        /// </summary>
        public int Key { get => Priorty; set { Priorty = value; } }

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

    public class QueueHeap
    {
        public Node[] _arr;
        public int Count = 0;
        public int TotalCapacity { get => _arr.Length; }
        public QueueHeap(int size) => _arr = new Node[size];
        public Node GetHighestPriority() => Count == 0 ? null : _arr[0];
    }
    /// <summary>
    /// Highest Priority in this implementation is int.MinValue, so lesser the value Higher the priority
    /// </summary>
    public class PriorityQueue : QueueHeap
    {
        public PriorityQueue(int size) : base(size) { }

        /// <summary>
        /// Time Complexity O(Logn) as we travel from leaf to root node || Space Complexity O(1)
        /// </summary>
        /// <param name="priority"></param>
        /// <param name="value"></param>
        public int Enqueue(int priority, int value = 1)
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

        /// <summary>
        /// Time O(n) || Space O(1)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="newPriority"></param>
        public void UpdatePriority(int data, int newPriority = int.MinValue)
        {
            int Nodeindex = -1;
            // Search and find the index of Node whose value matches the data
            for (int index = 0; index < Count; index++)                         // O(n)
                if (_arr[index].Value == data)
                {
                    Nodeindex = index;
                    break;
                }

            // Decrease the priority of Node
            if (Nodeindex != -1) DecreasePriority(Nodeindex, newPriority);      // O(LogN)
        }

        /// <summary>
        /// Decreases Priority at passed index in Min PriorityQueue
        /// Time Complexity O(logN) (worst case we start at the leaf and come up to the root) || Space O(1)
        /// We need not worry about the Heap Integrity from that node downwards as value is decreasing even more and not increasing
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue">default value is int.MinValue</param>
        public void DecreasePriority(int index, int priority = int.MinValue, bool silent = true)
        {
            if (!silent) Console.WriteLine($" Decreasing Priority at index : {index} having value : '{_arr[index].Value}' PrvPriority : '{_arr[index].Priorty }' NewPriority : '{priority}'");
            _arr[index].Priorty = priority;
            // Current Node Priority value is smaller than its parent than Swap (as its a Min Priority DataStructure)
            while (index != 0 && _arr[index].Priorty < _arr[HeapUtility.Parent(index)].Priorty)
            {
                Node.Swap(ref _arr[index], ref _arr[HeapUtility.Parent(index)]);
                index = HeapUtility.Parent(index);
            }
        }

    }

    /// <summary>
    /// Highest Priority in this implementation is int.MaxValue, so Higher the value Higher the priority
    /// </summary>
    public class PriorityQueueMax : QueueHeap
    {
        public PriorityQueueMax(int size) : base(size) { }

        public int Enqueue(int priority, int value = 1)
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

        public void Heapify(int index = 0)
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

        public void Delete_iThIndex(int index)
        {
            if (index >= Count) return;
            Console.WriteLine($" Deleting Node '{_arr[index]}' present at Index : {index}");
            _arr[index] = _arr[Count - 1];          // replace current node with last element in array
            Count--;                                // decrease count
            Heapify(index);                         // call Heapify on current Node to maintain Integrety from current Node to all the way down
        }

    }
}
