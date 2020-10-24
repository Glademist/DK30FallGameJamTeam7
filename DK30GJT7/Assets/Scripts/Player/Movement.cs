using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;

    public float runSpeed = 10.0f;
    Interaction interaction;

    void Start()
    {
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
    }
}
