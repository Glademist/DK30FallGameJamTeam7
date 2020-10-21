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
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (extended)
        {
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
            } else
            {
                extended = false;
                anim.SetBool("Extended", false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!extended && collision.gameObject.name == "Player")
        {
            extended = true;
            anim.SetBool("Extended", true);
            currentTime = extendedTime;
        }
    }
}
