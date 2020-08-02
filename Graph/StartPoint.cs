using System;
using System.Collections.Generic;

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
            DijkstraAlgorithm_ForAdjacencyListRepresentation_GreedyAlgo();
            FindRelationsInAnimal();
            SingleSourceShortestPathInWeightedGraph_WithNegativeWt();
            MinimalSpanningTree_PrimsAlgo();
            MinimalSpanningTree_KruskalAlgo();
            ArticulationPointsORCutVerticesInAGraph();
            FindEulerianCircuit();
            DetectCycleInUnDirectedGraph_UsingBFS();
            DetectCycleInDirectedGraph_UsingDFS();
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
            GraphUtility.Print("Printing SCC in DiGraph (using Kosaraju’s algorithm)");
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

        // GFG https://www.geeksforgeeks.org/dijkstras-algorithm-for-adjacency-list-representation-greedy-algo-8/?ref=lbp
        // DataStructures & Algo Book : Dijkstra’s Algorithm (p. 451)
        public static void DijkstraAlgorithm_ForAdjacencyListRepresentation_GreedyAlgo()
        {
            GraphUtility.Print("DijkstraAlgorithm_ForAdjacencyListRepresentation_GreedyAlgo [Single Source Shortest Path, May/MayNot return Shortest Path if -ve Edges present]");
            //UnDirectedWeightedGraph graph = GraphUtility.GetUnDirectedWeighted();
            DiGraphWeighted graph = GraphUtility.GetDiGraphWeighted(); // pass this to see working on -> Directed Weighted Acyclic Graph
            graph.PrintGraph();
            int sourceNode = 0, destination = 4;
            GraphUtility.DijkstraAlgorithm(graph, sourceNode);      // if no destination is provided prints shortest path for all the nodes
        }

        // Google Phone Interview https://youtu.be/V0xjK_6ZoEY
        public static void FindRelationsInAnimal()
        {
            GraphUtility.Print("Google Telephone Interview Ques : Find Relations In Animal and Print the Herariechy");

            // input list containing pairs of animal
            List<Relation> input = Relation.GetRelations();

            // Objective is to write static func printTree() which print Relations : pair of parent->child relationships
            GraphUtility.FindAnimalKingdomTree(input);

        }

        public static void SingleSourceShortestPathInWeightedGraph_WithNegativeWt()
        {
            GraphUtility.Print("Bellman-Ford Algorithm (p. 456) [Single Source Shortest Path in Weighted Graph with Negative Wt (NO -ve Cycle]");
            DiGraphWeighted diGWN = GraphUtility.GetDiGraphWeightedWithNegativeEdges();
            diGWN.PrintGraph();
            int source = 0;
            GraphUtility.BellManFord_AdjacencyList(diGWN, source);

            // GFG https://www.geeksforgeeks.org/bellman-ford-algorithm-dp-23/
            // YouTube(Jenny) https://youtu.be/KudAWAMiQog
            GraphUtility.Print("Bellman-Ford Algo when graph given as pair of Edges(having src,Dest,Wt) [Single Source Shortest Path in Weighted Graph with Negative Wt (NO -ve Cycle]");
            GraphAsPairOfEdges graph = GraphAsPairOfEdges.GetGraph();
            //GraphAsPairOfEdges graph = GraphAsPairOfEdges.GetGraphWithNegativeWtCycle(); Uncomment this & comment above to check if modified BellMan detects -ve wt cycle
            graph.PrintGraph();
            GraphUtility.BellManFord_GraphAsPairOfEdges(graph, source);
        }

        // GFG https://www.geeksforgeeks.org/prims-minimum-spanning-tree-mst-greedy-algo-5/ Matrix
        // Youtube GFG https://www.youtube.com/watch?v=eB61LXLZVqs [Adjacancy Matrix]
        // Youtube Jenny https://www.youtube.com/watch?v=ZtZaR7EcI5Y
        // GFG https://www.geeksforgeeks.org/prims-mst-for-adjacency-list-representation-greedy-algo-6/ Adjacancy List
        public static void MinimalSpanningTree_PrimsAlgo()
        {
            GraphUtility.Print("Prim’s Algorithm [to find MST of a Graph] - Adjacancy Matrix representation");
            // Create 2-D array to represent Adjacancy Matrix (Graph)
            int[,] graph = new int[,] { { 0, 2, 0, 6, 0 },
                                      { 2, 0, 3, 8, 5 },
                                      { 0, 3, 0, 0, 7 },
                                      { 6, 8, 0, 0, 9 },
                                      { 0, 5, 7, 9, 0 } };
            GraphUtility.PrintMatrix(graph);
            GraphUtility.PrimAlgo_AdjacencyMatrix(graph);

            // GFG https://youtu.be/PzznKcMyu0Y [Adjacancy List]
            // Prim Algo for Adjacancy List representation of the graph
            GraphUtility.Print("Prim’s Algorithm [to find MST of a Graph] - Adjacancy List representation");
            UnDirectedWeightedGraph unDiW = new UnDirectedWeightedGraph(4);
            unDiW.AddEdge(0, 1, 10);
            unDiW.AddEdge(0, 2, 30);
            unDiW.AddEdge(0, 3, 15);
            unDiW.AddEdge(1, 2, 40);
            unDiW.AddEdge(2, 3, 50);
            unDiW.PrintGraph();
            GraphUtility.PrimAlgo_AdjacencyList(unDiW);
        }

        // https://youtu.be/wU6udHRIkcc?list=LLAMgEHd1K0kIEc0zSY6EO8g&t=919
        public static void MinimalSpanningTree_KruskalAlgo()
        {
            GraphUtility.Print("Kruskal’s Algorithm(p. 459)");
            // Sets representation is easier
        }

        // GFG https://www.geeksforgeeks.org/articulation-points-or-cut-vertices-in-a-graph/ [TarjanAlgorithm]
        public static void ArticulationPointsORCutVerticesInAGraph()
        {
            GraphUtility.Print("Problem - 14 DFS Application: Cut Vertex or Articulation Points(p. 471)");
            UnDirectedGraph UG = new UnDirectedGraph(5);
            UG.AddEdge(1, 0);
            UG.AddEdge(0, 2);
            UG.AddEdge(2, 1);
            UG.AddEdge(0, 3);
            UG.AddEdge(3, 4);
            UG.PrintGraph();
            GraphUtility.FindArticulartionPoint_TarjanAlgorithm(UG);
        }

        // Euler's Circuit/Tour a.k.a Hamiltonian Cycle
        public static void FindEulerianCircuit()
        {
            GraphUtility.Print("Eulerian circuit(p. 476)");
            DiGraph DG = new DiGraph(6);
            DG.AddEdge(0, 1);
            DG.AddEdge(1, 2);
            DG.AddEdge(1, 3);
            DG.AddEdge(2, 0);
            DG.AddEdge(2, 3);
            DG.AddEdge(3, 4);
            DG.AddEdge(3, 5);
            DG.AddEdge(4, 1);
            DG.AddEdge(4, 2);
            DG.AddEdge(5, 4);
            DG.PrintGraph();
            int source = 0;     // valid source for above graph can be either '0' or '5'
            GraphUtility.EulersCircuit(DG, source);
        }

        // Jenny https://youtu.be/vXrv3kruvwE
        public static void DetectCycleInUnDirectedGraph_UsingBFS()
        {
            GraphUtility.Print("Problem - 23 Detecting a cycle in an undirected graph(p. 485)");
            UnDirectedGraph UG = GraphUtility.GetUnDirectedGraph();
            UG.PrintGraph();
            GraphUtility.DetectCycleInUnDirectedGraph(UG);
        }

        // Jenny https://youtu.be/AK7BuT5MgU0
        // GFG https://www.geeksforgeeks.org/detect-cycle-in-a-graph/
        public static void DetectCycleInDirectedGraph_UsingDFS()
        {
            GraphUtility.Print("Problem - 24 Detecting a cycle in DAG(p. 485)");
            //var DG = GraphUtility.GetDiGraph();
            DiGraph DG = new DiGraph(5);
            DG.AddEdge(0, 1);
            DG.AddEdge(0, 2);
            DG.AddEdge(1, 2);
            DG.AddEdge(1, 3);
            DG.AddEdge(3, 4);
            DG.AddEdge(4, 1);
            DG.PrintGraph();
            /* flag
            -1 : not visited
            0 : visited and in stack
            1 : visited but not in stack
            */
            // initialize visited Flag as -1 for all Vertices
            int[] parent = new int[DG.NoOfVertex];
            DG.Reset_VisitedArr();
            for (int i = 0; i < DG.NoOfVertex; i++)
                DG._IsVisitedVertex[i] = parent[i] = -1;

            for (int i = 0; i < DG.NoOfVertex; i++)
                if (DG._IsVisitedVertex[i] == -1)
                    GraphUtility.DetectCycleInDiGraph(DG, ref parent);

        }
    }
}
