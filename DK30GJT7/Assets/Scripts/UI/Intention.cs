using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intention : MonoBehaviour
{
    public float timeToShowCurrent;
    private float lifeTimer;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 0;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate() {
        lifeTimer += Time.fixedDeltaTime;
        if(lifeTimer >= timeToShowCurrent){
            gameObject.SetActive(false);
        }
    }

    public void ShowIntention(string intention_type, float timeToDisplay){
        gameObject.SetActive(true);
        lifeTimer = timeToDisplay;
    }
}
