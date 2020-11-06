using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isFood = false;
    public int restoration = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interaction player = collision.GetComponent<Interaction>();
        if(player != null)
        {
            if (player.targetedObject == null)
            {
                //Debug.Log("player can pick up");
                player.targetedObject = transform.parent.gameObject;
            }
        }
    }


    public void EatFood(Health consumer)
    {
        if (isFood)
        {
            consumer.Heal(restoration);
            Destroy(transform.parent.gameObject);
        }
    }
}
