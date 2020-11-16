using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinKing : MonoBehaviour
{
    bool fightStarted = false;
    public GameObject healthbar;
    EnemyBehaviour behaviour;


    public GameObject knight;
    public GameObject goblin, fakeGold, foodCrate;
    float throwCooldown = 6f, currentCooldown = 10f;
    float throwRange = 2f, tempThrowRange = 2f;

    //create ladder when it dies
    public GameObject ladder;
    AudioManager audio;

    // Start is called before the first frame update
    void Start()
    {
        behaviour = GetComponent<EnemyBehaviour>();
        GetComponent<Health>().HideHealth();
        audio = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(behaviour.IsSeeking())
        {
            if (!fightStarted)
            {
                StartFight();
            }
        }
        if ((behaviour.IsSeeking() || behaviour.IsAttacking()) && currentCooldown <= 0)
        {
            tempThrowRange = throwRange;
            ThrowObjects(3);
            currentCooldown = throwCooldown;
        }
    }

    public void StartFight()
    {
        if (!fightStarted)
        {
            fightStarted = true;
            healthbar.SetActive(true);
            //behaviour.currentTarget = knight;
            /*
            knight.transform.position = behaviour.currentTarget.transform.position;
            behaviour.currentTarget = knight;
            KnightMove knightMove = knight.GetComponent<KnightMove>();
            knightMove.currentRoom = knightMove.rooms[4];
            knightMove.CheckDoors();
            KnightDecisionTree knightAI = knight.GetComponent<KnightDecisionTree>();
            //knightAI.Interests = null;
            knightAI.KnownInterests = null;
            knightAI.CurrentInterest = null;
            //knightAI.UpdateRoomInterest();*/
        }
    }

    void ThrowObjects(int amount)
    {
        for(int x = 0; x < amount; x++)
        {
            ThrowObject();
        }
        audio.Play("Object_Land");
    }

    void ThrowObject()
    {
        GameObject thrownObject = Instantiate(GetRandomObject(), new Vector3(0, 0, 0), Quaternion.identity);
        Rigidbody2D body = thrownObject.GetComponent<Rigidbody2D>();
        Vector2 target = GetThrowPosition();
        //Debug.Log(offset);
        thrownObject.transform.position = target;
        //body.velocity = (target) * 4;
        thrownObject.AddComponent<AddToInterests>();

        Destroy(thrownObject, 20);
    }

    Vector2 GetThrowPosition()
    {
        Vector2 target = transform.position + (new Vector3(Random.Range(-tempThrowRange, tempThrowRange), Random.Range(-tempThrowRange, tempThrowRange)));
        tempThrowRange -= 0.5f;
        Debug.Log(target);
        RaycastHit2D[] hits = Physics2D.RaycastAll(target, -Vector2.up);

        foreach (RaycastHit2D hit in hits)
        {
            if (!hit.collider.isTrigger)
            {
                Debug.Log(hit.collider.name);
                target = transform.position;
                return target;
            }
        }

        return target;
    }

    private GameObject GetRandomObject()
    {
        switch (Random.Range(0, 5))
        {
            case 0:
            case 1:
                return goblin;
            case 2:
            case 3:
                return fakeGold;
            case 4:
                return foodCrate;
        }
        return goblin;
    }

    private void OnDestroy()
    {
        ladder.SetActive(true);
    }
}
