using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSound : MonoBehaviour
{
    public string material;
    public bool playing = false;
    AudioManager audio;

    Vector3 lastPos;
    public float velocity = 0f;

    private void Start()
    {
        lastPos = transform.position;
        audio = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate()
    {
        velocity = (transform.position - lastPos).magnitude;
        lastPos = transform.position;

        if(velocity > 0.01f)
        {
            if (!playing)
            {
                playing = true;
                audio.Play("Move_" + material);
            }
        }
        else
        {
            if (playing)
            {
                playing = false;
                audio.StopPlaying("Move_" + material);
            }
        }
    }
}
