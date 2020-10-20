using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]
    static int currentRoom = 0;

    public static void UpdateCurrentRoom(int room)
    {
        currentRoom = room;
        Debug.Log("Current room is now " + currentRoom);
    }
}
