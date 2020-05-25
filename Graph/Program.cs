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
            List<int>[] adj = new List<int>[5];

            //Initializing the our array
            for (int i = 0; i < adj.Length; i++)
                adj[i] = new List<int>();

            //Adding Edges
            Graph.AddEdge(adj, 0, 1);
            Graph.AddEdge(adj, 0, 4);
            Graph.AddEdge(adj, 1, 2);
            Graph.AddEdge(adj, 1, 3);
            Graph.AddEdge(adj, 1, 4);
            Graph.AddEdge(adj, 2, 3);
            Graph.AddEdge(adj, 3, 4);
            Graph.PrintGraph(adj);
            //Removing Edges
            Graph.RemoveEdge(adj, 1, 2);
            Graph.RemoveEdge(adj, 1, 3);
            Graph.RemoveEdge(adj, 1, 4);
            Graph.PrintGraph(adj);

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
            adj[node1].Remove(node2);
            adj[node2].Remove(node1);
        }
    }
}
