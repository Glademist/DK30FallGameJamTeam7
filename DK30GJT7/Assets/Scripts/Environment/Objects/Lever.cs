using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool lever = true;
    List<ToggledSpikeTrap> traps = new List<ToggledSpikeTrap>();
    public GameObject TrapList;

    private void Start()
    {
        foreach (Transform child in TrapList.transform)
        {
            if (child.GetComponent<ToggledSpikeTrap>())
            {
                traps.Add(child.GetComponent<ToggledSpikeTrap>());
            }
        }
    }

    public void FlipLever()
    {
        lever = !lever;
        FindObjectOfType<AudioManager>().Play("Lever");
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        for(int i = 0; i < traps.Count; i++)
        {
            traps[i].Flip(lever);
        }
    }
}
