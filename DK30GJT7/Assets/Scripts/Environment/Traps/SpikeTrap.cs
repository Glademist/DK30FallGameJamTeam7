using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public bool extended = false;
    Animator anim;

    //manage extension
    float extendedTime = 2f, currentTime = 0f;
    //manage damage
    [SerializeField]
    List<Health> targetObjects = new List<Health>();
    float cooldown = 2f, currentDamageTime = 0f;

    private void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            if(!extended && currentTime > extendedTime / 2f)
            {
                extended = true;
            }

        }
        else
        {
            extended = false;
            anim.SetBool("Extended", false);
        }

        if (currentDamageTime > 0)
        {
            currentDamageTime -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (extended && currentDamageTime <= 0 && targetObjects.Count > 0)
        {
            foreach (Health victim in targetObjects)
            {
                victim.TakeDamage(1);
            }
            currentDamageTime = cooldown;
        }

        if (!extended && targetObjects.Count > 0)
        {
            anim.SetBool("Extended", true);
            currentTime = extendedTime;
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
}
