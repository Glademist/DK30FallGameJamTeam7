using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    GameObject knight, player;
    float aggroRange, attackRange;
    GameObject currentTarget;

    enum State {Wandering, Seeking, Attacking}
    State currentState;


    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Wandering;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState == State.Wandering)
        {
            //while wandering enemy will path randomly in a radius

            //if(Health.recentlyTakenDamage)    add some damage taken boolean which determines if damage was taken recently
            //{
                //path back away from trap, use previously taken path if possible
            //}

            if(Vector2.Distance(knight.transform.position, transform.position) < aggroRange)
            {
                currentState = State.Seeking;   //seeking means trying to get into combat range
                currentTarget = knight;
            }

            //can have different aggro ranges for knight or player
            if (Vector2.Distance(player.transform.position, transform.position) < aggroRange)
            {
                currentState = State.Seeking;   //seeking means trying to get into combat range
                currentTarget = player;
            }
        }

        if(currentState == State.Seeking)
        {
            //path towards current target

            if(Vector2.Distance(currentTarget.transform.position, transform.position) < attackRange)
            {
                currentState = State.Attacking;
                //start attacking target
            }


            if (Vector2.Distance(currentTarget.transform.position, transform.position) > aggroRange)
            {
                currentState = State.Wandering;
                //stop chasing target and resume wandering
            }
        }

        if (currentState == State.Attacking)
        {
            //attack current target

            if (Vector2.Distance(currentTarget.transform.position, transform.position) > attackRange)
            {
                currentState = State.Seeking;
                //chase target to get back into attack range
            }

        }
    }
}
