using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room
{
    public RectInt room;
    public float Interest;
    public float addedInterest;
    public float InterestGrowth = 0.5f;
    public bool isAccessible = false;
    public List<Door> Doors;
    public Room(RectInt r, float i, float ig)
    {
        room = r;
        Interest = i;
        InterestGrowth = ig;
    }
}
