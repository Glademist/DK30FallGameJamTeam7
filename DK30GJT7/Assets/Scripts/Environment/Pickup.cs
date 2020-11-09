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
        if(player)
        {
            if (player.targetedObject == null)
            {
                player.targetedObject = transform.parent.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Interaction player = collision.GetComponent<Interaction>();
        if (player)
        {
            if (player.targetedObject == transform.parent.gameObject)
            {
                player.targetedObject = null;
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
