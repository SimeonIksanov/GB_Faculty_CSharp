using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson06
{
    public static class GraphNodeExtensions
    {
        public static IEnumerable<GraphNode> DepthSearch(this GraphNode startNode)
        {
            Stack<GraphNode> stack = new Stack<GraphNode>();
            HashSet<GraphNode> visited = new HashSet<GraphNode>();
            stack.Push(startNode);
            visited.Add(startNode);

            while (stack.Count != 0)
            {
                var node = stack.Pop();
                yield return node;

                foreach (var incidentNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    stack.Push(incidentNode);
                    visited.Add(incidentNode);
                }
            }
        }

        public static IEnumerable<GraphNode> BreadthSearch(this GraphNode startNode)
        {
            Queue<GraphNode> queue = new Queue<GraphNode>();
            HashSet<GraphNode> visited = new HashSet<GraphNode>();
            queue.Enqueue(startNode);
            visited.Add(startNode);

            while (queue.Count != 0)
            {
                var node = queue.Dequeue();
                yield return node;

                foreach (var incidentNode in node.IncidentNodes.Where(n => !visited.Contains(n)))
                {
                    queue.Enqueue(incidentNode);
                    visited.Add(incidentNode);
                }
            }
        }

        public static List<GraphNode> Dijkstra(this Graph graph, int from, int to)
        {
            if (from < 0 || from > graph.Nodes.Count() || to < 0 || to > graph.Nodes.Count())
                throw new ArgumentException("Wrong node indexes specified");
            if (graph.Edges.Any(e => e.Weight < 0))
                throw new InvalidOperationException("Edges with non positive weight found");

            GraphNode start = graph[from];
            GraphNode end = graph[to];

            List<GraphNode> notVisited = graph.Nodes.ToList();
            Dictionary<GraphNode, DijkstraData> track = new Dictionary<GraphNode, DijkstraData>();

            track[start] = new DijkstraData() { Distance = 0, Previous = null };

            while (true)
            {
                GraphNode toOpen = null;

                int minDistance = int.MaxValue;
                foreach (var nv in notVisited)
                {
                    if (track.ContainsKey(nv) && track[nv].Distance < minDistance)
                    {
                        minDistance = track[nv].Distance;
                        toOpen = nv;
                    }
                }

                if (toOpen == null) return null;
                if (toOpen == end) break;

                foreach (var edge in toOpen.IncidentEdges)
                {
                    var nextNode = edge.OtherNode(toOpen);
                    if (!notVisited.Contains(nextNode))
                        continue;

                    var newDistance = track[toOpen].Distance + edge.Weight;
                    if (!track.ContainsKey(nextNode) || track[nextNode].Distance > newDistance)
                    {
                        track[edge.OtherNode(toOpen)] = new DijkstraData() { Distance = newDistance, Previous = toOpen };
                    }
                }
                notVisited.Remove(toOpen);
            }

            List<GraphNode> retVal = new List<GraphNode>();
            GraphNode prev = end;
            while (prev != null)
            {
                retVal.Add(prev);
                prev = track[prev].Previous;
            }
            retVal.Reverse();
            return retVal;
        }
    }

    internal class DijkstraData
    {
        public GraphNode Previous;
        public int Distance;
    }
}
