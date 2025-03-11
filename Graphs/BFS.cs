namespace AcuAlgoNet;

public static partial class Graphs
{
    public static T BFS<T>(Dictionary<T, List<T>> graph, T start, T target, Action<T>? log) 
    {
        var queue = new Queue<T>();
        var visited = new HashSet<T>();

        queue.Enqueue(start);
        visited.Add(start);
        while (queue.Count > 0)
        {
            var node = queue.Dequeue();

            if (Equals(node, target))
            {
                log?.Invoke(node);
                return node;
            }

            if (graph.ContainsKey(node)) 
            {
                foreach (var lf in graph[node])
                {
                    if (visited.Contains(lf)) continue;
                    
                    visited.Add(lf);
                    queue.Enqueue(lf);
                }
            }
        }

        throw new KeyNotFoundException();
    }
}