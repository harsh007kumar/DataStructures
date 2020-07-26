using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryHeap;

namespace Graph
{
    //GFG Link https://www.geeksforgeeks.org/graph-and-its-representations/
    class EntryPoint
    {
        static void Main(string[] args)
        {
            CreateAndPerform_DFS_BFS_On_UnDirectedGraph();
            CreateAndPerform_DFS_BFS_On_DiGraph();
            StronglyConnectedCompoenent_In_DiGraph();
            TopologicalSorting_In_DAG();
            ShortestPathInUnWeightedGraph();
            Console.ReadKey();
        }

        public static void CreateAndPerform_DFS_BFS_On_UnDirectedGraph()
        {
            GraphUtility.Print("UnDirected-Graph");

            UnDirectedGraph UG = GraphUtility.GetUnDirectedGraph();
            UG.PrintGraph();
            UG.BreadthFirstSearch(2);
            //Removing Edges from Un-Directed Graph
            UG.RemoveEdge(1, 2);
            UG.RemoveEdge(1, 3);
            UG.RemoveEdge(1, 4);
            UG.PrintGraph();
            UG.BreadthFirstSearch(2);
        }

        public static void CreateAndPerform_DFS_BFS_On_DiGraph()
        {
            GraphUtility.Print("Directed-Graph ");

            DiGraph DG = GraphUtility.GetDiGraph();
            DG.PrintGraph();
            DG.BreadthFirstSearch(2);
            DG.DepthFirstSearch();
        }

        // GFG https://www.geeksforgeeks.org/strongly-connected-components/
        public static void StronglyConnectedCompoenent_In_DiGraph()
        {
            GraphUtility.Print("Printing SCC in DiGraph (using Kosarajuís algorithm)");
            DiGraph DG1 = new DiGraph(5);
            DG1.AddEdge(1, 0);
            DG1.AddEdge(0, 2);
            DG1.AddEdge(2, 1);
            DG1.AddEdge(0, 3);
            DG1.AddEdge(3, 4);
            GraphUtility.PrintSccInDiGraph(DG1);
        }

        // GFG https://www.geeksforgeeks.org/topological-sorting/
        public static void TopologicalSorting_In_DAG()
        {
            GraphUtility.Print("Topological Sorting in Directed Acyclic Graph aka. DAG");
            var DAG = GraphUtility.GetDAG();
            DAG.DepthFirstSearch();
            GraphUtility.TopologicalSortingOnDAG(DAG);
        }

        // GFG https://www.geeksforgeeks.org/shortest-path-unweighted-graph/
        public static void ShortestPathInUnWeightedGraph()
        {
            GraphUtility.Print("Shortest Path in Unweighted Graph(p. 448)");
            UnDirectedGraph UG = new UnDirectedGraph(8);
            UG.AddEdge(2, 1);
            UG.AddEdge(1, 0);
            UG.AddEdge(0, 3);
            UG.AddEdge(3, 7);
            UG.AddEdge(3, 4);
            UG.AddEdge(4, 6);
            UG.AddEdge(4, 5);
            UG.AddEdge(5, 6);
            UG.AddEdge(7, 4);
            UG.AddEdge(7, 6);
            UG.PrintGraph();
            int source = 0, destination = 6;
            GraphUtility.ShortestPathUnWeighted(UG, source, destination);

        }

        


    }
}
