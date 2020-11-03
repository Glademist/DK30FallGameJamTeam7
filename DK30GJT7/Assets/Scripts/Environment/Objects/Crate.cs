using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField]
    GameObject storedObject;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > 10)
        {
            if (collision.gameObject.GetComponentInChildren<Pickup>())
            {
                if (storedObject != null)
                {
                    GameObject reward = Instantiate(storedObject, (Vector2)transform.position, Quaternion.identity);
                }
                Destroy(gameObject);
            }
        }
    }
}
