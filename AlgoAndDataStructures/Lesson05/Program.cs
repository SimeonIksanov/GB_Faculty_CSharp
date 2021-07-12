using System;
using System.Linq;

namespace Lesson05
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph(6);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 2);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 4);
            graph.AddEdge(2, 5);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 5);
            graph.AddEdge(4, 5);

            Console.WriteLine(string.Join(' ', graph[0].DepthSearch().Select(n => n.NodeNumber)));

            Console.WriteLine(string.Join(' ', graph[0].BreadthSearch().Select(n => n.NodeNumber)));
        }
    }
}
