using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform targetObject;

    float smoothing = 0.1f;
    float startingZ;

    private void Awake()
    {
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, targetObject.position, smoothing);
        transform.position = new Vector3(transform.position.x, transform.position.y, startingZ); //camera sets z to 0 after lerp
    }
}
