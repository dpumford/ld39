using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Helpers
{
    public static class Pathfinder
    {
        public static IList<T> AStar<T>(List<T> space, T start, T end, Func<List<T>, T, T, int> heuristic, Func<List<T>, T, IList<T>> getNeighbors) where T : class
        {
            // The set of nodes already evaluated
            var closedSet = new List<T>();

            // The set of currently discovered nodes that are not evaluated yet.
            // Initially, only the start node is known.
            var openSet = new List<T>();
            openSet.Add(start);

            // For each node, which node it can most efficiently be reached from.
            // If a node can be reached from many nodes, cameFrom will eventually contain the
            // most efficient previous step.
            var cameFrom = new Dictionary<T, T>(space.Count);

            // For each node, the cost of getting from the start node to that node.
            var gScore = new Dictionary<T, int>(space.Count);

            // For each node, the total cost of getting from the start node to the goal
            // by passing by that node. That value is partly known, partly heuristic.
            var fScore = new Dictionary<T, int>(space.Count);

            foreach (var entity in space)
            {
                gScore.Add(entity, int.MaxValue);
                fScore.Add(entity, int.MaxValue);
            }

            // The cost of going from start to start is zero.
            gScore[start] = 0;

            // For the first node, that value is completely heuristic.
            fScore[start] = heuristic(space, start, end);

            while (openSet.Any())
            {
                var current = GetLowestFScore(openSet, fScore);

                if (current == end)
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Add(current);

                var neighbors = getNeighbors(space, current);

                foreach (var neighbor in neighbors) {
                    if (closedSet.Contains(neighbor))
                    {
                        continue; // Ignore the neighbor which is already evaluated.
                    }

                    if (!openSet.Contains(neighbor))
                    {
                        // Discover a new node
                        openSet.Add(neighbor);
                    }

                    // The distance from start to a neighbor
                    var tentativeGScore = gScore[current] + 1; //Assumes that the neighbors are all the same distance away

                    if (tentativeGScore >= gScore[neighbor])
                    {
                        continue; // This is not a better path.
                    }

                    // This path is the best until now. Record it!
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + heuristic(space, neighbor, end);
                }
            }

            return null;
        }

        private static T GetLowestFScore<T>(IList<T> openSet, IDictionary<T, int> fScore)
        {
            var minEntity = openSet.First();
            var minScore = fScore[minEntity];

            for (int e = 1; e < openSet.Count; e++)
            {
                if (fScore[openSet[e]] < minScore)
                {
                    minEntity = openSet[e];
                    minScore = fScore[minEntity];
                }
            }

            return minEntity;
        }

        private static IList<T> ReconstructPath<T>(IDictionary<T, T> cameFrom, T almostEnd)
        {
            var total_path = new List<T> { almostEnd };
            var current = almostEnd;

            while (cameFrom.Keys.Contains(current))
            {
                current = cameFrom[current];
                total_path.Add(current);
            }

            return total_path;
        }
    }
}
