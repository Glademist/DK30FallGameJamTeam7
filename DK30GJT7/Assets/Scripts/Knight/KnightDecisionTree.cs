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
    public Interest CurrentInterest;
    public Room InterestedRoom;
    public KnightMove knightMove;
    public CircleCollider2D InterestRange;
    int resetTarget = 0;

    // Start is called before the first frame update
    void Awake()
    {
        knightMove = this.GetComponent<KnightMove>();
        CurrentInterest = null;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInterests();
        UpdateRoomInterest();
        ChooseInterest();
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

    public void AddInterest(Interest Interest)
    {
        Interest.CurrentInterest = Interest.InitInterest;
        Interests.Add(Interest);
    }

    public void UpdateInterests()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<HasInterest>() != null)
        {
            if (!Interests.Contains(collision.gameObject.GetComponent<HasInterest>().Interest))
            {
                AddInterest(collision.gameObject.GetComponent<HasInterest>().Interest);
            }
        }
    }

    public void ChooseInterest()
    {
        if (Interests.Count > 0)
        {
            foreach (Interest Interest in Interests)
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
        foreach (Room Room in knightMove.AccessibleRooms)
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
        if (CurrentInterest != knightMove.TargetInterest || resetTarget >= 10)
        {
            resetTarget = 0;
            knightMove.TargetInterest = CurrentInterest;
            knightMove.CommandKnight(CurrentInterest.Object.transform.position);
        }
    }
}
