using System;
namespace Lesson05
{
    public class GraphEdge
    {
        public readonly GraphNode From;
        public readonly GraphNode To;

        public GraphEdge(GraphNode from, GraphNode to)
        {
            From = from;
            To = to;
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
