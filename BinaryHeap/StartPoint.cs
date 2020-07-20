using System;

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
            Console.ReadKey();
        }

        public static void Create_MinHeap_Perform_Insert_Delete_ExtractMin()
        {
            HeapUtility.Print("Create an MinHeap and Perform :\tInsert()\tDelete()\tExtractMin()\tOperations");
            MinHeap myHeap = new MinHeap(2);
            myHeap.InsertNode(1,false);
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
    }
}