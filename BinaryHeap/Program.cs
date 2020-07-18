using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap.BinaryHeap
{
    // GFG referrence https://www.geeksforgeeks.org/binary-heap/
    class Program
    {
        static void Main(string[] args)
        {
            MinHeap myHeap = new MinHeap(11);
            myHeap.InsertNode(1);
            myHeap.InsertNode(2);
            myHeap.InsertNode(3);
            myHeap.DeleteKey(0);
            myHeap.InsertNode(15);
            myHeap.InsertNode(5);
            myHeap.InsertNode(4);
            myHeap.InsertNode(45);
            myHeap.PrintHeap();
            myHeap.GetMin();
            myHeap.IncreaseKey(0, 55);
            myHeap.PrintHeap();
            myHeap.ExtractMin();
            myHeap.GetMin();
            myHeap.DecreaseKey(2, 1);
            myHeap.GetMin();

            Console.ReadKey();
        }
    }

    public class MinHeap
    {
        // array which will represent and hold are minHeap
        private int[] _heapArr;
        private int currentCapacity = 0;

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
        public void InsertNode(int value)
        {
            if (TotalCapacity == currentCapacity)
                Console.WriteLine("Heap is Full");
            else
            {
                // increasing heapSize post fetching current avaliable index
                int index = currentCapacity++;
                // Insert new value at the last index in array
                _heapArr[index] = value;
                // Now compare with parent and keep updating
                while (index != 0 && _heapArr[Heap.Parent(index)] > _heapArr[index])
                {
                    Heap.Swap(ref _heapArr[Heap.Parent(index)], ref _heapArr[index]);
                    index = Heap.Parent(index);
                }
            }
        }

        /// <summary>
        /// Deletes the minimum element from MinHeap i.e, Root Node and calls Heapify to re-balance the Heap
        /// Time Complexity O(logn) & Space Complexity O(logn) Because of Heapify func() call
        /// </summary>
        public void ExtractMin()
        {
            if (currentCapacity == 0)
                Console.WriteLine("Cannot remove as Heap is empty");
            else
            {
                Console.WriteLine($" Replacing root key : '{_heapArr[0]}' with Key present at Last Index {currentCapacity - 1} : having value : '{_heapArr[currentCapacity-1]}'");
                //assigning last variable in array to top node
                _heapArr[0] = _heapArr[currentCapacity - 1];
                //deleteing last variable by reducing the 6capacity
                currentCapacity--;
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
            if (currentCapacity < 2 || Heap.LeftChild(index) > currentCapacity - 1)
                return;
            else if (Heap.RightChild(index) > currentCapacity - 1)
            {
                if (_heapArr[index] > _heapArr[Heap.LeftChild(index)])
                    Heap.Swap(ref _heapArr[index], ref _heapArr[Heap.LeftChild(index)]);
            }
            else
            {
                int smallest = index;
                if (_heapArr[Heap.LeftChild(index)] < _heapArr[index])
                    smallest = Heap.LeftChild(index);
                if (_heapArr[Heap.RightChild(index)] < _heapArr[smallest])
                    smallest = Heap.RightChild(index);
                if (smallest != index)
                {
                    // Swap root node with bigger child
                    Heap.Swap(ref _heapArr[smallest], ref _heapArr[index]);
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
            if (currentCapacity == 0) Console.WriteLine($" Heap is Empty");
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
            while (index != 0 && _heapArr[Heap.Parent(index)] > _heapArr[index])
            {
                Heap.Swap(ref _heapArr[Heap.Parent(index)], ref _heapArr[index]);
                index = Heap.Parent(index);
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
            while (index < currentCapacity)
            {
                var lt = Heap.LeftChild(index);
                var rt = Heap.RightChild(index);
                if (lt < currentCapacity && rt < currentCapacity)
                {
                    int smallerChild = _heapArr[lt] < _heapArr[rt] ? lt : rt;
                    // Swap root node with bigger child
                    Heap.Swap(ref _heapArr[smallerChild], ref _heapArr[index]);
                    // Update index to bigger child to re-order subtree downwards and maintain MinHeap integrity
                    index = smallerChild;
                }
                else if (lt < currentCapacity - 1 && _heapArr[lt] < _heapArr[index])
                    Heap.Swap(ref _heapArr[lt], ref _heapArr[index]);
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
            if (index > currentCapacity - 1)
            {
                Console.WriteLine(" Cannot delete key from index which doesn't exists");
                return -1;
            }
            else
            {
                var root = _heapArr[0];
                Console.WriteLine($" Deleting key from Index : {index} with value : '{_heapArr[index]}' from Min-Heap");
                DecreaseKey(index);
                ExtractMin();
                return root;
            }
        }

        public void PrintHeap()
        {
            Console.Write($"\n Printing Heap from Top to Bottom in Level Order fashion :\t");
            for (int i = 0; i < currentCapacity; i++)
                Console.Write($" {_heapArr[i]}");
            Console.WriteLine();
        }

    }
}

namespace Heap
{
    /// <summary>
    /// Commonly used Heap Operations are present in here
    /// </summary>
    public static class Heap
    {
        /// <summary>
        /// Returns the index of parent node in an Heap, 
        /// when calculating from left child gives excat Whole no, 
        /// and from rt child give value like 10.5, 25.5 which is rounded off to its floor value as we are using Integers datatype
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int Parent(int index) => (index - 1) / 2;

        /// <summary>
        /// Returns the index of left child in an Heap
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int LeftChild(int index) => (2 * index) + 1;

        /// <summary>
        /// Returns the index of right child in an Heap
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int RightChild(int index) => (2 * index) + 2;

        public static void Swap(ref int i1, ref int i2)
        {
            int temp = i1;
            i1 = i2;
            i2 = temp;
        }

    }
}