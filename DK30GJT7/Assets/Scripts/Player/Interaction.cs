﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    public Vector2 mousePos;

    [SerializeField]
    int keys = 0, food = 2;

    [SerializeField]
    TMPro.TextMeshProUGUI keyText;
    ProgressBar actionProgress;

    float unlockDoor = 3.0f, currentTime = 0f;
    float maxInteractionDistance = 3.0f;

    Door doorBeingUnlocked;
    bool unlockingDoor = false;
    
    public GameObject gold;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        actionProgress = new ProgressBar(gameObject, new Vector3(150, 15, 1), new Vector3(140, 12, 1), new Vector2(0, 0f), Color.black, Color.grey, false);
        actionProgress.ToggleVisible(false);

        UpdateKeys(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Interact with item at mouse position
        if (unlockingDoor)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                actionProgress.UpdateHealthbar(unlockDoor - currentTime, unlockDoor);
            }
            else
            {
                doorBeingUnlocked.UnlockDoor();
                doorBeingUnlocked.TryOpenDoor();
                actionProgress.ToggleVisible(false);
                unlockingDoor = false;
                UpdateKeys(-1);
            }
        }


        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), -Vector2.up);
            //Debug.Log("Mouse down");
            if (hit.collider != null)
            {
                //Debug.Log("Player interact:" + hit.collider.name);
                Interact(hit.collider);
            }
        }
        // Send knight to mouse position
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 goTo = (new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f));
            KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
            knightController.AddKnightStimulus(null, goTo, "player_order");
        }
        // Call the knight to your position
        if (Input.GetKeyDown("e")){
            //Debug.Log("Player calling Knight");
            Vector2 goTo = (new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f));
            KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
            knightController.AddKnightStimulus(null, goTo, "player_call");
        }
        if (Input.GetMouseButtonDown(1))
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
                Debug.Log(hit.collider.name);
                Door door = hit.collider.gameObject.GetComponent<Door>();
                if (door == null)
                {
                    return;
                }
                if (!door.TryOpenDoor())
                {
                    if (keys <= 0)
                    {
                        Debug.Log("no keys");
                        return;
                    }
                    if (Vector2.Distance(transform.position, door.transform.position) > maxInteractionDistance)
                    {
                        Debug.Log("too far away");
                        return;
                    }
                    unlockingDoor = true;
                    currentTime = unlockDoor;
                    doorBeingUnlocked = door;
                    actionProgress.ToggleVisible(true);
                }

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject thrownGold = Instantiate(gold, new Vector3(0, 0, 0), Quaternion.identity);
            Rigidbody2D body = thrownGold.GetComponent<Rigidbody2D>();
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 start = transform.position;
            body.position = start + (target - start) / 5f;
            body.velocity = target - start;

        }
    }

    public void UpdateKeys(int value)
    {
        keys += value;
        keyText.text = "x " + keys;
    }

    public void CancelInteration()
    {
        unlockingDoor = false;
        actionProgress.ToggleVisible(false);
    }
}
