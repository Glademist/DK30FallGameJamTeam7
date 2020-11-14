using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightDecisionTree : MonoBehaviour
{
    [Header("emotions")]
    public float Comfort = 0; //interest in staying put
    public float UnComfort = 0; //interest in moving forward
    public float Distracted = 0; //interest in new things
    public float Focused = 0; //interest in present things
    public float Exhaustion = 0; //interest in avoiding combat
    public float Energized = 0; //interest in combat
    public float Fear = 0; //interest in avoiding danger
    public float Courage = 0; //interest in facing danger

    public List<Interest> Interests;
    public List<Interest> KnownInterests;
    public Interest CurrentInterest;
    public Room InterestedRoom;
    public KnightMove knightMove;
    int resetTarget = 0;

    //interest interaction
    float interactionDistance = 2f;
    KnightInteract interact;

    // Start is called before the first frame update
    void Awake()
    {
        knightMove = this.GetComponent<KnightMove>();
        interact = GetComponent<KnightInteract>();
        CurrentInterest = null;
        GetInterests();
    }



    // Update is called once per frame
    void Update()
    {
        UpdateRoomInterest();
        ChooseInterest();
        if (knightMove.currentRoom != null)
        {
            UpdateInterests(knightMove.currentRoom.room);
        }
        if (CurrentInterest == null)
        {
            TargetRoom();
        }
        else if (InterestedRoom.Interest > CurrentInterest.CurrentInterest)
        {
            TargetRoom();
        }
        else
        {
            TargetObject();
        }
    }

    public void GetInterests()
    {
        Interests.Clear();
        foreach (Interest interest in FindObjectsOfType<Interest>())
        {
            Interests.Add(interest);
        }
    }

    public void UpdateRoomInterest()
    {
        foreach (Room Room in knightMove.rooms)
        {
            if (Room == knightMove.currentRoom)
            {
                if (Room.InterestGrowth >= -0.1f)
                {
                    Room.InterestGrowth -= 0.001f;
                }
            }
            else
            {
                if (Room.InterestGrowth <= 0.1f)
                {
                    Room.InterestGrowth += 0.001f;
                }
            }
            if (Room.Interest <= 30 || Room.InterestGrowth < 0)
            {
                Room.Interest += Room.InterestGrowth;
            }
        }
    }

    public void UpdateInterests(RectInt Area)
    {
        for (int i = 0; i < Interests.Count; i++)
        {
            if (Interests[i] == null)
            {
                Interests.Remove(Interests[i]);
                KnownInterests.Remove(Interests[i]);
                if (CurrentInterest == Interests[i])
                {
                    CurrentInterest = null;
                }
            }
            else if (Area.Contains(Vector2Int.RoundToInt(Interests[i].transform.position)) && !KnownInterests.Contains(Interests[i]))
            {
                Interests[i].CurrentInterest = 50;
                KnownInterests.Add(Interests[i]);
            }
        }
    }

    public void ChooseInterest()
    {
        if (KnownInterests.Count > 0)
        {
            foreach (Interest Interest in KnownInterests)
            {
                if (CurrentInterest == null)
                {
                    CurrentInterest = Interest;
                }
                else
                {
                    Interest NextInterest = CurrentInterest;
                    if (NextInterest.CurrentInterest < Interest.CurrentInterest)
                    {
                        NextInterest = Interest;
                    }
                    CurrentInterest = NextInterest;
                }
            }
        }
        foreach (Room Room in knightMove.AdjacentRooms())
        {
            if (InterestedRoom == null)
            {
                InterestedRoom = Room;
            }
            else
            {
                Room NextRoom = InterestedRoom;
                if (NextRoom.Interest < Room.Interest)
                {
                    NextRoom = Room;
                }
                if (NextRoom.Interest - Vector2.Distance(this.transform.position, Room.room.center) >= InterestedRoom.Interest)
                {
                    InterestedRoom = NextRoom;
                }
            }
        }
    }

    public void TargetRoom()
    {
        if (InterestedRoom != knightMove.currentRoom)
        {
            Vector2 TargetSpot = InterestedRoom.room.center; //+ (Vector2.up * Random.Range(-InterestedRoom.room.width / 2, InterestedRoom.room.width / 2)) + (Vector2.right * Random.Range(-InterestedRoom.room.width / 2, InterestedRoom.room.width / 2));
            knightMove.CommandKnight(TargetSpot);
        }
    }

    public void TargetObject()
    {
        resetTarget++;
        if (CurrentInterest != knightMove.TargetInterest || resetTarget >= 10 && !interact.IsAttacking())
        {
            resetTarget = 0;
            knightMove.TargetInterest = CurrentInterest;
            knightMove.CommandKnight(CurrentInterest.transform.position);
        }
        if (Vector2.Distance(CurrentInterest.transform.position, transform.position) < interactionDistance)
        {
            InteractWithInterest();
        }
    }

    void InteractWithInterest()
    {

        switch (CurrentInterest.InterestType)
        {
            case "Enemy":
                if (!interact.IsAttacking())
                {
                    interact.StartAttacking(CurrentInterest.gameObject);
                }
                break;
            case "Gold":
                if (interact.IsGold(CurrentInterest.gameObject))
                {
                    interact.CollectGold(CurrentInterest.gameObject);
                    //reduce greed stat
                }
                break;
            case "Food":
                if (interact.IsFood(CurrentInterest.gameObject))
                {
                    interact.EatFood(CurrentInterest.gameObject);
                    //reduce hunger/increase energy
                }
                break;
            case "Chest":
                if (interact.IsChest(CurrentInterest.gameObject))
                {
                    interact.OpenChest(CurrentInterest.gameObject);
                }
                break;
        }
    }
}
