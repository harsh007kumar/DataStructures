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
            myHeap.InsertNode(3);
            myHeap.InsertNode(2);
            myHeap.DeleteKey(1);
            myHeap.InsertNode(15);
            myHeap.InsertNode(5);
            myHeap.InsertNode(4);
            myHeap.InsertNode(45);
            myHeap.GetMin();
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
        private static int currentCapacity = 0;

        /// <summary>
        ///  Parametrized Constructor for MinHeap which take only single argument "size" of type int to initialize array of that size.
        /// </summary>
        /// <param name="size"></param>
        public MinHeap(int size) => _heapArr = new int[size];

        /// <summary>
        /// Returns SizeOfHeap
        /// </summary>
        public int TotalCapacity => _heapArr.Length;

        /// <summary>
        /// inserts new node in MinHeap
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
                // Here we are inserting new value at next avaliable index in array
                _heapArr[index] = value;
                while (index != 0 && _heapArr[Heap.Parent(index)] > _heapArr[index])
                {
                    Heap.Swap(ref _heapArr[Heap.Parent(index)], ref _heapArr[index]);
                    index = Heap.Parent(index);
                }
            }
        }

        /// <summary>
        /// decreases value at passed index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newValue">default value is int.MinValue</param>
        public void DecreaseKey(int index, int newValue=int.MinValue)
        {
            _heapArr[index] = newValue;
            while (index != 0 && _heapArr[Heap.Parent(index)] > _heapArr[index])
            {
                Heap.Swap(ref _heapArr[Heap.Parent(index)], ref _heapArr[index]);
                index = Heap.Parent(index);
            }
        }

        /// <summary>
        /// Deletes the minimum element from MinHeap i.e, Root Node and calls Heapify to re-balance the Heap. Time Complexity O(Logn)
        /// </summary>
        public void ExtractMin()
        {
            if (currentCapacity == 0)
                Console.WriteLine("Cannot remove as Heap is empty");
            else
            {
                //assigning last variable in array to top node
                _heapArr[0] = _heapArr[currentCapacity-1];
                //deleteing last variable by reducing the 6capacity
                currentCapacity--;
                // Mainting Heap integrity
                MinHeapify();
            }
        }

        /// <summary>
        /// Re-balances the Heap from Parent Node, assuming all child nodes are already balanced and do not violate Heap integrity
        /// </summary>
        /// <param name="index">When Heapfying from root 0th index is passed as default</param>
        public void MinHeapify(int index=0)
        {
            //For empty or Heap with single element we need not perform any operation
            if (currentCapacity < 2 || Heap.LeftChild(index)>currentCapacity-1 || Heap.RightChild(index) > currentCapacity-1)
                return;
            else
            {
                int i = index, smallest = index;
                if (_heapArr[Heap.LeftChild(i)] < _heapArr[i])
                    smallest = Heap.LeftChild(i);
                if (_heapArr[Heap.RightChild(i)] < _heapArr[smallest])
                    smallest = Heap.RightChild(i);
                if (smallest != i)
                {
                    // Swap root node with bigger child
                    Heap.Swap(ref _heapArr[smallest], ref _heapArr[i]);
                    //Recurisvely call Heapify on child node to re-order subtree and maintain MinHeap integrity
                    MinHeapify(smallest);
                }
            }
        }

        /// <summary>
        /// Deletes Key at passed index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool DeleteKey(int index)
        {
            if(index>currentCapacity-1)
            {
                Console.WriteLine("Cannot delete key from index which doesn't exists");
                return false;
            }
            else
            {
                DecreaseKey(index);
                ExtractMin();
                return true;
            }
        }

        /// <summary>
        /// Returns value of root node i.e. Min value in MinHeap or Max value in MaxHeap
        /// </summary>
        /// <returns></returns>
        public void GetMin() => Console.WriteLine($"current Min value is {_heapArr[0]}");

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
        /// Returns the index of parent node in an Heap
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int Parent(int index)
        { return (index - 1) / 2; }

        /// <summary>
        /// Returns the index of left child in an Heap
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int LeftChild(int index)
        { return (2 * index) + 1; }

        /// <summary>
        /// Returns the index of right child in an Heap
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int RightChild(int index)
        { return (2 * index) + 2; }

        public static void Swap(ref int i1, ref int i2)
        {
            int temp = i1;
            i1 = i2;
            i2 = temp;
        }

    }
}