using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoomSetup : MonoBehaviour
{
    public List<GameObject> floors;
    public List<GameObject> walls;
    public List<RoomFlavour> flavourSetups;

    public List<int> RandomObjects(int seed)
    {
        var setupList = new List<int>();
        Random.InitState(seed);
        var randomFloor = Random.Range(0, floors.Count);
        var randomWall = Random.Range(0, walls.Count);
        var randomFlavour = Random.Range(0, flavourSetups.Count);
        setupList.Add(randomFloor);
        setupList.Add(randomWall);
        setupList.Add(randomFlavour);
        //foreach (var floor in floors)
        //{
        //    floor.SetActive(false);
        //}
        //floors[randomFloor].SetActive(true);
        //foreach (var wall in walls)
        //{
        //    wall.SetActive(false);
        //}
        //walls[randomWall].SetActive(true);
        //foreach (var setup in flavourSetups)
        //{
        //    foreach (var flavour in setup.flavourObjects)
        //    {
        //        flavour.SetActive(false);
        //    }
        //}
        //foreach (var flavour in flavourSetups[randomFlavour].flavourObjects)
        //{
        //    flavour.SetActive(true);
        //}
        return setupList;
    }

    public void LoadSetup(List<int> savedSetup)
    {
        foreach (var floor in floors)
        {
            floor.SetActive(false);
        }
        floors[savedSetup[1]].SetActive(true);
        foreach (var wall in walls)
        {
            wall.SetActive(false);
        }
        walls[savedSetup[2]].SetActive(true);
        foreach (var setup in flavourSetups)
        {
            foreach (var flavour in setup.flavourObjects)
            {
                flavour.SetActive(false);
            }
        }
        foreach (var flavour in flavourSetups[savedSetup[3]].flavourObjects)
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
        var seed = Random.Range(0, int.MaxValue);
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

