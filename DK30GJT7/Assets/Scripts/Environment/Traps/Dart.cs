using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public int damage = 2;
    float speed = 4f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health victim = collision.gameObject.GetComponent<Health>();
        if (victim)
        {
            victim.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
