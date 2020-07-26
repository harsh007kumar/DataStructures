using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    /// <summary>
    /// Implementing for Un-Weighted Un-Directed Graphs (Adjacency List)
    /// Time Complexity O(V) for Searching connection b/w 2 Nodes/Vertex or Deletion of edge and O(1) for Insertion, where V = no_of_nodes
    /// Space req O(|V|+|E|), In the worst case, it can be C(V, 2) number of edges in a graph thus consuming O(V^2), where V = no_of_nodes & E = no_of_Edges
    /// </summary>
    public class Graph
    {
        //Array of generic lists which is used as graph data structure
        public List<int>[] _Graph;
        public int[] _IsVisitedArr;

        public Graph(int size)
        {
            //Here we are using Array to implement Graph using AdjacencyList
            _Graph = new List<int>[size];

            //Initializing the array
            for (var i = 0; i < _Graph.Length; i++)
                _Graph[i] = new List<int>();
        }

        public int Length { get { return _Graph.Length; } }

        public void Reset_VisitedArr() => _IsVisitedArr = new int[Length];

        public int NoOfVertex { get { return _Graph.Length; } }

        public void PrintGraph()                                                                        //O(V+E)
        {
            Console.Write("\nPrint all connections in graph per Node");
            for (int i = 0; i < _Graph.Length; i++)
            {
                Console.Write($"\nNode:{i} ");                                  // Print Node
                foreach (int node in _Graph[i])
                    Console.Write($"-->{node}");                                // Print all the nodes its connected with
            }
            Console.WriteLine();
        }

        //GFG https://www.geeksforgeeks.org/breadth-first-search-or-bfs-for-a-graph/
        /// <summary>
        /// All the vertices may not be reachable from a given vertex (example Disconnected graph). To print all the vertices, we can modify the BFS function to do traversal starting from all nodes one by one.
        /// Time Complexity: O(V+E) where V is number of vertices in the graph and E is no number of edges in the graph.
        /// Space Complexity: O(V). Since, an extra visited array is needed of size V.
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="currentNode"></param>
        public void BreadthFirstSearch(int currentNode)
        {
            if (_Graph == null) return;

            //Step1 : Reset Array which keeps track of each node in graph has been visited yet or not
            _IsVisitedArr = new int[_Graph.Length];

            //Step2 : Create empty Queue
            Queue<int> q = new Queue<int>();

            //Step3 : add starting node to Queue before starting BFS/LevelOrder traversal
            q.Enqueue(currentNode);                                                                        // add that element to queue
            _IsVisitedArr[currentNode] = 1;                                                                 // mark the element as visited

            Console.Write($"\nBreadth First Traversal/Level Order Traversal\tStarting with Node :\t");
            //Step4 : loop until Queue is not empty
            while (q.Count > 0)
            {
                var temp = q.Dequeue();
                Console.Write($" {temp} "); // Print starting node data
                foreach (int node in _Graph[temp])
                    if (_IsVisitedArr[node] != 1)
                    {
                        q.Enqueue(node); //Add linked node to Queue
                        _IsVisitedArr[node] = 1; // Mark node as visited
                    }
            }
            Console.WriteLine();
        }

        //GFG https://www.geeksforgeeks.org/depth-first-search-or-dfs-for-a-graph/
        /// <summary>
        /// Time Complexity: O(V + E), where V is the number of vertices and E is the number of edges in the graph.
        /// Space Complexity: O(V). Since, an extra visited array is needed of size V.
        /// </summary>
        /// <param name="startingNode"></param>
        public void DepthFirstSearch()
        {
            //Create Array which keeps track of each node in graph has been visited yet or not
            _IsVisitedArr = new int[_Graph.Length];

            //Calling recursive DFS on graph on every node to cover disconnected graph (in which every node is reachble for just one Node)
            for (int startingNode = 0; startingNode < _Graph.Length; startingNode++)
            {
                if (_IsVisitedArr[startingNode] != 1)
                {
                    Console.Write($"\nDepth First Traversal\tStarting with Node:\t");
                    DepthFirstSearch_Recursive(startingNode);
                }
            }
            Console.WriteLine();
        }

        public void DepthFirstSearch_Recursive(int currentNode)
        {
            if (_Graph == null || _IsVisitedArr[currentNode] == 1) return;

            //Step1: Mark current node as visited and print the node
            _IsVisitedArr[currentNode] = 1;
            Console.Write($" {currentNode} ");

            //Step2 : Traverse all the adjacent and unmarked nodes
            foreach (var adjacentNode in _Graph[currentNode])
                DepthFirstSearch_Recursive(adjacentNode);
        }
    }

    //Un-Directed Graph
    public class UnDirectedGraph : Graph
    {
        //calling base class constructor
        public UnDirectedGraph(int size) : base(size) { }

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
