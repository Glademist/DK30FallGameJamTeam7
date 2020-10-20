using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = false;
    public Sprite openDoor, closedDoor;
    public Door connectedDoor;
    public Vector2 offset; //offset the player when they go through the door
    public int roomNo;

    SpriteRenderer spriteRend;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        //calling from a fixed update but can be done by game manager when room is cleared
        UpdateDoor();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(open && collision.gameObject.name == "Player")
        {
            Vector2 targetPosition = connectedDoor.transform.position;
            collision.gameObject.transform.position = targetPosition + offset;
            //update room number
            RoomManager.UpdateCurrentRoom(connectedDoor.roomNo);
        }
    }

    void UpdateDoor()
    {
        if (open)
        {
            spriteRend.sprite = openDoor;
        } else
        {
            spriteRend.sprite = closedDoor;
        }
    }
}
