using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    Interaction player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(player.targetedObject == null)
        {
            Debug.Log("player can pick up");
            player.targetedObject = transform.parent.gameObject;
        }
    }
}
