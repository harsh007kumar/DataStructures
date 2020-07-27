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
        public Dictionary<int,int> _positionDict;                  // position Dictonary which keep tracks of index of where each Key is present in Queue
        public int Count = 0;
        public int TotalCapacity { get => _arr.Length; }

        public QueueHeap(int size)
        {
            _arr = new Node[size];
            _positionDict = new Dictionary<int, int>();
        }

        public Node GetHighestPriority() => Count == 0 ? null : _arr[0];

        /// <summary>
        /// Swap values of index of Data in Dictonary
        /// </summary>
        /// <param name="index1"></param>
        /// <param name="index2"></param>
        protected void UpdatePosition(int index1, int index2)
        {
            _positionDict[_arr[index1].Key] = index2;
            _positionDict[_arr[index2].Key] = index1;
        }

        protected void UpdatePositionPriority(int oldPriority, int newPriority)
        {
            _positionDict.Add(newPriority, _positionDict[oldPriority]);
            _positionDict.Remove(oldPriority);
        }
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
            int index = Count++;
            _arr[index] = new Node(priority, value);
            _positionDict.Add(priority, index);                             // index of newly added Node is saved in Dictonary, would be used later to search/Update/Delete in O(1)
            // 'Percolate-Up' Operation
            while (index > 0 && _arr[index].Priorty < _arr[HeapUtility.Parent(index)].Priorty)
            {
                UpdatePosition(index, HeapUtility.Parent(index));
                Node.Swap(ref _arr[index], ref _arr[HeapUtility.Parent(index)]);
                index = HeapUtility.Parent(index);
            }
            return index;
        }

        public Node ExtractHighest()
        {
            if (Count < 1) return null;

            UpdatePosition(0, Count - 1);                                   // Updating index of Delete node and last Node in Dictonary, would be used later to search/Update/Delete in O(1)
            _positionDict.Remove(_arr[0].Key);                              // Delete Data which was stored earlier at _arr[0] as its going to be Extracted from PQ
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
                    UpdatePosition(maxPriority, index);     // index of two Node is updated/swapped in Dictonary, would be used later to search/Update/Delete in O(1)
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
        /// Time O(Logn) || Space O(1)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="newPriority"></param>
        public void UpdatePriority(int oldPriority, int newPriority = int.MinValue)
        {
            // Search not required now as _positionDict is tracking index of all the Data in the PriorityQueue
            //// Search and find the index of Node whose value matches the data
            //int Nodeindex = -1;
            //for (int index = 0; index < Count; index++)                         // O(n)
            //    if (_arr[index].Value == data)
            //    {
            //        Nodeindex = index;
            //        break;
            //    }
            var nodeIndex = _positionDict[oldPriority];
            UpdatePositionPriority(oldPriority, newPriority);       // update _positionDict here
            // Decrease the priority of Node
            if (_positionDict.ContainsKey(oldPriority)) DecreasePriority(nodeIndex, newPriority);      // O(LogN)
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
                UpdatePosition(index, HeapUtility.Parent(index));           // index of newly added Node is saved in Dictonary, would be used later to search/Update/Delete in O(1)
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
