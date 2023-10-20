using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public List<RoomSetup> roomSetups;
    [HideInInspector]
    public RoomSetup currentSetup;

    public List<int> GenerateNewSetup(int seed)
    {
        var randomedSetup = new List<int>();
        Random.InitState(seed);
        var randomSetupIndex = Random.Range(0, roomSetups.Count);
        //foreach (RoomSetup setup in roomSetups)
        //{
        //    setup.gameObject.SetActive(false);
        //}
        var currentSetup = roomSetups[randomSetupIndex];
        randomedSetup.Add(randomSetupIndex);
        //currentSetup.gameObject.SetActive(true);
        randomedSetup.AddRange(currentSetup.RandomObjects(seed));
        //Debug.Log($"Seed: {seed}, Randomed setup: {randomedSetup[0]}, {randomedSetup[1]}, {randomedSetup[2]}, {randomedSetup[3]}");
        return randomedSetup;
    }

    public void LoadSetup(List<int> setup)
    {
        foreach (RoomSetup roomSetup in roomSetups)
        {
            roomSetup.gameObject.SetActive(false);
        }
        if (setup.Count > 0)
        {
            roomSetups[setup[0]].gameObject.SetActive(true);
            roomSetups[setup[0]].LoadSetup(setup);
        }
        else
        {
            GenerateNewSetup(Random.Range(0, int.MaxValue));
        }
    }
}

public enum RoomType
{
    Nothing, //We should not be using this, just here to spot broken or unset rooms
    Fight,
    Chest
}
