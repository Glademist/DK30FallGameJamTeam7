using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            Debug.Log("Mouse down");
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                Door door = hit.collider.gameObject.GetComponent<Door>();
                if (door != null)
                {
                    Debug.Log("toggle door");
                    door.ToggleDoor();
                }
            }
        }
    }
}
