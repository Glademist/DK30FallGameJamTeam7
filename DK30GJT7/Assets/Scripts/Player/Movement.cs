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

    void Start()
    {
        GlobalReferences.Player = gameObject;
        body = GetComponent<Rigidbody2D>();
        interaction = GetComponent<Interaction>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        if(body.velocity.x != 0 || body.velocity.y != 0)
        {
            interaction.CancelInteration();
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
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
