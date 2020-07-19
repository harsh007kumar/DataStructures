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
        public void Enqueue(int priority, int value)
        {
            int lastIndex = Count++;
            _arr[lastIndex] = new Node(priority, value);
            // 'Percolate-Up' Operation
            while (lastIndex > 0 && _arr[lastIndex].Priorty < _arr[HeapUtility.Parent(lastIndex)].Priorty)
            {
                Node.Swap(ref _arr[lastIndex], ref _arr[HeapUtility.Parent(lastIndex)]);
                lastIndex = HeapUtility.Parent(lastIndex);
            }
        }

        public Node GetHighestPriority() => Count == 0 ? null : _arr[0];

        public Node ExtractHighest()
        {
            if (Count <= 0) return null;

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
            while (index < Count)   // Instead of while we can call Heapify again at end of else statement on higherP to make Heapify Recursive from Iterative
            {
                if (HeapUtility.LeftChild(index) >= Count || Count <= 1)
                    return;
                else if (HeapUtility.RightChild(index) < Count)
                {
                    int higherP = index;
                    if (_arr[HeapUtility.LeftChild(index)].Priorty < _arr[index].Priorty)
                        higherP = HeapUtility.LeftChild(index);
                    if (_arr[HeapUtility.RightChild(index)].Priorty < _arr[higherP].Priorty)
                        higherP = HeapUtility.RightChild(index);

                    if (higherP != index)
                    {
                        Node.Swap(ref _arr[higherP], ref _arr[index]);
                        index = higherP;
                    }
                }
                else
                {
                    if (_arr[HeapUtility.LeftChild(index)].Priorty < _arr[index].Priorty)
                        Node.Swap(ref _arr[HeapUtility.LeftChild(index)], ref _arr[index]);
                    break;
                }
            }
        }
    }
}
