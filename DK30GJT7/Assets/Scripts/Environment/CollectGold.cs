using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGold : MonoBehaviour
{
    public int goldAmount = 1;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Interaction player = collision.gameObject.GetComponent<Interaction>();
        if (player != null)
        {
            player.UpdateGold(goldAmount);
            Destroy(gameObject);
        }
    }

    public void LootGold()  //knight looting gold
    {
        Destroy(gameObject);
    }
}