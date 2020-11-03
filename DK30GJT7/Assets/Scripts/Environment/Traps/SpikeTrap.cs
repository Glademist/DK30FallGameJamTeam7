using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public bool extended = false;
    Animator anim;

    float extendedTime = 2f, currentTime = 0f;

    private void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
        Debug.Log("animator"+ anim);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!extended && collision.gameObject.name == "Player")
        {
            anim.SetBool("Extended", true);
            currentTime = extendedTime;
        }

        Health victim = collision.gameObject.GetComponent<Health>();

        if (extended && victim != null)
        {
            Debug.Log("trying to deal damage");
            victim.TakeDamage(1);
        }
    }
}
