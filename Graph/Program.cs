using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    //GFG Link https://www.geeksforgeeks.org/graph-and-its-representations/
    class EntryPoint
    {
        static void Main(string[] args)
        {
            //Array of generic linked list which will we will use as graph data structure
            //Here we are starting with Array which has 5 Nodes/Vertex
            List<int>[] adjacencyList = new List<int>[5];

            //Initializing the our array
            for (int i = 0; i < adjacencyList.Length; i++)
                adjacencyList[i] = new List<int>();

            //Adding Edges
            Graph.AddEdge(adjacencyList, 0, 1);
            Graph.AddEdge(adjacencyList, 0, 4);
            Graph.AddEdge(adjacencyList, 1, 2);
            Graph.AddEdge(adjacencyList, 1, 3);
            Graph.AddEdge(adjacencyList, 1, 4);
            Graph.AddEdge(adjacencyList, 2, 3);
            Graph.AddEdge(adjacencyList, 3, 4);
            Graph.PrintGraph(adjacencyList);
            Graph.BreadthFirstSearch(adjacencyList,2);
            //Removing Edges
            Graph.RemoveEdge(adjacencyList, 1, 2);
            Graph.RemoveEdge(adjacencyList, 1, 3);
            Graph.RemoveEdge(adjacencyList, 1, 4);
            Graph.PrintGraph(adjacencyList);
            Graph.BreadthFirstSearch(adjacencyList, 2);

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Implementing for Un-Weighted Un-Directed Graphs (Adjacency List)
    /// Time Complexity O(V) for Searching connection b/w 2 Nodes/Vertex or Deletion of edge and O(1) for Insertion, where V = no_of_nodes
    /// Space req O(|V|+|E|), In the worst case, it can be C(V, 2) number of edges in a graph thus consuming O(V^2), where V = no_of_nodes & E = no_of_Edges
    /// </summary>
    public static class Graph
    {
        public static void AddEdge(List<int>[] adj, int node1, int node2)                                   //O(1)
        {
            //Console.WriteLine($"Adding edge b/w {node1} and {node2}");
            adj[node1].Add(node2);
            adj[node2].Add(node1);
        }

        public static void PrintGraph(List<int>[] adj)                                                      //O(v*v)
        {
            Console.Write("\n\nPrint all connections in graph per Node");
            for(int i=0;i<adj.Length;i++)
            {
                Console.Write($"\nNode:{i} ");                                  // Print Node
                        foreach (int node in adj[i])
                            Console.Write($"-->{node}");                        // Print all the nodes its connected with
            }
        }

        public static void RemoveEdge(List<int>[] adj, int node1, int node2)                                //O(1)
        {
            Console.WriteLine($"Removing edge b/w {node1} and {node2}");
            adj[node1].Remove(node2);
            adj[node2].Remove(node1);
        }

        public static void BreadthFirstSearch(List<int>[] graph, int startingNode)
        {
            if (graph == null) return;

            //Step1 : Create Array which keeps track of each node in graph has been visited yet or not
            int[] isVisitedArr = new int[graph.Length];

            //Step2 : Create empty Queue
            Queue<int> q = new Queue<int>();

            //Step3 : add starting node to Queue before starting BFS/LevelOrder traversal
            q.Enqueue(startingNode);                                                                        // add that element to queue
            isVisitedArr[startingNode] = 1;                                                               // mark the element as visited

            Console.WriteLine("\n\nBreadth First Traversal/Level Order Traversal\nStarting with Node:{0}",startingNode);
            //Step4 : loop until Queue is not empty
            while (q.Count > 0)
            {
                var temp = q.Dequeue();
                Console.Write($"{temp} "); // Print starting node data
                foreach (int node in graph[temp])
                    if (isVisitedArr[node] != 1)
                    {
                        q.Enqueue(node); //Add linked node to Queue
                        isVisitedArr[node] = 1; // Mark node as visited
                    }
            }
            Console.WriteLine("\n");
        }
    }
}
