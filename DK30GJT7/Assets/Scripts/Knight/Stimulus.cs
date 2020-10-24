using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stimulus
{
    public GameObject gameobject;
    public Vector2 position;
    public string type;
    public float timeSeen;

    public Stimulus(GameObject stimulus_go, Vector2 stimulus_pos, string stimulus_type){
        gameobject = stimulus_go;
        position = stimulus_pos;
        type =  stimulus_type;
        timeSeen = Time.time;
    }
}