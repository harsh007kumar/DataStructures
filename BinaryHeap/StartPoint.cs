using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    // GFG referrence https://www.geeksforgeeks.org/binary-heap/
    public class StartPoint
    {
        static void Main(string[] args)
        {
            Create_MinHeap_Perform_Insert_Delete_ExtractMin();
            GivenMinHeap_AlgoToFindMaximum_FindAndDeleteAnNode_DeleteFromGivenIndex();
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
            Console.WriteLine($" Finding Max in above Min Heap, gives O/P :\t'{HeapUtility.GetMaxFromMinHeap(minH)}'");

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
    }
}