using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    // A class to represent a weighted edge in graph 
    public class Edge
    {
        public int src, dest, weight;
        public Edge() => src = dest = weight = 0;       // Constructor
        public Edge(int src, int dest, int wt = 1)      // Parametrized Constructor
        {
            this.src = src;
            this.dest = dest;
            weight = wt;
        }
    };

    public class GraphAsPairOfEdges
    {
        public int V, E;
        public Edge[] edge;                                 // array of edges

        // Creates a graph with V vertices and E edges 
        public GraphAsPairOfEdges(int NoOfVertex, int TotalNoOfEdges)
        {
            V = NoOfVertex;
            E = TotalNoOfEdges;
            edge = new Edge[TotalNoOfEdges];
            for (int i = 0; i < TotalNoOfEdges; ++i)
                edge[i] = new Edge();
        }

        public void PrintGraph()
        {
            Console.WriteLine("Print all connections in graph per Vertex");
            for (int i = 0; i < V; i++)
            {
                Console.Write($" Node:{i} ");
                for (int j = 0; j < E; j++)
                    if (edge[j].src == i)
                        Console.Write($"\t-->{edge[j].dest} cost({edge[j].weight})");
                Console.WriteLine();
            }
        }

        // GFG https://www.geeksforgeeks.org/bellman-ford-algorithm-dp-23/
        public static GraphAsPairOfEdges GetGraph()
        {
            int V = 5; // Number of vertices in graph 
            int E = 8; // Number of edges in graph 

            GraphAsPairOfEdges graph = new GraphAsPairOfEdges(V, E);

            // add edge 0-1 (or A-B in above figure) 
            graph.edge[0].src = 0;
            graph.edge[0].dest = 1;
            graph.edge[0].weight = -1;

            // add edge 0-2 (or A-C in above figure) 
            graph.edge[1].src = 0;
            graph.edge[1].dest = 2;
            graph.edge[1].weight = 4;

            // add edge 1-2 (or B-C in above figure) 
            graph.edge[2].src = 1;
            graph.edge[2].dest = 2;
            graph.edge[2].weight = 3;

            // add edge 1-3 (or B-D in above figure) 
            graph.edge[3].src = 1;
            graph.edge[3].dest = 3;
            graph.edge[3].weight = 2;

            // add edge 1-4 (or A-E in above figure) 
            graph.edge[4].src = 1;
            graph.edge[4].dest = 4;
            graph.edge[4].weight = 2;

            // add edge 3-2 (or D-C in above figure) 
            graph.edge[5].src = 3;
            graph.edge[5].dest = 2;
            graph.edge[5].weight = 5;

            // add edge 3-1 (or D-B in above figure) 
            graph.edge[6].src = 3;
            graph.edge[6].dest = 1;
            graph.edge[6].weight = 1;

            // add edge 4-3 (or E-D in above figure) 
            graph.edge[7].src = 4;
            graph.edge[7].dest = 3;
            graph.edge[7].weight = -3;

            return graph;
        }

        // Youtube https://youtu.be/KudAWAMiQog
        public static GraphAsPairOfEdges GetGraphWithNegativeWtCycle()
        {
            int V = 4; // Number of vertices in graph 
            int E = 5; // Number of edges in graph 

            GraphAsPairOfEdges graph = new GraphAsPairOfEdges(V, E);

            // add edge 0-1
            graph.edge[0].src = 0;
            graph.edge[0].dest = 1;
            graph.edge[0].weight = 4;

            // add edge 0-2
            graph.edge[1].src = 0;
            graph.edge[1].dest = 2;
            graph.edge[1].weight = 5;

            // add edge 1-3
            graph.edge[2].src = 1;
            graph.edge[2].dest = 3;
            graph.edge[2].weight = 7;

            // add edge 2-1
            graph.edge[3].src = 2;
            graph.edge[3].dest = 1;
            graph.edge[3].weight = 7;

            // add edge 3-2
            graph.edge[4].src = 3;
            graph.edge[4].dest = 2;
            graph.edge[4].weight = -15;

            return graph;
        }
    }
}
