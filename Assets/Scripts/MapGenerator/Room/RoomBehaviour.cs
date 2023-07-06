using System;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room roomData;
    public RoomType roomType;
    public Action OnRoomGenerated = delegate { };

    [ContextMenu("Generate new room")]
    public void GenerateNewRoom(RoomType type)
    {
        var seed = UnityEngine.Random.Range(0, int.MaxValue);
        if (roomData.GenerateNewSetup(seed))
        {
            roomType = type;
            //TODO: Generate interaction according to the room type
            OnRoomGenerated();
            return;
        }
        Debug.LogError("Room generation failed!");
    }
}
