using BinaryHeap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public static class GraphUtility
    {
        public static void Print(string input = "") => Console.WriteLine($"\n========= {input} =========");

        public static UnDirectedGraph GetUnDirectedGraph()
        {
            UnDirectedGraph UG = new UnDirectedGraph(5);
            //Adding Edges to Un-Directed Graph
            UG.AddEdge(0, 1);
            UG.AddEdge(0, 4);
            UG.AddEdge(1, 2);
            UG.AddEdge(1, 3);
            UG.AddEdge(1, 4);
            UG.AddEdge(2, 3);
            UG.AddEdge(3, 4);
            return UG;
        }

        public static DiGraph GetDiGraph()
        {
            DiGraph DG = new DiGraph(4);

            //Adding Edges to Di-Graph
            DG.AddEdge(0, 1);
            DG.AddEdge(0, 2);
            DG.AddEdge(1, 2);
            DG.AddEdge(2, 0);
            DG.AddEdge(2, 3);
            DG.AddEdge(3, 3);

            return DG;
        }

        public static DiGraph GetDAG()
        {
            DiGraph DG = new DiGraph(6);

            //Adding Edges to Di-Graph
            DG.AddEdge(5, 0);
            DG.AddEdge(5, 2);
            DG.AddEdge(4, 0);
            DG.AddEdge(4, 1);
            DG.AddEdge(2, 3);
            DG.AddEdge(3, 1);

            return DG;
        }

        public static UnDirectedWeightedGraph GetUnDirectedWeighted()
        {
            UnDirectedWeightedGraph unDi = new UnDirectedWeightedGraph(9);
            unDi.AddEdge(0, 1, 4);
            unDi.AddEdge(0, 7, 8);
            unDi.AddEdge(1, 2, 8);
            unDi.AddEdge(1, 7, 11);
            unDi.AddEdge(2, 3, 7);
            unDi.AddEdge(2, 8, 2);
            unDi.AddEdge(2, 5, 4);
            unDi.AddEdge(3, 4, 9);
            unDi.AddEdge(3, 5, 14);
            unDi.AddEdge(4, 5, 10);
            unDi.AddEdge(5, 6, 2);
            unDi.AddEdge(6, 7, 1);
            unDi.AddEdge(6, 8, 6);
            unDi.AddEdge(7, 8, 7);

            return unDi;
        }

        public static DiGraphWeighted GetDiGraphWeighted()
        {
            DiGraphWeighted diG = new DiGraphWeighted(5);
            diG.AddEdge(0, 1, 4);
            diG.AddEdge(0, 2, 1);
            diG.AddEdge(1, 4, 4);
            diG.AddEdge(2, 1, 2);
            diG.AddEdge(2, 3, 4);
            diG.AddEdge(3, 4, 4);

            return diG;
        }


        /// <summary>
        /// Printing SCC in DiGraph (using Kosaraju’s algorithm)
        /// </summary>
        public static void PrintSccInDiGraph(DiGraph graphObj)
        {
            if (graphObj?._Graph == null) return;

            int Len = graphObj.Length;

            //Step1: Create a stack to hold the nodes after each iteration of DFS
            Stack<int> s = new Stack<int>(Len);

            //Step2: run DFS & store vertices according to their finish times in stack.
            graphObj.Reset_VisitedArr();                                                   // Reset Visited vertices Array 

            Console.Write("\nDFS of current Graph : ");
            for (int index = 0; index < Len; index++)
                if (graphObj._IsVisitedArr[index] != 1)
                    FillStackUsingRecursiveDFS(graphObj, index, ref s);                    // Time complexity O(V+E) 

            //Step3: Reverse all directed Edges in Di-Graph || so that 'Source' becomes 'sink' and the 'Sink' becomes 'source'.
            DiGraph reverse = GetTranspose(graphObj);                                      // Time complexity O(V+E)

            //Step4: Run DFS of the reversed graph using sequence of vertices in stack (process Vertices from Sink to Source)
            reverse.Reset_VisitedArr();                                             // Reset Visited vertices Array 
            Console.Write("\n\nPrinting All Strongly Connected Components in Di-Graph (using Kosaraju’s algorithm) below :");
            foreach (var node in s)
                if (reverse._IsVisitedArr[node] != 1)
                {
                    Console.Write("\nSCC : ");
                    reverse.DepthFirstSearch_Recursive(node); //Base class method   // Time complexity O(V+E)
                }
            Console.WriteLine();
        }

        public static void FillStackUsingRecursiveDFS(DiGraph graph, int startingNode, ref Stack<int> stack)
        {
            if (graph == null || graph._IsVisitedArr[startingNode] == 1) return;

            //Step1: Mark current node as visited and print the node
            graph._IsVisitedArr[startingNode] = 1;
            Console.Write($" {startingNode} ");

            //Step2 : Traverse all the adjacent and unmarked nodes
            foreach (var node in graph._Graph[startingNode])
                FillStackUsingRecursiveDFS(graph, node, ref stack);

            //Step3: add starting node to Stack after traversing thru its connected nodes
            stack.Push(startingNode);
        }

        public static DiGraph GetTranspose(DiGraph graph)
        {
            var Len = graph.Length;
            DiGraph reverseGraph = new DiGraph(Len);
            for (int i = 0; i < Len; i++)
                foreach (var node in graph._Graph[i])
                    reverseGraph._Graph[node].Add(i);
            return reverseGraph;
        }

        /// <summary>
        /// Time Complexity: O(V+E) its simply DFS with an extra stack, complexity same as DFS || Auxiliary space: O(V) needed for the stack.
        /// </summary>
        /// <param name="DG"></param>
        public static void TopologicalSortingOnDAG(DiGraph DG)
        {
            // Step1 create a stack to hold the Vertex in the reverse order of when they appear in call-stack
            Stack<int> st = new Stack<int>();

            // Reset InVisited Arr
            DG.Reset_VisitedArr();

            //Calling recursive TopologicalSort on graph for every node to cover disconnected graph (in which every is not reachble from single Node)
            for (int startingNode = 0; startingNode < DG.Length; startingNode++)
                if (DG._IsVisitedArr[startingNode] != 1)
                    TopologicalSort_Recursive(DG, startingNode, ref st);

            Console.Write("\nTopological Sort of above DAG is :\t");
            foreach (var Vertex in st)
                Console.Write($" {Vertex} ");

            Console.WriteLine();
        }

        static void TopologicalSort_Recursive(DiGraph DG, int currentNodeIndex, ref Stack<int> stack)
        {
            if (DG?._Graph == null || DG._IsVisitedArr[currentNodeIndex] == 1) return;

            // Mark current Node as Visited Now
            DG._IsVisitedArr[currentNodeIndex] = 1;

            foreach (var adjacentNode in DG._Graph[currentNodeIndex])
                TopologicalSort_Recursive(DG, adjacentNode, ref stack);

            // add current Node to stack after adding all its adjacent Node(adjacent Nodes) to stack i.e, Current Node is added to top of stack
            stack.Push(currentNodeIndex);
        }

        /// <summary>
        /// Time Complexity O(V+E), V = No Of Vertex & E = No Of Edges (this is same Time Complexity as BFS)
        /// Auxillary Space O(3V) ~ O(V) (for storing Queue, Path & Distance)
        /// </summary>
        /// <param name="UG"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void ShortestPathUnWeighted(UnDirectedGraph UG, int source, int destination)
        {
            if (UG == null) return;

            // Create Queue to do BFS
            Queue<int> q = new Queue<int>();

            // array to store index of prv Vertex to current Vertex
            int[] path = new int[UG.Length];
            // array storing distance from Source Vertex
            int[] distance = new int[UG.Length];

            // Set -1 initial value for all Vertex
            for (int i = 0; i < UG.Length; i++)
                path[i] = distance[i] = -1;

            distance[source] = 0;           // Set source distance from itself to Zero

            // Reset IsVisited Array of Graph
            UG.Reset_VisitedArr();
            q.Enqueue(source);              // Add source to Queue
            UG._IsVisitedArr[source] = 1;   // Mark source Visited
            // do BFS
            while(q.Count>0)
            {
                var prvNode = q.Dequeue();
                foreach (var AdjacentNode in UG._Graph[prvNode])           // Iterate thru Adjacent Node of current Vertex
                {
                    if (UG._IsVisitedArr[AdjacentNode] != 1)    // can use check: if(distance[AdjacentNode] == -1) and in that case we dont even need to update/check Visited Array thruout the code
                    {
                        path[AdjacentNode] = prvNode;          // Update PrvNode for Current Node
                        distance[AdjacentNode] = distance[prvNode] + 1;

                        q.Enqueue(AdjacentNode);
                        UG._IsVisitedArr[AdjacentNode] = 1;
                    }
                }
            }
            
            // Fetching and printing distance and path b/w source and destination
            Console.WriteLine($"The Min distance from Source : '{source}' to Destination '{destination}' is :\t{distance[destination]} ");
            PrintShortestPath(path, destination);
        }

        /// <summary>
        /// Time Complexity O(ELogV), V = No Of Vertex & E = No Of Edges (as we are performing E times update/insert operation in Priority Queue)
        /// Auxillary Space O(3V) ~ O(V) (for storing Priority Queue, Path & Distance)
        /// Supports Only Graphs with Non-Negative Edges
        /// </summary>
        /// <param name="UGW"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void DijkstraAlgorithm(WeightedGraph UGW, int source, int destination = -1)
        {
            if (UGW?._Graph == null) return;
            // To Get Vertex with Min/least distance from source Vertex
            PriorityQueue pq = new PriorityQueue(UGW.Length);

            // Create Distance Array to store min distance for each Vertex from source Vertex
            int[] dist = new int[UGW.Length];

            // Array which stores previous Node in path to current Node
            int[] path = new int[UGW.Length];

            for (int i = 0; i < UGW.Length; i++)
                dist[i] = path[i] = -1;

            dist[source] = 0;

            // add distance from source as key and index as value to priority queue
            pq.Enqueue(dist[source], source);

            // traverse thru the Graph
            while (pq.Count > 0)
            {
                var prvNode = pq.ExtractHighest();
                foreach (var adjacentVertex in UGW._Graph[prvNode.Value])
                {
                    var distanceFromSource = dist[prvNode.Value] + adjacentVertex.Weight;
                    // check if this Vertex is being processed for first time than add it to priority queue by Key = its distance from source
                    if (dist[adjacentVertex.Index] == -1)
                    {
                        dist[adjacentVertex.Index] = distanceFromSource;
                        pq.Enqueue(distanceFromSource, adjacentVertex.Index);
                        path[adjacentVertex.Index] = prvNode.Value;
                    }
                    else if (distanceFromSource < dist[adjacentVertex.Index])
                    {
                        dist[adjacentVertex.Index] = distanceFromSource;
                        pq.UpdatePriority(adjacentVertex.Index, distanceFromSource);            // O(n) Liner Search + Update O(LogN)
                        path[adjacentVertex.Index] = prvNode.Value;
                    }
                }
            }
            
            #region Print Shortest Path b/w source and destination
            if (destination == -1)   // If No destination provided print shortest path for each Vertex from source
            {
                // Print Path for each Vertex in Graph
                for(int i =0;i<UGW.Length;i++)
                {
                    Console.WriteLine($"The Min distance from Source : '{source}' to Destination '{i}' is :\t{dist[i]} ");
                    PrintShortestPath(path, i);
                }
            }
            else
            {
                Console.WriteLine($"The Min distance from Source : '{source}' to Destination '{destination}' is :\t{dist[destination]} ");
                PrintShortestPath(path, destination);
            }
            #endregion
        }

        public static void PrintShortestPath(int[] pathArray, int prvVertex)
        {
            Console.Write($"The Path for above MinDistance is : ");
            Stack<int> pathStoD = new Stack<int>();
            while (prvVertex != -1)         // While Previous Vertex is not Equal to -1 (i.e, prv Vertex for Source Vertex in Path Array
            {
                pathStoD.Push(prvVertex);   // Adding Vertex closest to destination in decreasing order
                prvVertex = pathArray[prvVertex];
            }
            foreach (var Vertex in pathStoD)
                Console.Write($"--> {Vertex} ");
            Console.WriteLine();
        }
    }
}
