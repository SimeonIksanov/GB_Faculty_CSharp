using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson05
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
    }
}
