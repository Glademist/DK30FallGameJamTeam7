using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggledSpikeTrap : MonoBehaviour
{
    public bool extended = true;
    Animator anim;
    List<Health> targetObjects = new List<Health>();
    float cooldown = 2f, currentTime = 0f;

    public int damage = 1;

    private void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
        anim.SetBool("Extended", true);
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (extended && currentTime <= 0 && targetObjects.Count > 0)
        {
            foreach(Health victim in targetObjects)
            {
                victim.TakeDamage(damage);
            }
            currentTime = cooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health victim = collision.gameObject.GetComponent<Health>();
        if (victim)
        {
            targetObjects.Add(victim);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Health victim = collision.gameObject.GetComponent<Health>();
        if (victim)
        {
            targetObjects.Remove(victim);
        }
    }

    public void Flip(bool state)
    {
        extended = state;
        anim.SetBool("Extended", extended);
    }
}
