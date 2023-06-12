using System;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public Room roomData;
    public Action OnRoomGenerated = delegate { };

    [ContextMenu("Generate new room")]
    private void GenerateNewRoom()
    {
        var seed = UnityEngine.Random.Range(0, int.MaxValue);
        if (roomData.GenerateNewSetup(seed))
        {
            OnRoomGenerated();
            return;
        }
        Debug.LogError("Room generation failed!");
    }
}
