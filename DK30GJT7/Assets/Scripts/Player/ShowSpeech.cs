using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSpeech : MonoBehaviour
{

    TextMeshPro speech;
    float timeToShow = 3f, timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        speech = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                speech.enabled = false;
            }
        }
    }

    public void Display()
    {
        speech.enabled = true;
        timer = timeToShow;
    }
}
