using System;
using System.Linq;

namespace Lesson06
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(5);

            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 5);
            graph.AddEdge(0, 4, 7);
            graph.AddEdge(1, 2, 1);
            graph.AddEdge(2, 3, 5);
            graph.AddEdge(2, 4, 1);
            graph.AddEdge(3, 4, 3);

            Console.WriteLine(graph.Dijkstra(0, 4)
                                   .Select(node => node.NodeNumber.ToString())
                                   .Aggregate((a, b) => a + " - " + b));
        }
    }
}
