using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    bool open = false;
    public Sprite openDoor, closedDoor;

    SpriteRenderer spriteRend;
    BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }


    private void FixedUpdate()
    {
        
    }
    
    void UpdateDoor()
    {
        if (open)
        {
            spriteRend.sprite = openDoor;
            collider.isTrigger = true;
        } else
        {
            spriteRend.sprite = closedDoor;
            collider.isTrigger = false;
        }
    }

    public void ToggleDoor()
    {
        open = !open;
        UpdateDoor();
    }
}
