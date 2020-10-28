using System.Collections;
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
            if(child.name == "Spike Trap")
            {
                traps.Add(child.GetComponent<ToggledSpikeTrap>());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "PlayerOnlyCollider")
        {
            objects++;
            checkPressed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        objects--;
        checkPressed();
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
        }
    }
}
