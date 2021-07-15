using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson06
{
    public class Graph
    {
        private GraphNode[] nodes;

        public Graph(int n)
        {
            nodes = Enumerable.Range(0, n).Select(num => new GraphNode((uint)num)).ToArray();
        }

        public IEnumerable<GraphNode> Nodes
        {
            get
            {
                foreach (var node in nodes)
                    yield return node;
            }
        }

        public IEnumerable<GraphEdge> Edges
        {
            get
            {
                return nodes.SelectMany(node => node.IncidentEdges).Distinct();
            }
        }

        public int Length { get { return nodes.Length; } }

        public GraphNode this[int index]
        {
            get
            {
                return nodes[index];
            }
        }

        public void AddEdge(uint index1, uint index2, int weight = 1)
        {
            GraphNode.Connect(nodes[index1], nodes[index2], this, weight);
        }

        public void DeleteEdge(GraphEdge edge)
        {
            GraphNode.Disconnect(edge);
        }
    }
}
