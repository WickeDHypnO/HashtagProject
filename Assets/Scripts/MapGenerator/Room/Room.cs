using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room 
{
    public List<RoomSetup> roomSetups;
    [HideInInspector]
    public RoomSetup currentSetup;

    public bool GenerateNewSetup(int seed)
    { 
        Random.InitState(seed);
        var randomSetupIndex = Random.Range(0, roomSetups.Count);
        foreach(RoomSetup setup in roomSetups)
        {
            setup.gameObject.SetActive(false);
        }
        var currentSetup = roomSetups[randomSetupIndex];
        currentSetup.gameObject.SetActive(true);
        currentSetup.RandomObjects(seed);
        return true;
    }
}

public enum RoomType
{
    Nothing, //We should not be using this, just here to spot broken or unset rooms
    Fight,
    Chest
}
