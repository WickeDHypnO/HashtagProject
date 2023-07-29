using System;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room roomData;
    public RoomType roomType;
    public System.Action OnRoomGenerated = delegate { };
    [SerializeField]
    private MapBuilder mapBuilder;
    [SerializeField]
    private GameObject chestPrefab;
    [SerializeField]
    private GameObject fightPrefab;
    private GameObject currenChest;
    private GameObject currentFight;

    private void OnEnable()
    {
        mapBuilder.OnMapGenerated += GenerateRooms;
    }

    private void OnDisable()
    {
        mapBuilder.OnMapGenerated += GenerateRooms;
    }

    private void GenerateRooms(int[][] map)
    {
        Dictionary<Tuple<int, int>, List<int>> rooms = new Dictionary<Tuple<int, int>, List<int>>();
        for (int x = 0; x < map.Length; x++)
        {
            for (int y = 0; y < map[x].Length; y++)
            {
                if (map[x][y] != 0)
                {
                    rooms[new Tuple<int, int>(x, y)] = roomData.GenerateNewSetup(UnityEngine.Random.Range(0, int.MaxValue));
                }
            }
        }

        mapBuilder.StoreRooms(rooms);
    }

    public void LoadSetup(List<int> setup)
    {
        if (currenChest) Destroy(currenChest);
        if (currentFight) Destroy(currentFight);
        Debug.Log($"Loading setup {setup[0]}, {setup[1]}, {setup[2]},{setup[3]}");
        roomData.LoadSetup(setup);
    }

    public void SpawnChest()
    {
        currenChest = Instantiate(chestPrefab, transform);
    }

    public void SpawnFight()
    {
        currentFight = Instantiate(fightPrefab, transform);
    }

    [ContextMenu("Generate new room")]
    public void GenerateNewRoom(RoomType type)
    {
        var seed = UnityEngine.Random.Range(0, int.MaxValue);
        roomData.GenerateNewSetup(seed);
        roomType = type;
        //TODO: Generate interaction according to the room type
        OnRoomGenerated();
    }
}
