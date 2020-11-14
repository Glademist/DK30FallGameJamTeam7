using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnAwake : MonoBehaviour
{
    public string sound_name;

    private void Awake()
    {
        FindObjectOfType<AudioManager>().Play(sound_name);
    }
}
