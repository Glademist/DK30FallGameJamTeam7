using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariedMovingSoundRigidBody : MonoBehaviour
{
    public string name;
    Rigidbody2D body;

    AudioManager audio;
    float stepDelay = 0.2f, currentDelay = 0f;
    List<string> sounds;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        audio = FindObjectOfType<AudioManager>();
        sounds = AudioMixing.GetMovementSounds(name);
    }

    private void Update()
    {
        if(body.velocity.magnitude > 0.1)
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
