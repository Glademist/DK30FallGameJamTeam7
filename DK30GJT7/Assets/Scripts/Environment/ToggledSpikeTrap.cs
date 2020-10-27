using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggledSpikeTrap : MonoBehaviour
{
    public bool extended = true;
    Animator anim;

    private void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
        Debug.Log("animator" + anim);
        anim.SetBool("Extended", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health victim = collision.gameObject.GetComponent<Health>();

        if (extended && victim != null)
        {
            Debug.Log("trying to deal damage");
            victim.TakeDamage(1);
        }
    }

    public void Flip(bool state)
    {
        extended = state;
        anim.SetBool("Extended", extended);
    }
}
