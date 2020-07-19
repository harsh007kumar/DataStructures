using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    /// <summary>
    /// Commonly used Heap Operations are present in here
    /// </summary>
    public static class HeapUtility
    {
        public static void Print(string input = "") => Console.WriteLine($"\n========= {input} =========");

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

        public static MinHeap GetMinHeap()
        {
            MinHeap myHeap = new MinHeap(7);    // create inital Heap of Size 7
            myHeap.InsertNode(1);
            myHeap.InsertNode(2);
            myHeap.InsertNode(3);
            myHeap.InsertNode(15);
            myHeap.InsertNode(5);
            myHeap.InsertNode(4);
            //myHeap.InsertNode(45);
            return myHeap;
        }

        /// <summary>
        /// For given min heap, the maximum element will always be at leaf only.
        /// Time Complexity O(n/2) ~ O(n) || Space Complexity O(1)
        /// </summary>
        /// <param name="minHeap"></param>
        /// <returns></returns>
        public static int MaxInMinHeap(MinHeap minHeap)
        {
            if (minHeap == null) return -1;

            // Find the Parent of last Node
            var pIndex = minHeap.Count / 2 - 1;           // since left child = index*2 + 1 and rt child = index*2 + 2

            // Now we get Next Node to Parent as that would be the 1st Leaf Node
            var leaf = pIndex + 1;

            // Now just scan all the leaf Nodes and return Max
            int Max = int.MinValue;
            while (leaf < minHeap.Count)
            {
                Max = minHeap._heapArr[leaf] > Max ? minHeap._heapArr[leaf] : Max;
                leaf++;
            }
            return Max;
        }

        public static void Delete_iThIndexFromMinHeap(MinHeap m, int index)
        {
            if (m == null || index >= m.Count) return;
            Console.WriteLine($" Deleting Node '{m._heapArr[index]}' present at Index : {index}");
            m._heapArr[index] = m._heapArr[m.Count - 1];        // replace current node with last element in array
            m.Count--;                  // decrease count
            m.MinHeapify(index);                                // call Heapify on current Node to maintain Integrety from current Node to all the way down
        }

        /// <summary>
        /// Prints all Elements which have value less than K in "minHeap" || Time O(n) (worst case when all nodes have value less than K) || Space O(1)
        /// </summary>
        /// <param name="minH">underlying array of min Heap</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <param name="k"></param>
        public static void NodeSmallerThanK(int[] minH, int startIndex, int endIndex, int k)
        {
            if (startIndex <= endIndex && minH[startIndex] < k)
            {
                Console.Write($" >> {minH[startIndex]}");                       // print root
                NodeSmallerThanK(minH, LeftChild(startIndex), endIndex, k);     // call recursive func on left child
                NodeSmallerThanK(minH, RightChild(startIndex), endIndex, k);    // call recursive func on right child
            }
        }
    }
}
