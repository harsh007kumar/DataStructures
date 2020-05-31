using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_DataStructure
{
    //GFG Link https://www.geeksforgeeks.org/graph-and-its-representations/
    class EntryPoint
    {
        static void Main(string[] args)
        {
            Console.Write("====================  UnDirected-Graph ====================");
            UnDirectedGraph UG = new UnDirectedGraph(5);
            //Adding Edges to Un-Directed Graph
            UG.AddEdge( 0, 1);
            UG.AddEdge( 0, 4);
            UG.AddEdge( 1, 2);
            UG.AddEdge( 1, 3);
            UG.AddEdge( 1, 4);
            UG.AddEdge( 2, 3);
            UG.AddEdge( 3, 4);
            UG.PrintGraph();
            UG.BreadthFirstSearch(2);
            //Removing Edges from Un-Directed Graph
            UG.RemoveEdge( 1, 2);
            UG.RemoveEdge( 1, 3);
            UG.RemoveEdge( 1, 4);
            UG.PrintGraph();
            UG.BreadthFirstSearch( 2);

            Console.Write("====================  Directed-Graph ======================");

            DiGraph DG = new DiGraph(4);
            //Adding Edges to Di-Graph
            DG.AddEdge(0, 1);
            DG.AddEdge(0, 2);
            DG.AddEdge(1, 2);
            DG.AddEdge(2, 0);
            DG.AddEdge(2, 3);
            DG.AddEdge(3, 3);
            //Removing Edges from Di-Graph

            DG.PrintGraph();
            DG.BreadthFirstSearch(2);
            DG.DepthFirstSearch(2);

            Console.ReadKey();
        }
    }

    /// <summary>
    /// Implementing for Un-Weighted Un-Directed Graphs (Adjacency List)
    /// Time Complexity O(V) for Searching connection b/w 2 Nodes/Vertex or Deletion of edge and O(1) for Insertion, where V = no_of_nodes
    /// Space req O(|V|+|E|), In the worst case, it can be C(V, 2) number of edges in a graph thus consuming O(V^2), where V = no_of_nodes & E = no_of_Edges
    /// </summary>
    public class Graph
    {
        //Array of generic lists which is used as graph data structure
        protected List<int>[] _Graph;
        protected int[] _IsVisitedArr;

        public Graph(int size)
        {
            //Here we are using Array to implement Graph using AdjacencyList
            _Graph = new List<int>[size];

            //Initializing the array
            for (var i = 0; i < _Graph.Length; i++)
                _Graph[i] = new List<int>();
        }

        public void PrintGraph()                                                                        //O(v*v)
        {
            Console.Write("\n\nPrint all connections in graph per Node");
            for (int i = 0; i < _Graph.Length; i++)
            {
                Console.Write($"\nNode:{i} ");                                  // Print Node
                foreach (int node in _Graph[i])
                    Console.Write($"-->{node}");                                // Print all the nodes its connected with
            }
        }

        //GFG https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/
        /// <summary>
        /// All the vertices may not be reachable from a given vertex (example Disconnected graph). To print all the vertices, we can modify the BFS function to do traversal starting from all nodes one by one.
        /// Time Complexity: O(V+E) where V is number of vertices in the graph and E is no number of edges in the graph.
        /// Space Complexity: O(V). Since, an extra visited array is needed of size V.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="startingNode"></param>
        public void BreadthFirstSearch(int startingNode)
        {
            if (_Graph == null) return;

            //Step1 : Create Array which keeps track of each node in graph has been visited yet or not
            _IsVisitedArr = new int[_Graph.Length];

            //Step2 : Create empty Queue
            Queue<int> q = new Queue<int>();

            //Step3 : add starting node to Queue before starting BFS/LevelOrder traversal
            q.Enqueue(startingNode);                                                                        // add that element to queue
            _IsVisitedArr[startingNode] = 1;                                                                 // mark the element as visited

            Console.WriteLine("\n\nBreadth First Traversal/Level Order Traversal\nStarting with Node:{0}", startingNode);
            //Step4 : loop until Queue is not empty
            while (q.Count > 0)
            {
                var temp = q.Dequeue();
                Console.Write($"{temp} "); // Print starting node data
                foreach (int node in _Graph[temp])
                    if (_IsVisitedArr[node] != 1)
                    {
                        q.Enqueue(node); //Add linked node to Queue
                        _IsVisitedArr[node] = 1; // Mark node as visited
                    }
            }
            Console.WriteLine("\n");
        }

        //GFG https://www.geeksforgeeks.org/depth-first-search-or-dfs-for-a-graph/
        /// <summary>
        /// Time Complexity: O(V + E), where V is the number of vertices and E is the number of edges in the graph.
        /// Space Complexity: O(V). Since, an extra visited array is needed of size V.
        /// </summary>
        /// <param name="startingNode"></param>
        public void DepthFirstSearch(int startingNode)
        {
            Console.WriteLine("\n\nDepth First Traversal\nStarting with Node:{0}", startingNode);
            
            //Create Array which keeps track of each node in graph has been visited yet or not
            _IsVisitedArr = new int[_Graph.Length];

            //Calling recursive DFS on graph
            DepthFirstSearch_Recursive(_Graph, startingNode);
        }

        protected void DepthFirstSearch_Recursive(List<int>[] graph, int startingNode)
        {
            if (graph == null || _IsVisitedArr[startingNode] == 1) return;

            //Step1: Mark current node as visited and print the node
            _IsVisitedArr[startingNode] = 1;
            Console.Write($"{startingNode} ");

            //Step2 : Traverse all the adjacent and unmarked nodes
            foreach (var node in graph[startingNode])
                DepthFirstSearch_Recursive(graph, node);
        }
    }

    //Un-Directed Graph
    public class UnDirectedGraph : Graph
    {
        //calling base class constructor
        public UnDirectedGraph(int size) : base (size){}

        /// <summary>
        /// To Add Edge in Un-Directed Graph, Time Complexity O(1)
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public void AddEdge(int node1, int node2)                                                       //O(1)
        {
            //Console.WriteLine($"Adding edge b/w {node1} and {node2}");
            _Graph[node1].Add(node2);
            _Graph[node2].Add(node1);
        }

        /// <summary>
        /// To Remove Edge in Un-Directed Graph, Time Complexity O(1)
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public void RemoveEdge(int node1, int node2)                                                    //O(1)
        {
            Console.WriteLine($"Removing edge b/w {node1} and {node2}");
            _Graph[node1].Remove(node2);
            _Graph[node2].Remove(node1);
        }

    }

    //Directed Graph (Di-Graph)
    public class DiGraph : Graph
    {
        //calling base class constructor
        public DiGraph(int size) : base(size) { }

        /// <summary>
        /// To Add Edge in Di-Graph, Time Complexity O(1)
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public void AddEdge(int node1, int node2) => _Graph[node1].Add(node2);            //O(1)

        /// <summary>
        /// To Remove Edge in Di-Graph, Time Complexity O(1)
        /// </summary>
        /// <param name="node1"></param>
        /// <param name="node2"></param>
        public void RemoveEdge(int node1, int node2) => _Graph[node1].Remove(node2);      //O(1)

    }
}
