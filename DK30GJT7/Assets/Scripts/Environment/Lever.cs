using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public bool lever = true;
    [SerializeField]
    public ToggledSpikeTrap[] traps;
    
    public void FlipLever()
    {
        lever = !lever;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        for(int i = 0; i < traps.Length; i++)
        {
            traps[i].Flip(lever);
        }
    }
}
