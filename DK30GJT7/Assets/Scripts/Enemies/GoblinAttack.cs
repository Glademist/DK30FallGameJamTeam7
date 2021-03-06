﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : MonoBehaviour
{
    [SerializeField]
    GameObject bomb;

    GameObject thrownBomb;
    GameObject target;

    bool attacking = false;

    int targets = 0;

    private void FixedUpdate()
    {
        if (attacking && thrownBomb == null)
        {
            thrownBomb = Instantiate(bomb, new Vector3(0, 0, 0), Quaternion.identity);
            Rigidbody2D body = thrownBomb.GetComponent<Rigidbody2D>();
            Vector2 start = transform.position;
            Vector2 targetPos = target.transform.position;
            body.position = start + (targetPos - start) / 5f;
            body.velocity = (targetPos - start) * 10f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "Knight")
        {
            targets++;
            attacking = true;
            target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" || collision.gameObject.name == "Knight")
        {
            targets--;
            if(targets == 0)
            {
                attacking = false;
            }
        }
    }
}
