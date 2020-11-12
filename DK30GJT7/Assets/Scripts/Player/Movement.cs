using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    bool facingRight;

    public float runSpeed = 10.0f;
    Interaction interaction;
    GameObject vfx;

    AudioManager audio;
    float stepDelay = 0.2f, currentDelay = 0f;

    void Start()
    {
        GlobalReferences.Player = gameObject;
        body = GetComponent<Rigidbody2D>();
        interaction = GetComponent<Interaction>();
        vfx = transform.GetChild(0).gameObject;

        audio = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(currentDelay > 0)
        {
            currentDelay -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if(body.velocity.x != 0 || body.velocity.y != 0)    //moving
        {
            interaction.CancelInteration();
            if (currentDelay <= 0)
            {
                int index = Random.Range(0, 3);
                audio.Play("Squire_Move" + index);
                currentDelay = stepDelay;
            }
        }
        ChangeFacingDirection();

    }

    

    private void ChangeFacingDirection(){
        if(body.velocity.x != 0){
            if(body.velocity.x < 0){
                if(!facingRight){
                    facingRight = true;
                    FlipSprite();
                }
            }
            else{
                if(facingRight){
                    facingRight = false;
                    FlipSprite();
                }
            }
        }
    }

    private void FlipSprite(){
        Vector3 theScale = vfx.transform.localScale;
        theScale.x *= -1;
        vfx.transform.localScale = theScale;
    }
}
