using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour
{
    AudioManager audio;
    float stepDelay = 0.4f, currentDelay = 0f;
    List<string> sounds;

    Animator anim;
    Vector3 lastPos;
    bool moving = false;

    private void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        sounds = AudioMixing.GetMovementSounds("Knight");
        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        if((transform.position - lastPos).magnitude > 0.01)  //moved
        {
            if (!moving)
            {
                anim.SetBool("Moving", true);
                moving = true;
            }
        }
        else
        {
            if (moving)
            {
                anim.SetBool("Moving", false);
                moving = false;
            }
        }
        lastPos = transform.position;
    }


    private void Update()
    {
        if(moving)
        {
            
            if (currentDelay > 0)
            {
                currentDelay -= Time.deltaTime;
            }
            else
            {
                int index = Random.Range(0, sounds.Count);
                audio.Play(sounds[index]);
                currentDelay = stepDelay;
            }
        }
    }
}
