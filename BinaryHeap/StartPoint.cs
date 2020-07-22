using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BinaryHeap
{
    // GFG referrence https://www.geeksforgeeks.org/binary-heap/
    public class StartPoint
    {
        static void Main(string[] args)
        {
            Create_MinHeap_Perform_Insert_Delete_ExtractMin();
            GivenMinHeap_AlgoToFindMaximum_FindAndDeleteAnNode_DeleteFromGivenIndex();
            NodesSmallerThanK();
            MergeTwoHeaps();
            FindKSmalledInMinHeap();
            ImplementStackUsingPriorityQueue();
            ImplementQueueUsingHeap();
            FindKMinFromUnSortedFileWithBillionNumbers();
            MergeKSortedLists();
            //MinMaxHeap();          IN PROGRESS
            FindMedianInUnSortedArrayUsingHeaps();
            Console.ReadKey();
        }

        public static void Create_MinHeap_Perform_Insert_Delete_ExtractMin()
        {
            HeapUtility.Print("Create an MinHeap and Perform :\tInsert()\tDelete()\tExtractMin()\tOperations");
            MinHeap myHeap = new MinHeap(2);
            myHeap.InsertNode(1, false);
            myHeap.InsertNode(2, false);
            myHeap.InsertNode(3, false);
            myHeap.DeleteKey(0);
            myHeap.InsertNode(15, false);
            myHeap.InsertNode(5, false);
            myHeap.InsertNode(4, false);
            myHeap.InsertNode(45, false);
            myHeap.PrintHeap();
            myHeap.GetMin();
            myHeap.IncreaseKey(0, 55);
            myHeap.PrintHeap();
            myHeap.ExtractMin();
            myHeap.GetMin();
            myHeap.DecreaseKey(2, 1);
            myHeap.GetMin();
        }

        public static void GivenMinHeap_AlgoToFindMaximum_FindAndDeleteAnNode_DeleteFromGivenIndex()
        {
            HeapUtility.Print("Problem - 7 Given a min - heap, give an algorithm for finding the maximum element.(p. 381)");
            var minH = HeapUtility.GetMinHeap();
            minH.PrintHeap();
            Console.WriteLine($" Finding Max in above Min Heap, gives O/P :\t'{HeapUtility.MaxInMinHeap(minH)}'");

            HeapUtility.Print("Problem - 8 Give an algorithm for deleting an arbitrary element from min heap.(p. 382)");
            minH.PrintHeap();
            Console.WriteLine($" Deleteing Node '15' from above Min Heap");
            var index = minH.FindIndexOfKey(15);
            minH.DeleteKey(index);
            minH.PrintHeap();

            HeapUtility.Print("Problem - 9 Give an algorithm for deleting the ith indexed element in a given min - heap.(p. 383)");
            minH.PrintHeap();
            Console.WriteLine($" Deleteing Index {index} from above Min Heap");
            HeapUtility.Delete_iThIndexFromMinHeap(minH, index);
            minH.PrintHeap();
        }

        public static void NodesSmallerThanK()
        {
            HeapUtility.Print("Problem - 11 Give an algorithm to find all elements less than some value of k in a binary heap.(p. 384)");
            var minH = HeapUtility.GetMinHeap();
            minH.PrintHeap();
            int k = 5;
            if (minH == null) return;
            Console.Write($" Print all elments with values less than given K : {k}\t\t");
            HeapUtility.NodeSmallerThanK(minH._heapArr, 0, minH.Count - 1, k);
            Console.WriteLine();
        }

        public static void MergeTwoHeaps()
        {
            HeapUtility.Print("Problem - 12 Give an algorithm for merging two binary heaps." +
                "Let us assume that the size of the first heap is m + n (1st m filled n last n index empty) and the size of the second heap is n.(p. 384)");
            int m = 10, n = 3, value = 0;
            var mnHeap = new MinHeap(m + n);
            var nHeap = new MinHeap(n);

            // Populate 1st Heap
            while (value < m)
                mnHeap.InsertNode(value++);
            Console.Write("\nPrint MN Heap :\t");
            mnHeap.PrintHeap();

            // Populate 2nd Heap
            while (value < m + n)
                nHeap.InsertNode(value++);
            Console.Write("Print N Heap :\t");
            nHeap.PrintHeap();

            // Merging Above two Min Heaps
            value = 0;
            while (value < nHeap.Count)
                mnHeap.InsertNode(nHeap._heapArr[value++]);
            Console.Write("Print Merged Heap :\t");
            mnHeap.PrintHeap();
        }

        public static void FindKSmalledInMinHeap()
        {
            HeapUtility.Print("Problem - 16 Give an algorithm for finding the kth smallest element in min - heap.(p. 385)");
            var minH = HeapUtility.GetMinHeap();
            minH.PrintHeap();
            int k = 6;
            Console.WriteLine($" {k}th smallest element in above Heap is :\t {HeapUtility.KthSmallestInMinHeap(minH, k)}");
        }

        public static void ImplementStackUsingPriorityQueue()
        {
            HeapUtility.Print("Problem - 19 How do we implement stack using heap?(p. 387)");
            StackUsingHeap st = new StackUsingHeap(10);
            for (int i = 100; i < 110; i++)
                st.Push(i);

            Console.WriteLine($" Peek on Above Stack returned :\t\t {st.Peek()}");
            Console.Write(" Performing Pop on are stack now :\t");
            for (int j = 0; j < 10; j++)
                Console.Write($" {st.Pop()}");
            Console.WriteLine();
        }

        public static void ImplementQueueUsingHeap()
        {
            HeapUtility.Print("Problem - 19 How do we implement Queue using heap?(p. 387)");
            QueueUsingHeap q = new QueueUsingHeap(10);
            for (int i = 100; i < 110; i++)
                q.EnQueue(i);

            Console.WriteLine($" Front on Above Queue returned :\t {q.Front()}");
            Console.Write(" Performing DeQueue on are stack now :\t");
            for (int j = 0; j < 10; j++)
                Console.Write($" {q.DeQueue()}");
            Console.WriteLine();
        }

        /// <summary>
        /// Time Complexity O(n) = N/M times * M Heapify Operation
        /// N/1000 * 1000 Log 1000 ~ N Log 1000 ~ N, Since complexity of heap sorting M = 1000 elements will be constant thats why Linear Time
        /// Space Complexity O(m) where m = size of Heap which is constant hence ~ O(1)
        /// </summary>
        public static void FindKMinFromUnSortedFileWithBillionNumbers()
        {
            HeapUtility.Print("Problem - 21 Given a big file containing billions of numbers, how can you find the 10 minmum numbers from that file? (p. 388)");
            int k = 10;

            // Step 1 : Divide the Read Big file into some constants parts lets say 1000 elements
            MinHeap heap1000Chunk = new MinHeap(1000);
            for (int i = 1; i <= (int)Math.Pow(10, 5); i++)          // Consider Billion Numbers Input Stream coming out of this Loop
            {
                if (heap1000Chunk.Count < heap1000Chunk.TotalCapacity)
                    heap1000Chunk.InsertNode(i, true);
                else            // After Inserting last element of Heap Capacity follow below extra Steps
                {
                    // Pop and store top k = 10 minimum element from arr1000Chunk
                    int[] kElemenet = new int[k];
                    for (int j = 0; j < k; j++)
                        kElemenet[j] = heap1000Chunk.ExtractMin(true);

                    // Delete current Heap to make space for inserting next set of 1000 - k elements;
                    heap1000Chunk.DeleteHeap(true);

                    // Copy current top 10 to Heap before adding next set of 1000 - k ( i.e, 990 elements)
                    for (int j = 0; j < k; j++)
                        heap1000Chunk.InsertNode(kElemenet[j], true);
                }
            }
            Console.Write($" Top {k} Minimum elements are :\t");
            for (int i = 0; i < k; i++)
                Console.Write($" {heap1000Chunk.ExtractMin(true)}");        // pass false to see last set of 1000 no's getting replaced in Extract Operation

            Console.WriteLine();
        }

        public static void MergeKSortedLists()
        {
            HeapUtility.Print("Problem - 22 Merge k sorted lists of Size N each (p. 390)");
            int k = 3;  // Number of linked lists 
            int n = 4;  // Number of elements in each list 
            LinkedList.Node[] arrOfKList = new LinkedList.Node[k];
            arrOfKList[0] = new LinkedList.Node(1);
            arrOfKList[0].next = new LinkedList.Node(3);
            arrOfKList[0].next.next = new LinkedList.Node(5);
            arrOfKList[0].next.next.next = new LinkedList.Node(7);

            arrOfKList[1] = new LinkedList.Node(2);
            arrOfKList[1].next = new LinkedList.Node(4);
            arrOfKList[1].next.next = new LinkedList.Node(6);
            arrOfKList[1].next.next.next = new LinkedList.Node(8);

            arrOfKList[2] = new LinkedList.Node(0);
            arrOfKList[2].next = new LinkedList.Node(9);
            arrOfKList[2].next.next = new LinkedList.Node(10);
            arrOfKList[2].next.next.next = new LinkedList.Node(11);

            for (int i = 0; i < k; i++)
                Print(arrOfKList[i]);

            LinkedList.Node merged = HeapUtility.MergeKSortedList(arrOfKList);

            Console.Write("Printing Merged List :\t");
            Print(merged);
            // Local Func
            void Print(LinkedList.Node temp)
            {
                while (temp != null)
                {
                    Console.Write($" {temp.Data}");
                    temp = temp.next;
                }
                Console.WriteLine();
            }
        }

        public static void MinMaxHeap()
        {
            HeapUtility.Print("Problem - 26 Min - Max heap: (p. 391)");
            // Expected Operation Complexity Init O(n), Insert O(logn), FindMin O(1), FindMax O(1), Delete Min O(logn), Delete Max O(logn)
            int size = 5;
            MinMaxHeap mnHeap = new MinMaxHeap(size);
            for (int i = 1; i <= size; i++)
                mnHeap.Insert(i * 100);
            mnHeap.Print();

            Console.WriteLine($" Get Max in O(1) time returns :\t{mnHeap.FindMax()}");
            Console.WriteLine($" Get Min in O(1) time returns :\t{mnHeap.FindMin()}");

            Console.Write("After deleting Min DataStructure Looks like");
            mnHeap.DeleteMin();
            mnHeap.Print();

            Console.Write("After deleting Max DataStructure Looks like");
            mnHeap.DeleteMax();
            mnHeap.Print();
        }

        public static void FindMedianInUnSortedArrayUsingHeaps()
        {
            HeapUtility.Print("Problem - 27 Dynamic median finding.(p. 392)");
            int[][] inputArr = { new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 }, new int[] { 10, 5, 15, 2, 30, 100, 14, 32, 20, 1 } };
            foreach (var input in inputArr)
            {
                // Print Input
                Console.Write("\n Input Array is :\t\t");
                for (int i = 0; i < input.Length; i++)
                    Console.Write($" {input[i]}\t");

                double[] mediam = HeapUtility.DynamicMedianFinding(input);

                // Print Output
                Console.Write("\n Median at each point in array :");
                for (int i = 0; i < mediam.Length; i++)
                    Console.Write($" {mediam[i]}\t");
                
                Console.WriteLine();
            }
        }
    }
}