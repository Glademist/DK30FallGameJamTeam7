using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    public Vector2 mousePos;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            //Debug.Log("Mouse down");
            if (hit.collider != null)
            {
                Debug.Log("Player interact:" + hit.collider.name);
                Interact(hit.collider);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 goTo = (new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f));
            KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
            knightController.AddKnightStimulus(null, goTo, "player_order");
        }
    }

    public void Interact(Collider2D hit){
        if(hit.gameObject.GetComponent<Door>())
        {
            Door door = hit.gameObject.GetComponent<Door>();
            door.ToggleDoor();
        }
    }
}
