using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MapUI : MonoBehaviour
{
    [SerializeField]
    private MapBuilder mapBuilder;
    [SerializeField]
    private RoomBehaviour roomBehaviour;
    [SerializeField]
    private List<GameObject> tilePrefabs = new List<GameObject>();
    [SerializeField]
    private float tileSpacing = 100;
    [SerializeField]
    private GameObject pathDot;
    [SerializeField]
    private GameObject clearedPrefab;
    private List<GameObject> pathList = new List<GameObject>();
    [SerializeField]
    private Tuple<int, int> currentPosition = new Tuple<int, int>(0, 0);
    private Tuple<int, int> selectedPosition = new Tuple<int, int>(0, 0);
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private ChestUI _chestUI;
    private GameObject playerObject;
    private Queue<Tuple<int, int>> queuedMovements = new Queue<Tuple<int, int>>();
    private bool moving = false;

    private void OnEnable()
    {
        mapBuilder.OnMapGenerated += CreateMapUI;
    }

    private void OnDisable()
    {
        mapBuilder.OnMapGenerated -= CreateMapUI;
    }

    private void CalculatePath(int x, int y)
    {
        if (moving)
        {
            return;
        }
        var path = new List<Tuple<int, int>>();
        try
        {
            path = mapBuilder.FindPathFromStartToEnd(currentPosition.Item1, currentPosition.Item2, x, y);
        }
        catch (Exception ex)
        {
            Debug.Log("This is not a valid target position");
        }
        foreach (var dot in pathList)
        {
            Destroy(dot);
        }
        if (selectedPosition.Item1 == path[path.Count - 1].Item1 && selectedPosition.Item2 == path[path.Count - 1].Item2)
        {
            selectedPosition = path[path.Count - 1];
            MoveSmoothly(queuedMovements.Dequeue());
            moving = true;
            //playerObject.transform.localPosition = new Vector3((float)x * tileSpacing, (float)y * tileSpacing);
            return;
        }
        if (path.Count == 0)
        {
            return;
        }
        pathList.Clear();
        queuedMovements.Clear();
        foreach (var point in path)
        {
            var dot = Instantiate(pathDot);
            dot.transform.SetParent(transform);
            dot.transform.localPosition = new Vector3((float)point.Item1 * tileSpacing, (float)point.Item2 * tileSpacing);
            pathList.Add(dot);
            queuedMovements.Enqueue(point);
        }
        selectedPosition = new Tuple<int, int>(x, y);
    }

    private void MoveSmoothly(Tuple<int, int> to)
    {
        FadeOverlay.FadeOut();
        playerObject.transform.DOLocalMove(new Vector3((float)to.Item1 * tileSpacing, (float)to.Item2 * tileSpacing), 0.3f).OnComplete(() =>
        {
            FadeOverlay.FadeIn();
            EvaluateTile(to);
        });
    }

    public void ClearTile()
    {
        var clearedFight = Instantiate(clearedPrefab);
        clearedFight.transform.SetParent(transform);
        clearedFight.transform.localPosition = new Vector3((float)currentPosition.Item1 * tileSpacing, (float)currentPosition.Item2 * tileSpacing);
        mapBuilder.SetTileCleared(currentPosition.Item1, currentPosition.Item2);
    }

    //This should not be here probably, should be in it's own controller since this is only a UI script
    private void EvaluateTile(Tuple<int, int> tile)
    {
        //TODO: Evaluate tile if it's a chest, fight etc.
        var tileType = mapBuilder.GetTileType(tile.Item1, tile.Item2);
        var roomSetup = mapBuilder.GetRoom(tile);
        if (roomSetup == null)
        {
            Debug.LogError("No room setup found for specified position, erroring out...");
            return;
        }
        roomBehaviour.LoadSetup(roomSetup);
        switch (tileType)
        {
            case 9:
                Debug.Log("end of dungeon, genereate new one");
                moving = false;
                break;
            case 3: //Fight
                Debug.Log("fight tile");

                //TODO: Start fight
                roomBehaviour.SpawnFight();
                moving = false;


                break;
            case 4: //Chest
                Debug.Log("chest tile");

                //TODO: Start chest
                roomBehaviour.SpawnChest();
                var clearedChest = Instantiate(clearedPrefab);
                clearedChest.transform.SetParent(transform);
                clearedChest.transform.localPosition = new Vector3((float)tile.Item1 * tileSpacing, (float)tile.Item2 * tileSpacing);
                _chestUI.GenerateChest();
                mapBuilder.SetTileCleared(tile.Item1, tile.Item2);
                moving = false;
                break;
            case 1:
            default:
                if (queuedMovements.Count > 0)
                {
                    MoveSmoothly(queuedMovements.Dequeue());
                }
                else
                {
                    moving = false;
                }
                break;
        }
        currentPosition = tile;
        selectedPosition = tile;
    }

    private void CreateMapUI(int[][] map)
    {
        for (int x = 0; x < map.Length; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                if (map[x][y] != 0)
                {
                    var go = Instantiate(tilePrefabs[map[x][y]]);
                    go.transform.SetParent(transform);
                    go.transform.localPosition = new Vector3((float)x * tileSpacing, (float)y * tileSpacing);
                    go.GetComponent<MapTile>().x = x;
                    go.GetComponent<MapTile>().y = y;
                    go.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        CalculatePath(go.GetComponent<MapTile>().x, go.GetComponent<MapTile>().y);
                    });
                    if (map[x][y] == 1)
                    {
                        currentPosition = new Tuple<int, int>(x, y);
                        playerObject = Instantiate(playerPrefab);
                        playerObject.transform.SetParent(transform);
                        playerObject.transform.localPosition = new Vector3((float)x * tileSpacing, (float)y * tileSpacing);
                    }
                }
            }
        }
        playerObject.transform.SetAsLastSibling();
    }
}
