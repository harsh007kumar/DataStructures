using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    public class MinHeap
    {
        // array which will represent and hold are minHeap
        public int[] _heapArr;
        public int Count = 0;

        /// <summary>
        ///  Parametrized Constructor for MinHeap which take only single argument "size" of type int to initialize array of that size.
        /// </summary>
        /// <param name="size"></param>
        public MinHeap(int size) => _heapArr = new int[size];

        // Main Operation

        /// <summary>
        /// Returns SizeOfHeap
        /// </summary>
        public int TotalCapacity => _heapArr.Length;

        /// <summary>
        /// Inserts new node in MinHeap || also called 'Percolate-Up' Operation
        /// Time Complexity O(Logn) as we travel from leaf to root node || Space Complexity O(1)
        /// </summary>
        /// <param name="value"></param>
        public void InsertNode(int value, bool silentMode = true)
        {
            if (TotalCapacity == Count)
            {
                Console.WriteLine(" Current Heap is Full");
                ResizeHeap();
            }
            if (!silentMode) Console.WriteLine($" Adding '{value}' to Heap");

            // increasing heapSize post fetching current avaliable index
            int index = Count++;
            // Insert new value at the last index in array
            _heapArr[index] = value;
            // Now compare with parent and keep updating
            while (index != 0 && _heapArr[HeapUtility.Parent(index)] > _heapArr[index])
            {
                HeapUtility.Swap(ref _heapArr[HeapUtility.Parent(index)], ref _heapArr[index]);
                index = HeapUtility.Parent(index);
            }

        }

        /// <summary>
        /// Deletes the minimum element from MinHeap i.e, Root Node and calls Heapify to re-balance the Heap
        /// Time Complexity O(logn) & Space Complexity O(logn) Because of Heapify func() call
        /// </summary>
        public void ExtractMin()
        {
            if (Count == 0)
                Console.WriteLine("Cannot remove as Heap is empty");
            else
            {
                Console.WriteLine($" Replacing root key : '{_heapArr[0]}' with Key present at Last Index {Count - 1} : having value : '{_heapArr[Count - 1]}'");
                //assigning last variable in array to top node
                _heapArr[0] = _heapArr[Count - 1];
                //deleteing last variable by reducing the 6capacity
                Count--;
                // Mainting Heap integrity
                MinHeapify();
            }
        }

        /// <summary>
        /// Re-balances the Heap from Parent Node, assuming all child nodes are already balanced and do not violate Heap integrity
        /// Time Complexity O(LogN), Logn = Height of Tree || Space Complexity O(logn) for Recursion Stack
        /// also called 'Percolate-Down' Operation
        /// </summary>
        /// <param name="index">When Heapfying from root 0th index is passed as default</param>
        public void MinHeapify(int index = 0)
        {
            //For empty or Heap with single element we need not perform any operation
            if (Count < 2 || HeapUtility.LeftChild(index) > Count - 1)
                return;
            else if (HeapUtility.RightChild(index) > Count - 1)
            {
                if (_heapArr[index] > _heapArr[HeapUtility.LeftChild(index)])
                    HeapUtility.Swap(ref _heapArr[index], ref _heapArr[HeapUtility.LeftChild(index)]);
            }
            else
            {
                int smallest = index;
                if (_heapArr[HeapUtility.LeftChild(index)] < _heapArr[index])
                    smallest = HeapUtility.LeftChild(index);
                if (_heapArr[HeapUtility.RightChild(index)] < _heapArr[smallest])
                    smallest = HeapUtility.RightChild(index);
                if (smallest != index)
                {
                    // Swap root node with bigger child
                    HeapUtility.Swap(ref _heapArr[smallest], ref _heapArr[index]);
                    //Recurisvely call Heapify on child node to re-order subtree and maintain MinHeap integrity
                    MinHeapify(smallest);
                }
            }
        }

        /// <summary>
        /// Returns value of root node i.e. Min value in MinHeap or Max value in MaxHeap || Time Complexity O(1)
        /// </summary>
        /// <returns></returns>
        public void GetMin()
        {
            if (Count == 0) Console.WriteLine($" Heap is Empty");
            else Console.WriteLine($" Current Heap Min is {_heapArr[0]}");
        }

        // Extra Operations

        /// <summary>
        /// Decreases value at passed index in Min Heap
        /// Time Complexity O(logN) (worst case we start at the leaf and come up to the root) || Space O(1)
        /// We need not worry about the Heap Integrity from that node downwards as value is decreasing even more and not increasing
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue">default value is int.MinValue</param>
        public void DecreaseKey(int index, int newValue = int.MinValue)
        {
            Console.WriteLine($" Decreasing key value at index : {index} having value : '{_heapArr[index]}' with New Value : {newValue}'");
            _heapArr[index] = newValue;
            while (index != 0 && _heapArr[HeapUtility.Parent(index)] > _heapArr[index])
            {
                HeapUtility.Swap(ref _heapArr[HeapUtility.Parent(index)], ref _heapArr[index]);
                index = HeapUtility.Parent(index);
            }
        }

        /// <summary>
        /// Increases value at passed index in Min Heap
        /// Time Complexity O(logN) (worst case we start at the root and come down to the leaf) || Space O(1)
        /// We need not worry about the Heap Integrity from that node upwards as value is increasing even more and not decreasing
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue">default value is int.MaxValue</param>
        public void IncreaseKey(int index, int newValue = int.MaxValue)
        {
            Console.WriteLine($" Increasing value at Index : {index} from '{_heapArr[index]}' to '{newValue}' ");
            _heapArr[index] = newValue;
            while (index < Count)
            {
                var lt = HeapUtility.LeftChild(index);
                var rt = HeapUtility.RightChild(index);
                if (lt < Count && rt < Count)
                {
                    int smallerChild = _heapArr[lt] < _heapArr[rt] ? lt : rt;
                    // Swap root node with bigger child
                    HeapUtility.Swap(ref _heapArr[smallerChild], ref _heapArr[index]);
                    // Update index to bigger child to re-order subtree downwards and maintain MinHeap integrity
                    index = smallerChild;
                }
                else if (lt < Count - 1 && _heapArr[lt] < _heapArr[index])
                    HeapUtility.Swap(ref _heapArr[lt], ref _heapArr[index]);
                else
                    break;
            }
        }

        /// <summary>
        /// Deletes Key at passed index || Time Complexity O(logn) || Space O(logn)
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int DeleteKey(int index = 0)
        {
            if (index > Count - 1 || index < 0)
            {
                Console.WriteLine(" Cannot delete key from index which doesn't exists");
                return -1;
            }
            else
            {
                var root = _heapArr[0];
                Console.WriteLine($" Deleting key from Index : {index} with value : '{_heapArr[index]}' from Min-Heap");
                DecreaseKey(index);         // decrease key to min so it propogates to Top of MinHeap
                ExtractMin();               // Extract root node from MinHeap and Heapify the array
                return root;
            }
        }

        /// <summary>
        /// Time Cmplexity O(n) to search thru the Heap || Space O(1)
        /// </summary>
        /// <param name="keyBeingSearched"></param>
        /// <returns></returns>
        public int FindIndexOfKey(int keyBeingSearched)
        {
            for (int i = 0; i < Count; i++)
                if (_heapArr[i] == keyBeingSearched)
                    return i;
            return -1;
        }

        public void PrintHeap()
        {
            Console.Write($"\n Printing Heap from Top to Bottom in Level Order fashion :\t");
            for (int i = 0; i < Count; i++)
                Console.Write($" {_heapArr[i]}");
            Console.WriteLine();
        }

        public void ResizeHeap()
        {
            Console.WriteLine(" Doubling the current Heap Size");
            int[] _doubleHeapArr = new int[_heapArr.Length * 2];
            for (int i = 0; i < Count; i++)
                _doubleHeapArr[i] = _heapArr[i];

            _heapArr = _doubleHeapArr;
        }

    }
}
