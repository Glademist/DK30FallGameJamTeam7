using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    //public gameObject target;
    public float secondsToReprioritise;
    public float secondsSinceLastReprioritise;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void FixedUpdate() {
        secondsSinceLastReprioritise += Time.fixedDeltaTime;
        if (secondsSinceLastReprioritise >= secondsToReprioritise){
            return;//KnightMove.CommandKnight(layer);
        }
    }
}
