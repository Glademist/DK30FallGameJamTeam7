﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectKey : MonoBehaviour
{


    void OnTriggerEnter2D(Collider2D collision)
    {
        Interaction player = collision.gameObject.GetComponent<Interaction>();
        if (player != null)
        {
            player.UpdateKeys(1);
            Destroy(gameObject);
        }
    }
}
