using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomSetup : MonoBehaviour
{
    public List<GameObject> floors;
    public List<GameObject> walls;
    public List<RoomFlavour> flavourSetups;

    public void RandomObjects(int seed)
    {
        Random.InitState(seed);
        var randomFloor = Random.Range(0, floors.Count);
        var randomWall = Random.Range(0, walls.Count);
        var randomFlavour = Random.Range(0, flavourSetups.Count);
        foreach (var floor in floors)
        {
            floor.SetActive(false);
        }
        floors[randomFloor].SetActive(true);
        foreach (var wall in walls)
        {
            wall.SetActive(false);
        }
        walls[randomWall].SetActive(true);
        foreach (var setup in flavourSetups)
        {
            foreach (var flavour in setup.flavourObjects)
            {
                flavour.SetActive(false);
            }
        }
        foreach (var flavour in flavourSetups[randomFlavour].flavourObjects)
        {
            flavour.SetActive(true);
        }
    }

    [System.Serializable]
    public struct RoomFlavour
    {
        public List<GameObject> flavourObjects;
    }

//Editor helpers - probably make it into a custom buttons in editor
    [ContextMenu("Generate random setup")]
    private void GenerateSetup()
    {
        var seed = Random.Range(0,int.MaxValue);
        RandomObjects(seed);
    }

    [ContextMenu("Disable all objetcs")]
    private void DisableAllObjects()
    {
        foreach (var floor in floors)
        {
            floor.SetActive(false);
        }
        foreach (var wall in walls)
        {
            wall.SetActive(false);
        }
        foreach (var setup in flavourSetups)
        {
            foreach (var flavour in setup.flavourObjects)
            {
                flavour.SetActive(false);
            }
        }
    }
}

