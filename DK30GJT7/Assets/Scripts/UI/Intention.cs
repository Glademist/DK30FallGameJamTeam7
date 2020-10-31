using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intention : MonoBehaviour
{
    public float timeToShowCurrent;
    private float lifeTimer;
    public SpriteRenderer mySprite;
    public Sprite sayExclamation;
    public Sprite thinkExclamation;
    public Sprite thinkPointer;
    public Sprite placeHolder;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = 0;
        mySprite = gameObject.GetComponent<SpriteRenderer>();
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
        if(intention_type == "player_call"){
            mySprite.sprite = sayExclamation;
        }
        else if(intention_type == "go_to_player_call"){
            mySprite.sprite = thinkExclamation;
        }
        else if(intention_type == "go_to_player_pointer"){
            mySprite.sprite = thinkPointer;
        }
        else{
            mySprite.sprite = placeHolder;
        }
        lifeTimer = 0;
        timeToShowCurrent = timeToDisplay;
    }
}
