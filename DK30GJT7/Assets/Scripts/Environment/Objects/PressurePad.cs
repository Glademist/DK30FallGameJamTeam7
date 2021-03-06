﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    [SerializeField]
    public Sprite onSprite, offSprite;
    bool pressed = false;
    public int objects = 0;
    SpriteRenderer rend;
    List<ToggledSpikeTrap> traps = new List<ToggledSpikeTrap>();
    public GameObject TrapList;

    private void Start()
    {
        GameObject vfx = transform.GetChild(0).gameObject;
        rend = vfx.GetComponent<SpriteRenderer>();

        foreach (Transform child in TrapList.transform)
        {
            if (child.GetComponent<ToggledSpikeTrap>())
            {
                traps.Add(child.GetComponent<ToggledSpikeTrap>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            objects++;
            checkPressed();
            FindObjectOfType<AudioManager>().Play("Plate_Activated");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Rigidbody2D>())
        {
            objects--;
            checkPressed();
        }
    }

    void checkPressed()
    {
        if(objects > 0 && !pressed)
        {
            pressed = true;
            rend.sprite = onSprite;
            UpdateTraps();
        }
        else if(objects == 0 && pressed)
        {
            pressed = false;
            rend.sprite = offSprite;
            UpdateTraps();
        }
    }

    void UpdateTraps()
    {
        for (int i = 0; i < traps.Count; i++)
        {
            traps[i].Flip(!pressed);
            FindObjectOfType<AudioManager>().Play("Spikes_Down");
        }
    }
}
