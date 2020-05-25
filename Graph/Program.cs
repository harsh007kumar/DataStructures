using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    //GFG Link https://www.geeksforgeeks.org/graph-and-its-representations/
    class Program
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
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Implementing Graphs (Adjacency List)
    /// Time Complexity O(V) for Searching connection b/w 2 Nodes/Vertex or Deletion of edge and O(1) for Insertion, where V = no_of_nodes
    /// Space req O(|V|+|E|), In the worst case, it can be C(V, 2) number of edges in a graph thus consuming O(V^2), where V = no_of_nodes & E = no_of_Edges
    /// </summary>
    public static class Graph
    {
        public static void AddEdge(List<int>[] adj, int node1, int node2) => adj[node1].Add(node2);

        public static void PrintGraph(List<int>[] adj)
        {
            for(int i=0;i<adj.Length;i++)
            {
                Console.Write($"\nNode:{i} ");                                  // Print Node
                for (int j = 0; j < adj.Length; j++)
                {
                    if(i==j)
                    {
                        foreach (int node in adj[j])
                            Console.Write($"-->{node}");                        // Print all the nodes its connected with
                    }
                    else
                    {
                        foreach (int node in adj[j])
                        {
                            if (node == i)
                            {
                                Console.Write($"-->{j}");                       // Print all the nodes which are connected to the node we are traversing for
                                break;                                          //Break the loop once we have found the connection
                            }
                        }
                    }
                }
            }
        }
    }
}
