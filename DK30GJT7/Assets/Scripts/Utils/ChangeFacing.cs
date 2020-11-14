using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFacing : MonoBehaviour
{
    //facing direction
    GameObject vfx;
    Vector2 rightFacing, leftFacing;

    Vector3 lastPos = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        vfx = transform.GetChild(0).gameObject;

        rightFacing = new Vector2(vfx.transform.localScale.x, vfx.transform.localScale.y);
        leftFacing = new Vector2(vfx.transform.localScale.x * -1, vfx.transform.localScale.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (transform.position.x >= lastPos.x && (transform.position.x - lastPos.x) > 0.001)
        {
            vfx.transform.localScale = rightFacing;
        }
        else
        {
            vfx.transform.localScale = leftFacing;
        }
        lastPos = transform.position;
    }
}
