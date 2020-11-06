using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    SpriteRenderer light;
    GameObject objects;
    bool visible = false;
    void Start()
    {
        light = GetComponent<SpriteRenderer>();
        light.color = new Color(0f, 0f, 0f, 0.5f);
        objects = transform.GetChild(0).gameObject;
    }

    public void ChangeVisibility(bool isVisible)
    {
        Debug.Log("changing visible to " + isVisible);
        visible = isVisible;
        objects.SetActive(visible);

        if (visible)
        {
            light.color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            light.color = new Color(0f, 0f, 0f, 0.5f);
        }
    }
}
