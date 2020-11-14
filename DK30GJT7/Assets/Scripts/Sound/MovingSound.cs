using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSound : MonoBehaviour
{
    public string material;
    public bool playing = false;
    Rigidbody2D body;
    AudioManager audio;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        audio = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        if(body.velocity.magnitude > 0)
        {
            if (!playing)
            {
                playing = true;
                audio.Play("Move_" + material);
            }
        }
        else
        {
            playing = false;
            audio.StopPlaying("Move_" + material);
        }
    }
}
