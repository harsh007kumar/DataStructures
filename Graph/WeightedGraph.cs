using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class GraphNode
    {
        public int Index { get; set; }
        public int Weight { get; set; }

        public GraphNode(int index, int weight = 1)
        {
            Index = index;
            Weight = weight;
        }
    }

    public class WeightedGraph
    {
        // Array of generic lists which is used as graph data structure
        public List<GraphNode>[] _Graph;
        public int[] _IsVisitedArr;

        public WeightedGraph(int size)
        {
            //Here we are using Array to implement Graph using AdjacencyList
            _Graph = new List<GraphNode>[size];

            //Initializing the array
            for (var i = 0; i < _Graph.Length; i++)
                _Graph[i] = new List<GraphNode>();
        }

        public int Length { get { return _Graph.Length; } }

        public void Reset_VisitedArr() => _IsVisitedArr = new int[Length];

        public int NoOfVertex { get { return _Graph.Length; } }

        public void PrintGraph()                                                                        //O(V+E)
        {
            Console.Write("\nPrint all connections in graph per Vertex");
            for (int i = 0; i < _Graph.Length; i++)
            {
                Console.Write($"\nNode:{i} ");                                  // Print Node
                foreach (var node in _Graph[i])
                    Console.Write($"-->Node:{node.Index} (Cost{node.Weight}) || ");       // Print all the nodes its connected with
            }
            Console.WriteLine();
        }

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
                foreach (var node in _Graph[temp])
                    if (_IsVisitedArr[node.Index] != 1)
                    {
                        q.Enqueue(node.Index);               // Add Adjacent Vertex to Queue
                        _IsVisitedArr[node.Index] = 1;       // Mark Vertex as visited
                    }
            }
            Console.WriteLine();
        }
    }

    public class DiGraphWeighted : WeightedGraph
    {
        //calling base class constructor
        public DiGraphWeighted(int size) : base(size) { }

        public void AddEdge(int node1, int node2, int weight) => _Graph[node1].Add(new GraphNode(node2, weight));

        public void RemoveEdge(int node1, int node2, int weight) => _Graph[node1].Remove(new GraphNode(node2, weight));
    }

    public class UnDirectedWeightedGraph : DiGraphWeighted
    {
        //calling base class constructor
        public UnDirectedWeightedGraph(int size) : base(size) { }

        public new void AddEdge(int node1, int node2, int weight)
        {
            base.AddEdge(node1, node2, weight);
            _Graph[node2].Add(new GraphNode(node1, weight));
        }

        public new void RemoveEdge(int node1, int node2, int weight)
        {
            base.AddEdge(node1, node2, weight);
            _Graph[node2].Remove(new GraphNode(node1, weight));
        }
    }


}
