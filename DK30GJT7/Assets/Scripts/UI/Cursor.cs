using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public float timeToLive;
    private float lifeTimer;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        lifeTimer += Time.fixedDeltaTime;
        if(lifeTimer >= timeToLive){
            Destroy(gameObject);
        }
    }
}
