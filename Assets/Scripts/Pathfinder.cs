namespace CFE
{
    using UnityEngine;
    using System.Collections.Generic;

    class Pathfinder
    {
        private static PathfindingPosition[,] graph;

        public static void init(TileModel[,] modelGraph)
        {
            if(graph != null)
            {
                Debug.LogError("Init is being called more than once");
                return;
            }

            graph = new PathfindingPosition[DataManager.Width + 1, DataManager.Height + 1];

            for (int i = 0; i < DataManager.Width + 1; i++)
            {
                for (int j = 0; j < DataManager.Height + 1; j++)
                {
                    graph[i, j] = new PathfindingPosition(i, j, true);
                }
            }
        }

        public static void init()
        {
            if (graph != null)
            {
                Debug.LogError("Init is being called more than once");
                return;
            }

            graph = new PathfindingPosition[DataManager.Width + 1, DataManager.Height + 1];

            for (int i = 0; i < DataManager.Width + 1; i++)
            {
                for (int j = 0; j < DataManager.Height + 1; j++)
                {
                    graph[i, j] = new PathfindingPosition(i, j, true);
                }
            }
        }

        public static int heuristicDistance(int x1, int y1, int x2, int y2)
        {
            int xDif = Mathf.Abs(x1 - x2);
            int yDif = Mathf.Abs(y1 - y2);
            return xDif + yDif;
        }

        public static TilePosition[] findPath(TilePosition start, TilePosition end)
        {
            BinaryHeap_Min<PathfindingPosition> frontier = new BinaryHeap_Min<PathfindingPosition>(15);
            resetGraph(graph);

            PathfindingPosition startPos = graph[start.xIndex, start.yIndex];
            PathfindingPosition endPos = graph[end.xIndex, start.yIndex];

            frontier.Enqueue(startPos, 0);
            PathfindingPosition workingTile = null;

            while(!frontier.isEmpty)
            {
                workingTile = frontier.Dequeue();
                workingTile.visited = true;

                if (workingTile.xIndex == end.xIndex && workingTile.yIndex == end.yIndex)
                    return backTracePath(workingTile);

                enqueueNeighbors(frontier, workingTile, endPos);
            }

            return backTracePath(workingTile);
        }

        private static void enqueueNeighbors(BinaryHeap_Min<PathfindingPosition> frontier, PathfindingPosition workingTile, PathfindingPosition endTile)
        {
            enqueueEastNeighbor(frontier, workingTile, endTile);
            enqueueWestNeighbor(frontier, workingTile, endTile);
            enqueueSouthNeighbor(frontier, workingTile, endTile);
            enqueueNorthNeighbor(frontier, workingTile, endTile);
        }

        private static void enqueueNorthNeighbor(BinaryHeap_Min<PathfindingPosition> frontier, PathfindingPosition workingTile, PathfindingPosition endTile)
        {
            PathfindingPosition nextTile;
            if (workingTile.yIndex < DataManager.Height)
            {
                nextTile = graph[workingTile.xIndex, workingTile.yIndex + 1];
                if (nextTile.pathfindingEnabled)
                {
                    int newCost = workingTile.costSoFar + 1;
                    if (!nextTile.visited || newCost < nextTile.costSoFar)
                    {
                        nextTile.cameFrom = workingTile;
                        nextTile.costSoFar = newCost;
                        int priority = newCost + heuristicDistance(nextTile.xIndex, nextTile.yIndex, endTile.xIndex, endTile.yIndex);
                        frontier.Enqueue(nextTile, priority);
                    }
                }
            }
        }

        private static void enqueueEastNeighbor(BinaryHeap_Min<PathfindingPosition> frontier, PathfindingPosition workingTile, PathfindingPosition endTile)
        {
            PathfindingPosition nextTile;
            if (workingTile.xIndex > 0)
            {
                nextTile = graph[workingTile.xIndex - 1, workingTile.yIndex];
                if (nextTile.pathfindingEnabled)
                {
                    int newCost = workingTile.costSoFar + 1;
                    if (!nextTile.visited || newCost < nextTile.costSoFar)
                    {
                        nextTile.cameFrom = workingTile;
                        nextTile.costSoFar = newCost;
                        int priority = newCost + heuristicDistance(nextTile.xIndex, nextTile.yIndex, endTile.xIndex, endTile.yIndex);
                        frontier.Enqueue(nextTile, priority);
                    }
                }
            }
        }

        private static void enqueueWestNeighbor(BinaryHeap_Min<PathfindingPosition> frontier, PathfindingPosition workingTile, PathfindingPosition endTile)
        {
            PathfindingPosition nextTile;
            if (workingTile.xIndex < DataManager.Width)
            {
                nextTile = graph[workingTile.xIndex + 1, workingTile.yIndex];
                if (nextTile.pathfindingEnabled)
                {
                    int newCost = workingTile.costSoFar + 1;
                    if (!nextTile.visited || newCost < nextTile.costSoFar)
                    {
                        nextTile.cameFrom = workingTile;
                        nextTile.costSoFar = newCost;
                        int priority = newCost + heuristicDistance(nextTile.xIndex, nextTile.yIndex, endTile.xIndex, endTile.yIndex);
                        frontier.Enqueue(nextTile, priority);
                    }
                }
            }
        }

        private static void enqueueSouthNeighbor(BinaryHeap_Min<PathfindingPosition> frontier, PathfindingPosition workingTile, PathfindingPosition endTile)
        {
            PathfindingPosition nextTile;
            if (workingTile.yIndex > 0)
            {
                nextTile = graph[workingTile.xIndex, workingTile.yIndex - 1];
                if (nextTile.pathfindingEnabled)
                {
                    int newCost = workingTile.costSoFar + 1;
                    if (!nextTile.visited || newCost < nextTile.costSoFar)
                    {
                        nextTile.cameFrom = workingTile;
                        nextTile.costSoFar = newCost;
                        int priority = newCost + heuristicDistance(nextTile.xIndex, nextTile.yIndex, endTile.xIndex, endTile.yIndex);
                        frontier.Enqueue(nextTile, priority);
                    }
                }
            }
        }

        private static TilePosition[] backTracePath(PathfindingPosition endNode)
        {
            Stack<PathfindingPosition> path = new Stack<PathfindingPosition>();
            path.Push(endNode);
            PathfindingPosition nextPos = endNode.cameFrom;
            while(nextPos != null)
            {
                path.Push(nextPos);
                nextPos = nextPos.cameFrom;
            }
            TilePosition[] retValue = new TilePosition[path.Count];
            PathfindingPosition temp;
            for (int i = 0; i < retValue.Length; i++)
            {
                temp = path.Pop();
                retValue[i] = new TilePosition(temp.xIndex, temp.yIndex);
            }
            return retValue;
        }

        private static void resetGraph(PathfindingPosition[,] graph)
        {
            for (int i = 0; i < DataManager.Width + 1; i++)
            {
                for (int j = 0; j < DataManager.Height + 1; j++)
                {
                    graph[i, j].reset();
                }
            }
        }


        private class PathfindingPosition
        {
            public int xIndex, yIndex, costSoFar;
            public PathfindingPosition cameFrom;
            public bool visited, pathfindingEnabled;

            public PathfindingPosition(int x, int y, bool enabled)
            {
                xIndex = x;
                yIndex = y;
                cameFrom = null;
                visited = false;
                pathfindingEnabled = enabled;
            }

            public void reset()
            {
                cameFrom = null;
                costSoFar = 0;
                visited = false;
            }

            public override string ToString()
            {
                return xIndex.ToString() + "," + yIndex.ToString();
            }
        }
    }
}
