using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public int damage = 2;
    float speed = 4f;

    float timer = 0.1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (timer > 0 && collision.gameObject.layer == 8)
        {
            return;
        }
        Health victim = collision.gameObject.GetComponent<Health>();
        if (victim)
        {
            victim.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        transform.Translate(0, speed * Time.deltaTime, 0);
    }
}
