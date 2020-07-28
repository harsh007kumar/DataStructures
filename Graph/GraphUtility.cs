﻿using BinaryHeap;
using System;
using System.Collections;
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
            DiGraph DAG = new DiGraph(6);

            //Adding Edges to Di-Graph
            DAG.AddEdge(5, 0);
            DAG.AddEdge(5, 2);
            DAG.AddEdge(4, 0);
            DAG.AddEdge(4, 1);
            DAG.AddEdge(2, 3);
            DAG.AddEdge(3, 1);

            return DAG;
        }

        public static UnDirectedWeightedGraph GetUnDirectedWeighted()
        {
            UnDirectedWeightedGraph unDiW = new UnDirectedWeightedGraph(9);
            unDiW.AddEdge(0, 1, 4);
            unDiW.AddEdge(0, 7, 8);
            unDiW.AddEdge(1, 2, 8);
            unDiW.AddEdge(1, 7, 11);
            unDiW.AddEdge(2, 3, 7);
            unDiW.AddEdge(2, 8, 2);
            unDiW.AddEdge(2, 5, 4);
            unDiW.AddEdge(3, 4, 9);
            unDiW.AddEdge(3, 5, 14);
            unDiW.AddEdge(4, 5, 10);
            unDiW.AddEdge(5, 6, 2);
            unDiW.AddEdge(6, 7, 1);
            unDiW.AddEdge(6, 8, 6);
            unDiW.AddEdge(7, 8, 7);

            return unDiW;
        }

        public static DiGraphWeighted GetDiGraphWeighted()
        {
            DiGraphWeighted diGW = new DiGraphWeighted(5);
            diGW.AddEdge(0, 1, 4);
            diGW.AddEdge(0, 2, 1);
            diGW.AddEdge(1, 4, 4);
            diGW.AddEdge(2, 1, 2);
            diGW.AddEdge(2, 3, 4);
            diGW.AddEdge(3, 4, 4);

            return diGW;
        }

        public static DiGraphWeighted GetDiGraphWeightedWithNegativeEdges()
        {
            var diGWN = GetDiGraphWeighted();
            diGWN.RemoveEdge(0, 1, 4);
            diGWN.AddEdge(0, 1, -4);
            return diGWN;
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
            Console.Write($"Min distance Source : '{source}' to Destination '{destination}' is : {distance[destination]}");
            PrintShortestPath(path, destination);
        }

        /// <summary>
        /// Time Complexity O((V+E)LogV) ~ O(ELogV), V = No Of Vertex & E = No Of Edges (as we are performing E times update/insert operation in Priority Queue)
        /// Auxillary Space O(3V) ~ O(V) (for storing Priority Queue, Path & Distance)
        /// Supports Only Graphs with Non-Negative Edges || Single Source Shortest Path
        /// </summary>
        /// <param name="graph_adjacency_list"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void DijkstraAlgorithm(WeightedGraph graph_adjacency_list, int source, int destination = -1)
        {
            if (graph_adjacency_list?._Graph == null) return;
            // To Get Vertex with Min/least distance from source Vertex
            PriorityQueue pq = new PriorityQueue(graph_adjacency_list.Length);

            // Create Distance Array to store min distance for each Vertex from source Vertex
            int[] dist = new int[graph_adjacency_list.Length];

            // Array which stores previous Node in path to current Node
            int[] path = new int[graph_adjacency_list.Length];

            for (int i = 0; i < graph_adjacency_list.Length; i++)
                dist[i] = path[i] = -1;

            dist[source] = 0;
            
            // add distance from source as key and index as value to priority queue
            pq.Enqueue(dist[source], source);

            // traverse thru the Graph
            while (pq.Count > 0)
            {
                var prvNode = pq.ExtractHighest();
                foreach (var adjacentVertex in graph_adjacency_list._Graph[prvNode.Value])
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
                        pq.UpdatePriority(dist[adjacentVertex.Index], distanceFromSource);  // Update O(LogN), as Position Dictonary used which holds index for all Nodes
                        dist[adjacentVertex.Index] = distanceFromSource;
                        path[adjacentVertex.Index] = prvNode.Value;
                    }
                }
            }
            
            #region Print Shortest Path b/w source and destination
            if (destination == -1)   // If No destination provided print shortest path for each Vertex from source
            {
                // Print Path for each Vertex in Graph
                for(int i =0;i<graph_adjacency_list.Length;i++)
                {
                    Console.Write($"Min distance Source : '{source}' to Destination '{i}' is : {dist[i]}");
                    PrintShortestPath(path, i);
                }
            }
            else
            {
                Console.Write($"Min distance Source : '{source}' to Destination '{destination}' is : {dist[destination]}");
                PrintShortestPath(path, destination);
            }
            #endregion
        }

        public static void PrintShortestPath(int[] pathArray, int prvVertex)
        {
            Console.Write($"\t||\tPath : ");
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

        /// <summary>
        /// Google Telephone Interview https://youtu.be/V0xjK_6ZoEY || Time Complexity O(n) n now of pairs/rows in input || Auxillary Space O(n)
        /// </summary>
        /// <param name="input"></param>
        public static void FindAnimalKingdomTree(List<Relation> input)
        {
            if (input == null) return;

            // Step1 create a Dictonary which hold each 'animal name as Key' and 'memory address of that Animal Node' as its value
            Dictionary<string, AnimalKingdom> animDict = new Dictionary<string, AnimalKingdom>();

            // Step2 create a HashSet which stores all the childs, this will be used later to identify the GrandParent(s) in Dictonary
            HashSet<string> childs = new HashSet<string>();

            // Step3 traverse thru input list
            foreach(var relation in input)                                      // Time O(n)
            {
                var parent = relation.parent;
                var child = relation.child;

                if(!animDict.ContainsKey(parent))       // new parent found
                    animDict.Add(parent, new AnimalKingdom(parent));
                if(!animDict.ContainsKey(child))        // new child found
                    animDict.Add(child, new AnimalKingdom(child));
                
                // add child to in the parent's Child list
                animDict[parent].AddChild(animDict[child]);

                // add child to the Set of Childs
                childs.Add(child);
            }

            // Step4 remove all child nodes from Parent Dictonary
            foreach (var child in childs)
                animDict.Remove(child);

            // Step5 print Parent(s) left in Dictonary (can be multiple in case be have disconnected forest/graph)
            foreach(var grandParent in animDict)
                PrintAnimalKingdomTree(grandParent.Value);
        }

        public static void PrintAnimalKingdomTree(AnimalKingdom grandParent, int NoOftab = 0)
        {
            if (grandParent == null) return;
            
            Console.WriteLine();                            // Start From New line
            PrintTab(NoOftab);                              // Give Tab for Parent
            Console.Write(grandParent.ParentName);          // Print Parent
            foreach (var child in grandParent.Childrens)
                PrintAnimalKingdomTree(child, NoOftab + 1); // Print Child with 1 extra tab
        }

        public static void PrintTab(int times)
        {
            while (times-- > 0)
                Console.Write("\t");
        }

        /// <summary>
        /// BellManFordAlgo_ForAdjacencyListRepresentation || Time Complexity O(V*E) || Auxillary Space O(n)
        /// </summary>
        /// <param name="graph_adjacency_list"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void BellManFordAlgo(WeightedGraph graph_adjacency_list, int source, int destination = -1)
        {
            if (graph_adjacency_list?._Graph == null) return;
            
            // Fetch No Of Vertexs in Graph
            var V = graph_adjacency_list.Length;

            // Step1 create Queue to hold index of Vertex which have to be processed next
            Queue<int> q = new Queue<int>();

            // Step2 create Distance array to keep record of min distance for each Vertex from Source
            int[] dist = new int[V];

            // Step3 create a path array to keep track of previous Vertex for each Vertex in Shortest Path from Source
            int[] path = new int[V];

            // Step4 create an additinal array which track of if an Vertex is present in Queue or not
            bool[] present = new bool[V];

            // Step5 Initialize distance for each vertex from source to Int.max as we campare against this val & new distancec in are algo below
            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                path[i] = -1;
            }

            // set source Vertex Min distance to 0 and Mark Source present in queue
            dist[source] = 0;
            present[source] = true;

            // Enqueue sorce to queue
            q.Enqueue(source);

            while (q.Count > 0)
            {
                var parent = q.Dequeue();
                foreach (var AdjacentVertex in graph_adjacency_list._Graph[parent])
                {
                    var newDistance = dist[parent] + AdjacentVertex.Weight;
                    if (newDistance < dist[AdjacentVertex.Index])
                    {
                        dist[AdjacentVertex.Index] = newDistance;   // update new min distance in dist array
                        path[AdjacentVertex.Index] = parent;        // update prvVertex in path array
                        if (!present[AdjacentVertex.Index])
                            q.Enqueue(AdjacentVertex.Index);        // if Adjacent Vertex not already present in Queue than add
                        
                        present[AdjacentVertex.Index] = true;       // mark Adjacent Vertex as present in queue
                    }
                }
                present[parent] = false;                            // mark parent as not present in Queue Now

            }

            #region Print Shortest Path b/w source and destination
            if (destination == -1)   // If No destination provided print shortest path for each Vertex from source
            {
                // Print Path for each Vertex in Graph
                for (int i = 0; i < graph_adjacency_list.Length; i++)
                {
                    Console.Write($"Min distance Source : '{source}' to Destination '{i}' is : {dist[i]}");
                    PrintShortestPath(path, i);
                }
            }
            else
            {
                Console.Write($"Min distance Source : '{source}' to Destination '{destination}' is : {dist[destination]}");
                PrintShortestPath(path, destination);
            }
            #endregion

        }
    }
}
