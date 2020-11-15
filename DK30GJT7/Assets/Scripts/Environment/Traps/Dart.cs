using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public int damage = 2;
    float speed = 4f;

    float timer = 0.15f;
    public float rotation;

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
        FindObjectOfType<AudioManager>().Play("Dart_Hit");
        Destroy(gameObject);
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        transform.Translate(0, speed * Time.deltaTime, 0);

        if(transform.rotation != Quaternion.Euler(0, 0, rotation))
        {
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
