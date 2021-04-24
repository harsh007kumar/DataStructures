using BinaryHeap;
using RangeMinimumQueries_RMQ;
using System;
using System.Collections;
using System.Collections.Generic;

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

            int Len = graphObj.NoOfVertex;

            //Step1: Create a stack to hold the nodes after each iteration of DFS
            Stack<int> s = new Stack<int>(Len);

            //Step2: run DFS & store vertices according to their finish times in stack.
            graphObj.Reset_VisitedVertexArr();                                                   // Reset Visited vertices Array 

            Console.Write("\nDFS of current Graph : ");
            for (int index = 0; index < Len; index++)
                if (graphObj._IsVisitedVertex[index] != 1)
                    FillStackUsingRecursiveDFS(graphObj, index, ref s);                    // Time complexity O(V+E) 

            //Step3: Reverse all directed Edges in Di-Graph || so that 'Source' becomes 'sink' and the 'Sink' becomes 'source'.
            DiGraph reverse = GetTranspose(graphObj);                                      // Time complexity O(V+E)

            //Step4: Run DFS of the reversed graph using sequence of vertices in stack (process Vertices from Sink to Source)
            reverse.Reset_VisitedVertexArr();                                             // Reset Visited vertices Array 
            Console.Write("\n\nPrinting All Strongly Connected Components in Di-Graph (using Kosaraju’s algorithm) below :");
            foreach (var node in s)
                if (reverse._IsVisitedVertex[node] != 1)
                {
                    Console.Write("\nSCC : ");
                    reverse.DepthFirstSearch_Recursive(node); //Base class method   // Time complexity O(V+E)
                }
            Console.WriteLine();
        }

        public static void FillStackUsingRecursiveDFS(DiGraph graph, int startingNode, ref Stack<int> stack)
        {
            if (graph == null || graph._IsVisitedVertex[startingNode] == 1) return;

            //Step1: Mark current node as visited and print the node
            graph._IsVisitedVertex[startingNode] = 1;
            Console.Write($" {startingNode} ");

            //Step2 : Traverse all the adjacent and unmarked nodes
            foreach (var node in graph._Graph[startingNode])
                FillStackUsingRecursiveDFS(graph, node, ref stack);

            //Step3: add starting node to Stack after traversing thru its connected nodes
            stack.Push(startingNode);
        }

        public static DiGraph GetTranspose(DiGraph graph)
        {
            var Len = graph.NoOfVertex;
            DiGraph reverseGraph = new DiGraph(Len);
            for (int vertex = 0; vertex < Len; vertex++)
                foreach (var AdjNode in graph._Graph[vertex])
                    reverseGraph._Graph[AdjNode].Add(vertex);
            return reverseGraph;
        }

        /// <summary>
        /// Time Complexity: O(V+E) its simply DFS with an extra stack, complexity same as DFS || Auxiliary space: O(V) needed for the stack.
        /// </summary>
        /// <param name="DG"></param>
        public static Stack<int> TopologicalSortingOnDAG(DiGraph DG)
        {
            // Step1 create a stack to hold the Vertex in the reverse order of when they appear in call-stack
            Stack<int> st = new Stack<int>();

            // Reset InVisited Arr
            DG.Reset_VisitedVertexArr();

            //Calling recursive TopologicalSort on graph for every node to cover disconnected graph (in which every is not reachble from single Node)
            for (int startingNode = 0; startingNode < DG.NoOfVertex; startingNode++)
                if (DG._IsVisitedVertex[startingNode] != 1)
                    TopologicalSort_Recursive(DG, startingNode, ref st);

            Console.Write("\nTopological Sort of above DAG is :\t");
            foreach (var Vertex in st)
                Console.Write($" {Vertex} ");

            Console.WriteLine();
            return st;
        }

        public static void PrintMatrix(int[,] graph)
        {
            var Len = graph.GetLength(0);

            Console.WriteLine("\nPrinting all connections in graph per Vertex");
            for (int row = 0; row < Len; row++)
            {
                Console.Write($"Node:{row}");
                for (int col = 0; col < Len; col++)
                    if (graph[row, col] > 0)
                        Console.Write($"\t--> {col} cost({graph[row, col]})");
                Console.WriteLine();
            }
        }

        static void TopologicalSort_Recursive(DiGraph DG, int currentNodeIndex, ref Stack<int> stack)
        {
            if (DG?._Graph == null || DG._IsVisitedVertex[currentNodeIndex] == 1) return;

            // Mark current Node as Visited Now
            DG._IsVisitedVertex[currentNodeIndex] = 1;

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

            var len = UG.NoOfVertex;
            // Create Queue to do BFS
            Queue<int> q = new Queue<int>();

            // array to store index of prv Vertex to current Vertex
            int[] path = new int[len];
            // array storing distance from Source Vertex
            int[] distance = new int[len];

            // Set -1 initial value for all Vertex
            for (int i = 0; i < len; i++)
                path[i] = distance[i] = -1;

            distance[source] = 0;           // Set source distance from itself to Zero

            // Reset IsVisited Array of Graph
            UG.Reset_VisitedVertexArr();
            q.Enqueue(source);              // Add source to Queue
            UG._IsVisitedVertex[source] = 1;   // Mark source Visited
            // do BFS
            while(q.Count>0)
            {
                var prvNode = q.Dequeue();
                foreach (var AdjacentNode in UG._Graph[prvNode])           // Iterate thru Adjacent Node of current Vertex
                {
                    if (UG._IsVisitedVertex[AdjacentNode] != 1)    // can use check: if(distance[AdjacentNode] == -1) and in that case we dont even need to update/check Visited Array thruout the code
                    {
                        path[AdjacentNode] = prvNode;          // Update PrvNode for Current Node
                        distance[AdjacentNode] = distance[prvNode] + 1;

                        q.Enqueue(AdjacentNode);
                        UG._IsVisitedVertex[AdjacentNode] = 1;
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
        /// Supports Only Weighted Graphs with Non-Negative Edges || Single Source Shortest Path
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
                var prvNode = pq.ExtractHighest();                                          // Time O(VLogV)
                foreach (var adjacentVertex in graph_adjacency_list._Graph[prvNode.Value])
                {
                    var distanceFromSource = dist[prvNode.Value] + adjacentVertex.Weight;
                    // check if this Vertex is being processed for first time than add it to priority queue by Key = its distance from source
                    if (dist[adjacentVertex.Index] == -1)
                    {
                        pq.Enqueue(distanceFromSource, adjacentVertex.Index);               // Max 'E' times insert in O(LogV)
                        dist[adjacentVertex.Index] = distanceFromSource;
                        path[adjacentVertex.Index] = prvNode.Value;
                    }
                    else if (distanceFromSource < dist[adjacentVertex.Index])
                    {
                        pq.UpdatePriority(dist[adjacentVertex.Index], distanceFromSource);  // Max 'E' times Update O(LogV), as Position Dictonary/Array used which holds index for all Nodes
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
        /// BellManFordAlgo_ForAdjacencyListRepresentation || Time Complexity O(V*E) || Auxillary Space O(V)
        /// Doesn't work in case of -ve weight cycle, i.e, Total of wt of all edges in cycle is -ve
        /// </summary>
        /// <param name="graph_adjacency_list"></param>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void BellManFord_AdjacencyList(WeightedGraph graph_adjacency_list, int source, int destination = -1)
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

        /// <summary>
        /// BellManFordAlgo for Weighted Graph(with -ve wt) represented as Pair of Edges(src,dest,wt) || Time Complexity O(V*E) || Auxillary Space O(V)
        /// Doesn't work in case of -ve weight cycle, i.e, Total of wt of all edges in cycle is -ve
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="source"></param>
        public static void BellManFord_GraphAsPairOfEdges(GraphAsPairOfEdges graph, int source)
        {
            if (graph.V <= 0 || graph.E <= 0) return;

            // Step1 create a distance array to hold min distance for each Vertex
            int[] dist = new int[graph.V];

            // Step2 creat a path array to hold prvVertex to current Vertex indedx
            int[] path = new int[graph.V];

            for (int i = 0; i < graph.V; i++)
            {
                dist[i] = int.MaxValue;
                path[i] = -1;
            }

            dist[source] = 0;

            // Step3 relaxing all the E edges in graph V-1 vertex times, to find min distance possible b/w each Edge
            for (int V = 1; V < graph.V; V++)
            {
                for (int E = 0; E < graph.E; E++)
                {
                    var src = graph.edge[E].src;
                    var dest = graph.edge[E].dest;
                    var wt = graph.edge[E].weight;
                    if (dist[src] + wt < dist[dest])
                    {
                        dist[dest] = dist[src] + wt;
                        path[dest] = src;
                    }
                }
            }

            // Step4 run below loop E edge time to check if graph edge cannot be relax further if they can means -ve wt cycle exists in graph
            for (int E = 0; E < graph.E; E++)
            {
                var src = graph.edge[E].src;
                var dest = graph.edge[E].dest;
                var wt = graph.edge[E].weight;
                if (dist[src] + wt < dist[dest])
                {
                    Console.WriteLine("Graph contains -ve weight cycle");
                    return;
                }
            }

            // Print Path for each Vertex in Graph
            for (int V = 0; V < graph.V; V++)
            {
                Console.Write($"Min distance Source : '{source}' to Destination '{V}' is : {dist[V]}");
                PrintShortestPath(path, V);
            }
        }

        /// <summary>
        /// Returns MST i.e, Minimal Spanning Tree of the weighted Connected Graph 
        /// Time Complexity O(V^2) || Space O(V), where V is no of rows/col in Graph Matrix (No Of Vertex)
        /// </summary>
        /// <param name="graphMatrix"></param>
        public  static void PrimAlgo_AdjacencyMatrix(int[,] graphMatrix)
        {
            if (graphMatrix == null) return;
            var Vertex = graphMatrix.GetLength(0);

            // create mstSet to keep track of Vertices included in MST
            bool[] mstSet = new bool[Vertex];

            // array to store key of each Vertex
            int[] key = new int[Vertex];

            // to store parent nodes in MST
            int[] path = new int[Vertex];

            // Initialize all key values as INFINITE
            for (int V = 0; V < Vertex; V++)
            {
                key[V] = int.MaxValue;
                path[V] = -1;
            }

            // set keyvalue to 0 for source so its picked first
            var source = 0;
            key[source] = 0;

            // while no of vertices in MST is less than Graph
            for (int V = 0; V < Vertex; V++)                                    // Time O(V) as we are adding Vertex to mst one at a time
            {
                // vertex u which is not there in mstSet and has minimum key value
                var vertexU = FindMinVertex(key, mstSet);                       // Time O(V) to find Vertex with minKeyValue
                // Include u to mstSet
                mstSet[vertexU] = true;

                // Update key value of all adjacent vertices of u
                for (int vertexV = 0; vertexV < Vertex; vertexV++)              // Time O(V) to traverse thru all the Vertices
                    // isAdjacent Vertex && AdjacnetVertexNotInMST && (wt of AdjacentVertex < keyvalue[AdjacentVertex])
                    if (graphMatrix[vertexU, vertexV] > 0 && !mstSet[vertexV] && graphMatrix[vertexU, vertexV] < key[vertexV])
                    {
                        key[vertexV] = graphMatrix[vertexU, vertexV];           // update wt of edge in keyvalue
                        path[vertexV] = vertexU;
                    }
            }

            // Print MST of the Graph and calculate weight of MST
            int WtOfMST = 0;
            for (int V = 0; V < Vertex; V++)
            {
                Console.Write($" Vertex : '{source}' connected to Vertex '{V}' by Edge with Cost : {key[V]}");
                PrintShortestPath(path, V);
                WtOfMST += key[V];
            }
            Console.WriteLine($" Weight of Above Minimal Spanning Tree is :\t'{WtOfMST}'");

        }

        /// <summary>
        /// Finds and returns index of Vertex with min associated value, such that Vertex is not present in mstSet(.i.e, Vertex is false) || Time O(V) || Space O(1)
        /// </summary>
        /// <param name="keyArr"></param>
        /// <param name="mstSet"></param>
        /// <returns></returns>
        public static int FindMinVertex(int[] keyArr, bool[] mstSet)
        {
            var minValue = int.MaxValue;
            var minIndex = -1;
            for (int i = 0; i < keyArr.Length; i++)               // Time O(V)
                if (!mstSet[i] && keyArr[i] < minValue)
                {
                    minValue = keyArr[i];
                    minIndex = i;
                }
            return minIndex;
        }

        /// <summary>
        /// Prim’s Algorithm to find MST of a 'UnDirected Weighted Connected Graph' represented using AdjacencyList || Time Complexity O(V+E)LogV) || Space O(V)
        /// </summary>
        /// <param name="graph"></param>
        public static void PrimAlgo_AdjacencyList(UnDirectedWeightedGraph graph)
        {
            if (graph?._Graph == null) return;
            var Vertex = graph.NoOfVertex;

            // to hold min edge value which connects each Vertex to tree
            int[] dist = new int[Vertex];

            // to store prv Vertex for each vertex
            int[] path = new int[Vertex];

            // create mstSet to keep track of Vertices included in MST
            bool[] mstSet = new bool[Vertex];

            for (int i = 0; i < Vertex; i++)
                dist[i] = path[i] = -1;

            var source = 0;
            dist[source] = 0;

            PriorityQueue pq = new PriorityQueue(Vertex);
            pq.Enqueue(dist[source],source);

            while (pq.Count > 0)                                                    // Time O(V)
            {
                var parent = pq.ExtractHighest();                                   // O(LogV)
                mstSet[parent.Value] = true;
                // Extract Vertex which has min key associated with it
                foreach (var adjacentVertex in graph._Graph[parent.Value])          // Time O(E)
                {
                    if (mstSet[adjacentVertex.Index]) continue;                     // Don't Process Vertices which are already included in MST

                    var newDistance = adjacentVertex.Weight;
                    // processing this Vertex for 1st time
                    if (dist[adjacentVertex.Index] == -1)
                    {
                        dist[adjacentVertex.Index] = adjacentVertex.Weight;
                        path[adjacentVertex.Index] = parent.Value;
                        pq.Enqueue(adjacentVertex.Weight, adjacentVertex.Index);    // O(LogV)
                    }
                    else if (newDistance < dist[adjacentVertex.Index])
                    {
                        pq.UpdatePriority(dist[adjacentVertex.Index], newDistance); // O(LogV)
                        dist[adjacentVertex.Index] = adjacentVertex.Weight;
                        path[adjacentVertex.Index] = parent.Value;
                    }
                }
            }

            // Print MST of the Graph and calculate weight of MST
            int WtOfMST = 0;
            for (int V = 0; V < graph.Length; V++)
            {
                Console.Write($" Vertex : '{source}' connected to Vertex '{V}' by Edge with Cost : {dist[V]}");
                PrintShortestPath(path, V);
                WtOfMST += dist[V];
            }
            Console.WriteLine($" Weight of Above Minimal Spanning Tree is :\t'{WtOfMST}'");
        }

        /// <summary>
        /// Finds ArticulartionPoint in Graph removing those Vertex(s) can divide the Graph in 2 or more connected components
        /// Time Complexity O(V+E) || Auxillary Space O(V)
        /// </summary>
        /// <param name="graph"></param>
        public static void FindArticulartionPoint_TarjanAlgorithm(UnDirectedGraph graph)
        {
            if (graph?._Graph == null) return;
            
            var V = graph.NoOfVertex;

            // array to store the parent
            int[] parent = new int[V];

            // array to store depth during DFS
            int[] depth = new int[V];

            // array to store the lowest point a Vertex can reach from adjacent Vertex
            int[] low = new int[V];

            // bool array to store articulation points of the graph (used for printing later)
            bool[] aps = new bool[V];

            // reset isVisitedArray so each Vertex is visited only once
            graph.Reset_VisitedVertexArr();

            // set the default parent and depth for each Vertex
            for (int i = 0; i < V; i++)
                parent[i] = depth[i] = low[i] = -1;

            int time = 0;

            // call recursive func on Each Vertex in Graph if its not Visited
            for (int i = 0; i < V; i++)
                if (graph._IsVisitedVertex[i] != 1)
                    ArticulartionPoint_Recursive(i,graph,parent,depth,low,aps, ref time);

            for (int i = 0; i < V; i++)
                if (aps[i] == true)
                    Console.WriteLine($" Vertex/Server: '{i}' is ArticulationPoint/CutVertex whose failure can divide network into 2 or more parts");
        }

        public static void ArticulartionPoint_Recursive(int u, UnDirectedGraph graph, int[] parent, int[] depth, int[] low, bool[] aps, ref int time)
        {
            graph._IsVisitedVertex[u] = 1;                                 // Mark the Vertex Visited
            depth[u] = low[u] = ++time;                                 // set depth and lowest Vertex

            // Count of children in DFS Tree 
            int children = 0;

            // traverse thru each connected Vertex and perform DFS
            foreach (var adjacentVertex in graph._Graph[u])
            {
                var v = adjacentVertex;
                if (graph._IsVisitedVertex[v] != 1)        // Not Visited
                {
                    children++;
                    parent[v] = u;
                    ArticulartionPoint_Recursive(v, graph, parent, depth, low, aps, ref time);
                    // set parent low as Min of its low or its connected Vertex Low (possible when subtree has connection to one of the ancestors of u)
                    low[u] = Math.Min(low[u], low[v]);


                    // Parent Node 'u' is an articulation point if :

                    // >> (1) u is root of DFS tree and has two or more chilren. 
                    if (parent[u] == -1 && children >= 2)
                        aps[u] = true;

                    // >> (2) If u is not root and low value of one of its child is more than/equal tp discovery value of u
                    if (parent[u] != -1 && low[v] >= depth[u])
                        aps[u] = true;
                }
                else if (v != parent[u])                // Already Visited
                    low[u] = Math.Min(low[u], depth[v]);                // back track edge
            }
        }

        // Finds Bridges in UnDirected-Graph removing which can divide the Graph in 2 or more connected components
        // Time Complexity O(V+E) || Space O(V)
        public static IList<IList<int>> FindAllBridges(UnDirectedGraph g)
        {
            List<IList<int>> bridges = new List<IList<int>>();
            if (g == null) return bridges;

            int n = g.NoOfVertex, time = 1, source = 0;     // we can choose any Vertex as source for Tarjan's Algo
            bool[] isVisited = new bool[n];                 // to mark is Vertex is already visited or not
            int[] startingTime = new int[n];                // to store the time at which Vertex was 1st visited
            int[] parent = new int[n];                      // to store the time at which Vertex was 1st visited
            int[] nodeWithEarlistTimeReachable = new int[n];// to store the the min-time node avalible via all any of the connected edges (except Parent)
            
            for (int i = 0; i < n; i++)
                startingTime[i] = parent[i] = nodeWithEarlistTimeReachable[i] = -1; // set default values

            FindAllBridges(source);                              // we can start from any vertex, taking 0th node as source

            foreach (var bridge in bridges)                 // Printing potentially Risky-Connection
                Console.WriteLine($" Edge/Cable: '{bridge[0]}---{bridge[1]}' is Only connection & its failure/damage to this can divide network into 2 or more parts");

            return bridges;

            // local func
            // DFS
            void FindAllBridges(int u)
            {
                isVisited[u] = true;            // Mark visited

                // update the starting time of newly visited node, also update the min-time-node reachable as self only for now
                startingTime[u] = nodeWithEarlistTimeReachable[u] = time++;

                foreach (var v in g._Graph[u])
                    if (!isVisited[v])              // not visited yet
                    {
                        parent[v] = u;          // mark parent
                        FindAllBridges(v);      // recursively check for adjacent Vertex

                        // Update 'starting time' of parent node is found one via Adjacent Vertex
                        nodeWithEarlistTimeReachable[u] = Math.Min(nodeWithEarlistTimeReachable[u], nodeWithEarlistTimeReachable[v]);

                        //-- now coming back i.e. back-tracking after above recursive call --//

                        // if Vertex 'V' can say to Vertex 'U' that i have found a Vertex better than you or i have found another way to reach to you
                        if (nodeWithEarlistTimeReachable[v] <= startingTime[u])
                            continue;
                        else // if (nodeWithEarlistTimeReachable[v] > startingTime[u]), meaning Vertex V only connection with Vertex 'U' is edge U---V
                            bridges.Add(new int[] { u, v });
                    }
                    else if (v != parent[u])        // vertex 'v' we are looking at is not parent of 'u'
                        // update if a node with even smaller start-time is reachable for Vertex 'u'
                        nodeWithEarlistTimeReachable[u] = Math.Min(nodeWithEarlistTimeReachable[u], startingTime[v]);
            }
        }




        /// <summary>
        /// Only works and applied on Strongly Connected Graphs (Ex- Find optiomal Path for PostMan so that he begins and end at Postoffice after delivering mails on every possible street without repeating)
        /// Time Complexity O(V+E) as worst case would be visiting all the Vertex after traversing thru all present edges || Auxillary Space O(V+1)
        /// •A connected undirected graph is Eulerian if and only if every graph vertex has an even degree, or exactly two vertices with an odd degree. 
        /// •​A directed graph is Eulerian if it is strongly connected and every vertex has an equal in and out degree.
        /// a.k.a 'Hamiltonian Cycle'
        /// </summary>
        /// <param name="graph"></param>
        public static void EulersCircuit(DiGraph graph, int sourceVertex)
        {
            if (graph?._Graph == null) return;

            // a array of HashSet which hold visited edge list for each Vertex
            HashSet<int>[] visitedEdgeList = new HashSet<int>[graph.NoOfVertex];
            // initialize adove array
            for (int i = 0; i < graph.NoOfVertex; i++)
                visitedEdgeList[i] = new HashSet<int>();

            // data structure to hold Euler's Path
            Stack<int> EulersCircuit = new Stack<int>();                            // Space O(E) holds each Edge once

            // reset visited Vertex List also to be used later to know if all Vertex are visited
            graph.Reset_VisitedVertexArr();

            int endVertex = sourceVertex;
            EulersCircuitUtil(ref EulersCircuit, ref visitedEdgeList, graph, sourceVertex, endVertex);

            // print the Euler tour/Path/Circuit
            foreach(var path in EulersCircuit)
                Console.Write($"-->{path}");
        }

        /// <summary>
        /// Here Edge (u,v) is such that u = parent & v = Adjacent Vertex
        /// Time Complexity O(V+E)
        /// </summary>
        /// <param name="eulersCircuit"></param>
        /// <param name="visitedEdgeList"></param>
        /// <param name="graph"></param>
        /// <param name="u"></param>
        /// <param name="finalStop"></param>
        public static void EulersCircuitUtil(ref Stack<int> eulersCircuit, ref HashSet<int>[] visitedEdgeList, DiGraph graph, int u, int finalStop)
        {
            foreach (var adjacentVertex in graph._Graph[u])
            {
                var v = adjacentVertex;
                if (!visitedEdgeList[u].Contains(v))                // if Edge not already in euler path
                {
                    // Mark edge as 'visited'
                    visitedEdgeList[u].Add(v);

                    // continue onto next Vertex once starting Node is reached, PostMan's last Vertex(PostOffice) found
                    if (v == finalStop)
                    {
                        eulersCircuit.Push(finalStop);              // first Vertex to be pushed onto the Circuit stack
                        continue;
                    }

                    // recursively call similar to DFS on Vertex 'v'
                    EulersCircuitUtil(ref eulersCircuit, ref visitedEdgeList, graph, v, finalStop);
                }
            }
            eulersCircuit.Push(u);                                  // adding visited Vertex when exiting system stack trace
        }

        /// <summary>
        /// Time Complexity O(V+E) when last vertex of the last edge makes the cycle in Graph || Space O(V)
        /// Modified DFS
        /// _IsVisitedVertex flags meaning { -1 : not visited || 0 : visited and in stack || 1 : visited but not in stack }
        /// </summary>
        /// <param name="DG"></param>
        /// <param name="parent"></param>
        /// <param name="currentVertex"></param>
        public static bool DetectCycleInDiGraph(DiGraph DG, ref int[] parent, int currentVertex = 0)
        {
            if (DG?._Graph == null) return false;

            DG._IsVisitedVertex[currentVertex] = 0;             // Mark current Vertes as Visited and in Stack
            Console.WriteLine($" Vertex : {currentVertex} visited");

            foreach (var AdjacentVertex in DG._Graph[currentVertex])
            {
                parent[AdjacentVertex] = currentVertex;         // update parent of Adjacent Vertex
                if (DG._IsVisitedVertex[AdjacentVertex] == -1 && DetectCycleInDiGraph(DG, ref parent, AdjacentVertex))      // if Vertex not visited make recursive call
                    return true;
                else if (DG._IsVisitedVertex[AdjacentVertex] == 0)  // Visited and present in stack
                {
                    Console.Write($"Cycle Found using DFS in DirectedGraph as follows : {AdjacentVertex}<--");
                    while (currentVertex != AdjacentVertex && currentVertex != -1)
                    {
                        Console.Write($" {currentVertex}<--");
                        currentVertex = parent[currentVertex];
                    }
                    Console.WriteLine($" {AdjacentVertex}<--");
                    return true;
                }
            }
            DG._IsVisitedVertex[currentVertex] = 1;             // Mark current Vertes as Visited and Not-in Stack

            return false;
        }

        /// <summary>
        /// Time Complexity O(V+E) when last vertex of the last edge makes the cycle in Graph || Space O(V)
        /// Modified BFS
        /// _IsVisitedVertex flags meaning { -1 : not visited || 0 : visited and in stack || 1 : visited but not in stack }
        /// </summary>
        /// <param name="UG"></param>
        /// <returns></returns>
        public static void DetectCycleInUnDirectedGraph(UnDirectedGraph UG)
        {
            if (UG?._Graph == null) return;

            Queue<int> q = new Queue<int>(UG.NoOfVertex);
            int source = 0;
            UG._IsVisitedVertex = new int[UG.NoOfVertex];
            for (int i = 0; i < UG.NoOfVertex; i++)
                UG._IsVisitedVertex[i] = -1;                // Mark all Vertex as Not-Visited

            q.Enqueue(source);
            UG._IsVisitedVertex[source] = 0;                // Mark Visited and in Queue

            while (q.Count>0)
            {
                var parent = q.Dequeue();
                Console.Write($"{parent} ");
                UG._IsVisitedVertex[parent] = 1;            // Mark Visited and Not-in Queue
                foreach (var AdjacentVertex in UG._Graph[parent])
                    if (UG._IsVisitedVertex[AdjacentVertex] == -1)
                    {
                        q.Enqueue(AdjacentVertex);
                        UG._IsVisitedVertex[AdjacentVertex] = 0;
                    }
                    else if (UG._IsVisitedVertex[AdjacentVertex] == 0)
                    {
                        Console.WriteLine($"\nCycle Found using BFS in UnDiGraph using BFS at Vertex : {parent}");
                        foreach (var V in q)
                            Console.Write($" {V}<--");
                        Console.WriteLine($" {parent}<--");
                    }
            }
        }

        /// <summary>
        /// Returns Longest path in an Un-Weighted DAG
        /// Time O(V+E) || Space O(V)
        /// </summary>
        /// <param name="dG"></param>
        public static void LongestPathInUnWightedDAG(DiGraph dG)
        {
            if (dG?._Graph == null) return;

            // Reset/Create Visited array which indicates if Vertex has been visited or not
            dG.Reset_VisitedVertexArr();

            // data structure to hold depth/max no of edges reachable from each Vertex
            int[] dp = new int[dG.NoOfVertex];

            for (int i = 0; i < dG.NoOfVertex; i++)                 // Time O(V+E) for performing DFS
                if (dG._IsVisitedVertex[i] != 1)                    // if Not Visited
                    LongestPathInUnWightedDAG_Util(dG, ref dp, i);  // DFS based apporach

            // Find and print the longest path from dp array
            var max = 0;
            for (int i = 0; i < dp.Length; i++)
                if (dp[i] > max)
                    max = dp[i];
            Console.WriteLine($" Longest Path in given Directed Unweighted graph is : {max}");
        }

        public static void LongestPathInUnWightedDAG_Util(DiGraph dg, ref int[] depthArr, int u)
        {
            if (dg._IsVisitedVertex[u] == 1) return;
            dg._IsVisitedVertex[u] = 1;                             // mark current Vertex as visited
            foreach (var adjacentVertex in dg._Graph[u])
            {
                if (dg._IsVisitedVertex[adjacentVertex] == -1)      // if adjacent not visited find the longest path for it
                    LongestPathInUnWightedDAG_Util(dg, ref depthArr, u);
                depthArr[u] = Math.Max(depthArr[u], depthArr[adjacentVertex] + 1);
            }
        }

        /// <summary>
        /// Returns Longest path from a given source Vertex in an Weighted DAG using Topological Sorting
        /// Time O(V+E) || Space O(V)
        /// </summary>
        /// <param name="dGW"></param>
        /// <param name="source"></param>
        public static void LongestPathInWightedDAG(DiGraphWeighted dGW, int source)
        {
            if (dGW?._Graph == null) return;

            // Reset/Create Visited array which indicates if Vertex has been visited or not
            dGW.Reset_VisitedArr();
            
            // Find Topolical Sorting of Graph
            var topolicalOrder = TopologicalSortingOnDAGWeighted(dGW);                                  // Time O(V+E)

            // create an distance array which holds max distance fr each Vertex
            int[] distance = new int[dGW.NoOfVertex];
            for (int i = 0; i < distance.Length; i++)
                distance[i] = int.MinValue;
            distance[source] = 0;

            // now update the edge weight for each Vertex in Topolical stack
            foreach (var Vertex in topolicalOrder)                                                      // Time O(V+E)
                if (distance[Vertex] != int.MinValue)
                    foreach (var adjacentVertex in dGW._Graph[Vertex])
                        if (distance[adjacentVertex.Index] < distance[Vertex] + adjacentVertex.Weight)
                            distance[adjacentVertex.Index] = distance[Vertex] + adjacentVertex.Weight;

            // print all longest distance fr each vertex from source
            for (int i = 0; i < distance.Length; i++)
                Console.WriteLine($" Distance from source {source} to destination {i} : {distance[i]}");
        }

        public static Stack<int> TopologicalSortingOnDAGWeighted(DiGraphWeighted dGW)
        {
            // Step1 create a stack to hold the Vertex in the reverse order of when they appear in call-stack
            Stack<int> st = new Stack<int>();

            // Reset InVisited Arr
            dGW.Reset_VisitedArr();

            //Calling recursive TopologicalSort on graph for every node to cover disconnected graph (in which every is not reachble from single Node)
            for (int startingNode = 0; startingNode < dGW.NoOfVertex; startingNode++)
                if (dGW._IsVisitedArr[startingNode] != 1)
                    TopologicalSort_Util(dGW, startingNode, ref st);

            Console.Write("\nTopological Sort of above DAG is :\t");
            foreach (var Vertex in st)
                Console.Write($" {Vertex} ");
            Console.WriteLine();
            return st;
        }

        public static void TopologicalSort_Util(DiGraphWeighted dGW, int vertex, ref Stack<int> st)
        {
            if (dGW?._Graph == null || dGW._IsVisitedArr[vertex] == 1) return;
            dGW._IsVisitedArr[vertex] = 1;
            foreach (var adjacentVertex in dGW._Graph[vertex])
                TopologicalSort_Util(dGW, adjacentVertex.Index, ref st);
            st.Push(vertex);
        }

        public static void CheckHamiltonianPathInDAG(DiGraph dG)
        {
            if (dG?._Graph == null) return;

            var topologicalSort = TopologicalSortingOnDAG(dG);
            // Approximation approach, if there exists a edge b/w each Vertex in Top Sort, than its Hamiltonian Path
            var u = topologicalSort.Pop();
            while (topologicalSort.Count > 1)
            {
                var v = topologicalSort.Peek();
                var edgeExists = false;
                foreach (var adj in dG._Graph[u])
                    if (adj == v) { edgeExists = true; break; }
                if (!edgeExists) { Console.WriteLine("Hamiltonian Path Not Found"); return; }
                u = topologicalSort.Pop();
            }
            Console.WriteLine("Given Graph contain at Hamiltonian Path");
        }

        /// <summary>
        /// Find LCA for any two Vertex based upon Eulers Tour + RMQ, https://youtu.be/sD1IoalFomA?list=PLDV1Zeh2NRsDGO4--qE8yH72HFL1Km93P
        /// Time for 1st run/Setup Max[ O(2vLogv) , O(V+E) ], subsequent LCA queries can be made in O(1) time
        /// Auxillary Space O(v), here V = No of vertex in Directed Acyclic Graph
        /// </summary>
        /// <param name="dG"></param>
        public static void LowestCommonAnsectorInDirectedAcyclicGraph(DiGraph dG, int vertex1, int vertex2)
        {
            if (dG?._Graph == null) return;

            var eulersTourLen = dG.NoOfVertex * 2 - 1;              // (NoOfVertex * 2) - 1;
            // create a depth array to store the depth of Vertices as encountered during modified Euler's Tour
            int[] depth = new int[eulersTourLen];

            // create a nodes array to store the index of Vertex as encountered during modified Euler's tour
            int[] nodes = new int[eulersTourLen];

            // array to store the 'last position' seen for each Vertex when going thru modified Euler's tour
            int[] lastSeenAt = new int[dG.NoOfVertex];

            var root = 0;       // Vertex with Indegree 0 in DAG (root in case of BinaryTree)
            var tourIndex = 0;
            Console.WriteLine("Eulerian Tour for given Graph along imaginary green edges[one in direction of directed edge (u,v) and 1 in opp. (v,u)]");
            EulersTourModifiedDFSForLCAOnDAG(dG, root, depth, nodes, lastSeenAt, ref tourIndex);                // Time O(V+E)

            // Construct an 'Sparse table' on depth array,
            // which return Minimum Index of the value from 'depth array' to allow RMQ queris in O(1) b/w given Lower n Upper Bound (array subset)
            SparseTable spTable = new SparseTable(depth);                                                       // Time O(nLogn), here n = 2V-1

            // RMQ on Vertices who's LCA we need, index of Vertices is fetched from 'lastSeenAt' array
            int LCA_Index = spTable.IndexQuery(lastSeenAt[vertex1], lastSeenAt[vertex2]);                       // Time O(1)

            // print the LCA
            Console.WriteLine($" \nFor Vertex '{vertex1}' n '{vertex2}' Lowest Common Anscetor is : \t{nodes[LCA_Index]}");

        }

        public static void EulersTourModifiedDFSForLCAOnDAG(DiGraph dG, int vertexU, int[] depthArr, int[] nodesArr, int[] lastseenArr, ref int tourIndex, int level = 0)
        {
            // registerVisit for vertex U
            RegisterVisit(vertexU, depthArr, nodesArr, lastseenArr, ref tourIndex, level);
            
            foreach (var vertexV in dG._Graph[vertexU])
            {
                // registerVisit for every adjacent VertexV
                EulersTourModifiedDFSForLCAOnDAG(dG, vertexV, depthArr, nodesArr, lastseenArr, ref tourIndex, level + 1);
                // registerVisit for vertex U again
                RegisterVisit(vertexU, depthArr, nodesArr, lastseenArr, ref tourIndex, level);
            }
        }

        public static void RegisterVisit(int vertexID, int[] depthArr, int[] nodesArr, int[] lastseenArr, ref int tourIndex, int level)
        {
            Console.Write(vertexID+" ");
            depthArr[tourIndex] = level;
            nodesArr[tourIndex] = vertexID;
            lastseenArr[vertexID] = tourIndex;
            ++tourIndex;
        }
    }
}
