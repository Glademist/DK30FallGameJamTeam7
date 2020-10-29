using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Interaction player = collision.GetComponent<Interaction>();
        if(player != null)
        {
            if (player.targetedObject == null)
            {
                Debug.Log("player can pick up");
                player.targetedObject = transform.parent.gameObject;
            }
        }

    }
}
