using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    float explodeTime = 3f;
    float explodeRadius = 2f;
    int damage = 3;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (explodeTime > 0)
        {
            explodeTime -= Time.deltaTime;

            if (explodeTime < 0.3f)
            {
                anim.SetTrigger("Detonate");
            }
        }
        else
        {
            Explode();
        }
    }

    void Explode()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        //Debug.Log(string.Format("Found {0} collisions", collisions.Length));
        for (int i = 0; i < collisions.Length; i++)
        {
            Debug.Log(collisions[i].name);
            Health character = collisions[i].gameObject.GetComponent<Health>();
            if(character)
            {
                character.TakeDamage(damage);
            }
            Crate crate = collisions[i].gameObject.GetComponent<Crate>();
            if (crate)
            {
                crate.SmashCrate();
            }

        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/Desktop/SFX/sfx_bomb_explode", GetComponent<Transform>().position);
        Destroy(gameObject);
    }
}
