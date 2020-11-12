using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Camera cam;
    public Vector2 mousePos;

    [SerializeField]
    int keys = 0, gold = 0;

    [SerializeField]
    TMPro.TextMeshProUGUI keyText, goldText;
    ProgressBar actionProgress;

    float unlockDoor = 3.0f, currentTime = 0f;
    float maxInteractionDistance = 3.0f;

    Door doorBeingUnlocked;
    bool unlockingDoor = false;

    [SerializeField]
    public GameObject heldObject, targetedObject;
    Rigidbody2D heldObjectBody;
    bool foodHeld = false;
    
    public GameObject gotoCursor;
    public Intention intention;
    public ShowSpeech speech;

    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        actionProgress = new ProgressBar(gameObject, new Vector3(150, 15, 1), new Vector3(140, 12, 1), new Vector2(0, 0f), Color.black, Color.grey, false);
        actionProgress.ToggleVisible(false);
        UpdateKeys(0);
        audio = FindObjectOfType<AudioManager>();
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
            RaycastHit2D[] hits = Physics2D.RaycastAll(target, -Vector2.up);
            //Debug.Log("Mouse down");

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    //Debug.Log("Player interact:" + hit.collider.name);
                    Interact(hit.collider);
                }
            }

            // Throw an item
            if(heldObject != null)
            {
                Vector2 start = transform.position;
                heldObject.transform.parent = null;
                heldObjectBody.isKinematic = false;
                heldObjectBody.constraints = RigidbodyConstraints2D.None;
                heldObjectBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                heldObject.GetComponent<CircleCollider2D>().enabled = true;

                heldObject.transform.position = start + (target - start) / 5f;
                heldObjectBody.velocity = (target - start).normalized * 50f;
                
                //activate a thrown bomb
                Bomb bomb = heldObject.GetComponent<Bomb>();
                if (bomb)
                {
                    bomb.EnableBomb();
                    FindObjectOfType<AudioManager>().Play("Bomb_Throw");
                } else {
                    audio.Play("Throw");
                }
                heldObject = null;
            }
        }
        // Send knight to mouse position
        if (Input.GetMouseButtonDown(0))
        {
            if (foodHeld)
            {
                heldObject.GetComponentInChildren<Pickup>().EatFood(GetComponent<Health>());
                FindObjectOfType<AudioManager>().Play("Eat_Food");
                heldObject = null;
                targetedObject = null;
            }
            else
            {
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector2 goTo = (new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f));
                Instantiate(gotoCursor, new Vector3(goTo.x, goTo.y, 0), Quaternion.identity);
                if (GlobalReferences.Knight)
                {
                    KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
                    knightController.AddKnightStimulus(null, goTo, "go_to_player_pointer");
                }
            }
        }
        // Call the knight to your position
        if (Input.GetKeyDown("f"))
        {
            Debug.Log("Player calling Knight");
            Vector2 goTo = (new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f));
            intention.ShowIntention("player_call", .5f);
            if (GlobalReferences.Knight)
            {
                KnightController knightController = GlobalReferences.Knight.GetComponent<KnightController>();
                knightController.AddKnightStimulus(null, goTo, "go_to_player_call");
            }
        }

        if (Input.GetKeyDown("e"))
        {
            if(targetedObject != null)
            {
                PickupObject();
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
                    speech.Display();
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
            Debug.Log("flip lever");
            hit.gameObject.GetComponent<Lever>().FlipLever();
        }
        else if (hit.gameObject.GetComponent<Chest>())
        {
            hit.gameObject.GetComponent<Chest>().OpenChest();
        }
        else if (hit.gameObject.name == "Mimic")
        {
            hit.gameObject.GetComponent<EnemyBehaviour>().WakeUp(gameObject);
        }
    }

    public void UpdateKeys(int value)
    {
        keys += value;
        keyText.text = "x " + keys;
    }

    public void UpdateGold(int value)
    {
        gold += value;
        goldText.text = "x " + gold;
    }

    public void CancelInteration()
    {
        unlockingDoor = false;
        actionProgress.ToggleVisible(false);
    }

    void PickupObject()
    {
        heldObject = targetedObject;
        //targetedObject = null;

        Pickup pickup = heldObject.GetComponentInChildren<Pickup>();
        if (pickup) { foodHeld = pickup.isFood; }
        heldObject.transform.parent = gameObject.transform;
        heldObject.transform.localPosition = new Vector3(0, -0.3f);
        heldObjectBody = heldObject.GetComponent<Rigidbody2D>();
        heldObjectBody.isKinematic = true;
        heldObjectBody.constraints = RigidbodyConstraints2D.FreezeAll;
        CircleCollider2D body = heldObject.GetComponent<CircleCollider2D>();
        if (body) { body.enabled = false; }
    }
}
