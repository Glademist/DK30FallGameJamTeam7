using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    bool open = false;
    [SerializeField]
    bool locked = false;
    public Sprite openDoor, closedDoor;

    SpriteRenderer spriteRend;
    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }
    
    void UpdateDoor()
    {
        if (open)
        {
            spriteRend.sprite = openDoor;
            collider.isTrigger = true;
        } else
        {
            if (!collider.IsTouchingLayers(-1)){
                spriteRend.sprite = closedDoor;
                collider.isTrigger = false;
            }
        }
    }

    void ToggleDoor()
    {
        open = !open;
        UpdateDoor();
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
