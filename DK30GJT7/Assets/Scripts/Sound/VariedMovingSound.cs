using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariedMovingSound : MonoBehaviour
{
    public string name;
    Rigidbody2D body;

    AudioManager audio;
    float stepDelay = 0.4f, currentDelay = 0f;
    List<string> sounds;

    EnemyBehaviour enemy;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();

        audio = FindObjectOfType<AudioManager>();
        sounds = AudioMixing.GetMovementSounds(name);
        enemy = GetComponent<EnemyBehaviour>(); //dont use rigidbody since enemy does use it to move
    }

    private void Update()
    {
        if (!enemy)
        {
            return;
        }
        if(enemy.speed > 0)
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
