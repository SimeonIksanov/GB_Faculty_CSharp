using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05
{
    public class GraphNode
    {
        public readonly uint NodeNumber;

        private List<GraphEdge> edges = new List<GraphEdge>();

        public GraphNode(uint number)
        {
            NodeNumber = number;
        }

        public IEnumerable<GraphEdge> IncidentEdges
        {
            get
            {
                foreach (var edge in edges) yield return edge;
            }
        }

        public IEnumerable<GraphNode> IncidentNodes
        {
            get
            {
                return edges.Select(e => e.OtherNode(this));
            }
        }

        public static GraphEdge Connect(GraphNode from, GraphNode to, Graph graph)
        {
            if (!graph.Nodes.Contains(from) || !graph.Nodes.Contains(to)) throw new ArgumentException();

            var edge = new GraphEdge(from, to);
            from.edges.Add(edge);
            to.edges.Add(edge);

            return edge;
        }

        public static void Disconnect(GraphEdge edge)
        {
            edge.To.edges.Remove(edge);
            edge.From.edges.Remove(edge);
        }
    }
}
