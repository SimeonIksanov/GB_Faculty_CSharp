using System;
namespace Lesson06
{
    public class GraphEdge
    {
        public readonly GraphNode From;

        public readonly GraphNode To;

        public readonly int Weight;

        public GraphEdge(GraphNode from, GraphNode to, int weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public bool isIncident(GraphNode node)
        {
            return From == node || To == node;
        }

        public GraphNode OtherNode(GraphNode node)
        {
            if (!isIncident(node)) throw new ArgumentException();
            return node == From ? To : From;
        }
    }
}
