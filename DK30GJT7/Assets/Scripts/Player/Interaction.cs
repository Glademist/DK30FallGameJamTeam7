using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
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

    [SerializeField]
    public GameObject heldObject, targetedObject;
    Rigidbody2D heldObjectBody;

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

        // Interact with item at mouse position
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(target, -Vector2.up);
            //Debug.Log("Mouse down");
            if (hit.collider != null)
            {
                //Debug.Log("Player interact:" + hit.collider.name);
                Interact(hit.collider);
            }

            // Throw an item
            if(heldObject != null)
            {
                Vector2 start = transform.position;
                Debug.Log("trying to throw a held object");
                heldObject.transform.parent = null;
                heldObjectBody.isKinematic = false;
                heldObjectBody.constraints = RigidbodyConstraints2D.None;
                heldObjectBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                heldObject.GetComponent<CircleCollider2D>().enabled = true;

                heldObject.transform.position = start + (target - start) / 5f;
                heldObjectBody.velocity = (target - start) * 3f;

                heldObject = null;
                targetedObject = null;
            }
        }
        // Send knight to mouse position
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 goTo = (new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f));
            if (GlobalReferences.Knight)
            {
                KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
                knightController.AddKnightStimulus(null, goTo, "player_call");
            }
        }
        // Call the knight to your position
        if (Input.GetKeyDown("f"))
        {
            //Debug.Log("Player calling Knight");
            Vector2 goTo = (new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f));
            if (GlobalReferences.Knight)
            {
                KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
                knightController.AddKnightStimulus(null, goTo, "player_call");
            }
        }
        /*
        // Throw an item
        if (Input.GetKeyDown("r"))
        {
            GameObject thrownGold = Instantiate(gold, new Vector3(0, 0, 0), Quaternion.identity);
            Rigidbody2D body = thrownGold.GetComponent<Rigidbody2D>();
            Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 start = transform.position;
            body.position = start + (target - start) / 5f;
            body.velocity = target - start;

        }*/

        if (Input.GetKeyDown("e"))
        {
            if(targetedObject != null)
            {
                Debug.Log("trying to pick up targeted object");
                heldObject = targetedObject;
                targetedObject = null;
                heldObject.transform.parent = gameObject.transform;
                heldObject.transform.localPosition = new Vector3(0, -0.3f);
                heldObjectBody = heldObject.GetComponent<Rigidbody2D>();
                heldObjectBody.isKinematic = true;
                heldObjectBody.constraints = RigidbodyConstraints2D.FreezeAll;
                heldObject.GetComponent<CircleCollider2D>().enabled = false;
                
            }
        }
    }



    public void Interact(Collider2D hit)
    {
        if (Vector2.Distance(transform.position, hit.gameObject.transform.position) > maxInteractionDistance)
        {
            Debug.Log("too far away");
            return;
        }

        if (hit.gameObject.GetComponent<Door>())
        {
            Door door = hit.gameObject.GetComponent<Door>();
            if (!door.TryOpenDoor())
            {
                if (keys <= 0)
                {
                    Debug.Log("no keys");
                    return;
                }
                unlockingDoor = true;
                currentTime = unlockDoor;
                doorBeingUnlocked = door;
                actionProgress.ToggleVisible(true);
            }

        }
        else if (hit.gameObject.GetComponent<Lever>())
        {
            
            Lever lever = hit.gameObject.GetComponent<Lever>();
            lever.FlipLever();
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
