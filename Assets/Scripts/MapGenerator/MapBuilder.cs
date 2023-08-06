using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    public int maxX = 10;
    public int maxY = 5;
    public int roomsNumber = 10;
    public int[][] map = new int[99][];
    public Transform mapParent;
    private int startY, endX, endY;
    public System.Action<int[][]> OnMapGenerated = delegate { };
    public Dictionary<Tuple<int, int>, List<int>> roomSetups;

    private void Start()
    {
        StartCoroutine(GenerateDelayed());
    }

    IEnumerator GenerateDelayed()
    {
        yield return new WaitForSeconds(0.3f);
        GenerateMap();
    }

    private void RandomWalkGeneration(int startX, int startY, bool ignoreFilled)
    {
        int currentX = startX;
        int currentY = startY;
        int lastDirection = 0;

        if (ignoreFilled)
        {
            map[currentX][currentY] = 1;
        }

        while (currentX < maxX - 1)
        {
            var direction = UnityEngine.Random.Range(-1, ignoreFilled ? 2 : 3);

            if (lastDirection != 0 && direction != 0)
            {
                direction = UnityEngine.Random.Range(-1, ignoreFilled ? 2 : 3);
            }

            if (lastDirection == 0 && direction == 0)
            {
                direction = UnityEngine.Random.Range(-1, ignoreFilled ? 2 : 3);
            }

            if (direction == -1)
            {
                if (currentY > 0)
                {
                    currentY--;
                }
            }
            else if (direction == 0)
            {
                if (currentX < maxX - 1)
                {
                    currentX++;
                }
            }
            else if (direction == 1)
            {
                if (currentY < maxY - 1)
                {
                    currentY++;
                }
            }
            else
            {
                if (currentX > 0)
                {
                    currentX--;
                }
            }

            if (map[currentX][currentY] != 0 && !ignoreFilled)
            {
                return;
            }
            else if (map[currentX][currentY] == 0)
            {
                map[currentX][currentY] = 2;
            }

            lastDirection = direction;
        }

        if (ignoreFilled)
        {
            map[currentX][currentY] = 9;
            endX = currentX;
            endY = currentY;
        }
    }

    private void DistanceGeneration()
    {
        var remainingRooms = roomsNumber;
        while (remainingRooms > 0)
        {
            var randX = UnityEngine.Random.Range(0, maxX);
            var randY = UnityEngine.Random.Range(0, maxY);
            while (map[randX][randY] != 0)
            {
                randX = UnityEngine.Random.Range(0, maxX);
                randY = UnityEngine.Random.Range(0, maxY);
            }
            map[randX][randY] = 1;
            remainingRooms--;
        }
    }

    public class matrixNode
    {
        public int fr = 0, to = 0, sum = 0;
        public int x, y;
        public matrixNode parent;
    }

    public static matrixNode AStar(int[][] matrix, int fromX, int fromY, int toX, int toY)
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // in this version an element in a matrix can move left/up/right/down in one step, two steps for a diagonal move.
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //the keys for greens and reds are x.ToString() + y.ToString() of the matrixNode 
        Dictionary<string, matrixNode> greens = new Dictionary<string, matrixNode>(); //open 
        Dictionary<string, matrixNode> reds = new Dictionary<string, matrixNode>(); //closed 

        matrixNode startNode = new matrixNode { x = fromX, y = fromY };
        string key = startNode.x.ToString() + startNode.x.ToString();
        greens.Add(key, startNode);

        Func<KeyValuePair<string, matrixNode>> smallestGreen = () =>
        {
            KeyValuePair<string, matrixNode> smallest = greens.ElementAt(0);

            foreach (KeyValuePair<string, matrixNode> item in greens)
            {
                if (item.Value.sum < smallest.Value.sum)
                    smallest = item;
                else if (item.Value.sum == smallest.Value.sum
                        && item.Value.to < smallest.Value.to)
                    smallest = item;
            }

            return smallest;
        };


        //add these values to current node's x and y values to get the left/up/right/bottom neighbors
        List<KeyValuePair<int, int>> fourNeighbors = new List<KeyValuePair<int, int>>()
                                            { new KeyValuePair<int, int>(-1,0),
                                              new KeyValuePair<int, int>(0,1),
                                              new KeyValuePair<int, int>(1, 0),
                                              new KeyValuePair<int, int>(0,-1) };

        int maxX = matrix.GetLength(0);
        if (maxX == 0)
            return null;
        int maxY = matrix[0].Length;

        while (true)
        {
            if (greens.Count == 0)
                return null;

            KeyValuePair<string, matrixNode> current = smallestGreen();
            if (current.Value.x == toX && current.Value.y == toY)
                return current.Value;

            greens.Remove(current.Key);
            reds.Add(current.Key, current.Value);

            foreach (KeyValuePair<int, int> plusXY in fourNeighbors)
            {
                int nbrX = current.Value.x + plusXY.Key;
                int nbrY = current.Value.y + plusXY.Value;
                string nbrKey = nbrX.ToString() + nbrY.ToString();
                if (nbrX < 0 || nbrY < 0 || nbrX >= maxX || nbrY >= maxY
                    || matrix[nbrX][nbrY] == 0 //obstacles marked by 'X'
                    || reds.ContainsKey(nbrKey))
                    continue;

                if (greens.ContainsKey(nbrKey))
                {
                    matrixNode curNbr = greens[nbrKey];
                    int from = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY);
                    if (from < curNbr.fr)
                    {
                        curNbr.fr = from;
                        curNbr.sum = curNbr.fr + curNbr.to;
                        curNbr.parent = current.Value;
                    }
                }
                else
                {
                    matrixNode curNbr = new matrixNode { x = nbrX, y = nbrY };
                    curNbr.fr = Math.Abs(nbrX - fromX) + Math.Abs(nbrY - fromY);
                    curNbr.to = Math.Abs(nbrX - toX) + Math.Abs(nbrY - toY);
                    curNbr.sum = curNbr.fr + curNbr.to;
                    curNbr.parent = current.Value;
                    greens.Add(nbrKey, curNbr);
                }
            }
        }
    }

    [ContextMenu("Generate")]
    public void GenerateMap()
    {
        map = new int[maxX][];
        for (int i = 0; i < maxX; i++)
        {
            map[i] = new int[maxY];
        }

        startY = UnityEngine.Random.Range(0, maxY);
        RandomWalkGeneration(0, startY, true);

        for (int i = 0; i < 2; i++)
        {
            int startX = UnityEngine.Random.Range(1, maxX - 1);
            RandomWalkGeneration(startX, 0, false);
        }
        for (int i = 0; i < 2; i++)
        {
            int startX = UnityEngine.Random.Range(1, maxX - 1);
            RandomWalkGeneration(startX, 4, false);
        }

        int remainingChests = 3;
        int remainingFights = 3;

        while (remainingChests > 0 || remainingFights > 0)
        {
            if (remainingChests > 0)
            {
                var randomX = UnityEngine.Random.Range(0, maxX - 1);
                var randomY = UnityEngine.Random.Range(0, maxY - 1);

                if (map[randomX][randomY] == 2)
                {
                    map[randomX][randomY] = 4;
                    remainingChests--;
                }
            }
            if (remainingFights > 0)
            {
                var randomX = UnityEngine.Random.Range(0, maxX - 1);
                var randomY = UnityEngine.Random.Range(0, maxY - 1);

                if (map[randomX][randomY] == 2)
                {
                    map[randomX][randomY] = 3;
                    remainingFights--;
                }
            }
        }

        //for (int x = 0; x < maxX; x++)
        //{
        //    for (int y = 0; y < maxY; y++)
        //    {
        //        if (map[x][y] != 0)
        //        {
        //            var go = Instantiate(mapTile);
        //            Instantiate(mapItems[map[x][y]]);
        //            //go.GetComponent<MeshRenderer>().material = materials[map[x][y]];
        //            go.transform.SetParent(mapParent);
        //            go.transform.localPosition = new Vector3((float)x, (float)y);
        //        }
        //    }
        //}

        try
        {
            FindPathFromStartToEnd(0, startY, endX, endY);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Map with wrong path generated, generating new one...");
            GenerateMap();
            return;
        }

        OnMapGenerated(map);
    }

    public void StoreRooms(Dictionary<Tuple<int, int>, List<int>> rooms)
    {
        roomSetups = rooms;
    }

    public List<int> GetRoom(Tuple<int, int> position)
    {
        foreach (var key in roomSetups.Keys)
        {
            if (key.Item1 == position.Item1 && key.Item2 == position.Item2)
            {
                return roomSetups[key];
            }
        }
        return null;
    }

    public List<Tuple<int, int>> FindPathFromStartToEnd(int fromX, int fromY, int toX, int toY)
    {
        matrixNode endNode = AStar(map, fromX, fromY, toX, toY);

        //looping through the Parent nodes until we get to the start node
        Stack<matrixNode> path = new Stack<matrixNode>();

        Debug.Log("FromX " + fromX);
        Debug.Log("FromY " + fromY);
        Debug.Log("ToX " + toX);
        Debug.Log("ToY " + toY);

        while (endNode.x != fromX || endNode.y != fromY)
        {
            path.Push(endNode);
            endNode = endNode.parent;
        }
        path.Push(endNode);

        Debug.Log("The shortest path from  " +
                          "(" + fromX + "," + fromY + ")  to " +
                          "(" + toX + "," + toY + ")  is:  \n");
        var pathList = new List<Tuple<int, int>>();
        while (path.Count > 0)
        {
            matrixNode node = path.Pop();
            Debug.Log(("(" + node.x + "," + node.y + ")"));
            pathList.Add(new Tuple<int, int>(node.x, node.y));
        }

        return pathList;
    }

    public int GetTileType(int x, int y)
    {
        return map[x][y];
    }

    public void SetTileCleared(int x, int y)
    {
        map[x][y] = 1;
    }
}
