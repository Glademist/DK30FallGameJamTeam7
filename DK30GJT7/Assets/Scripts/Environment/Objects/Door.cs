﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField]
    public bool open = false;
    [SerializeField]
    bool locked = false;
    public Sprite openDoor, closedDoor;
    public Tilemap walls;
    public RectInt Location;
    public Tile Ground;
    public List<Room> rooms;
    private GameObject knight;
    public KnightMove knightM;

    SpriteRenderer spriteRend;
    BoxCollider2D collider;

    //handle lighting
    //public Lighting connectedRoomA, connectedRoomB;

    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        knight = GlobalReferences.Knight;
        audio = FindObjectOfType<AudioManager>();
    }
    
    void UpdateDoor()
    {
        if (open)
        {
            spriteRend.sprite = openDoor;
            collider.isTrigger = true;
            UpdateTilemap(walls, Location, null);
            if(knight != null){
                knight.GetComponent<KnightMove>().CheckDoors();
            }
        } else
        {
            //if (!collider.IsTouchingLayers(-1)){
                spriteRend.sprite = closedDoor;
                collider.isTrigger = false;
                UpdateTilemap(walls, Location, Ground);
                if(knight != null){
                    knight.GetComponent<KnightMove>().CheckDoors();
                }
            //}
        }
        /*
        if (connectedRoomA)
        {
            connectedRoomA.ChangeVisibility(open);
        }
        if (connectedRoomB)
        {
            connectedRoomB.ChangeVisibility(open);
        }*/
    }

    public void UpdateTilemap(Tilemap map, RectInt area, Tile tile)
    {
        foreach (Vector2Int spot in area.allPositionsWithin)
        {
            //iterate through and set tiles on map to tile
        }
    }

    void ToggleDoor()
    {
        open = !open;
        if (open)
        {
            audio.Play("Chest_Open");
        }
        else
        {
            audio.Play("Door_Close");
        }
        UpdateDoor();
        if (knightM != null)
        {
            knightM.CheckDoors();
        }
    }

    //returns true if door gets opened, false if needs unlocked
    public bool TryOpenDoor()
    {
        if (locked)
        {
            return false;
        }
        ToggleDoor();
        return true;
    }

    public void UnlockDoor()
    {
        locked = false;
    }
}
