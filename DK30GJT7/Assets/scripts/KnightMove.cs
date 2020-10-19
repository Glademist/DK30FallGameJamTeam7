using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    public Vector2 target = new Vector2(0, 0);
    Camera Cam;
    public Vector2 mousePos;
    public float speed = 5;
    public Rigidbody2D rigid2d;
    // Start is called before the first frame update
    void Start()
    {
        Cam = Camera.main;
        rigid2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float Step = speed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
            CommandKnight(mousePos);
        }
        if (Vector2.Distance(this.transform.position, target) >= 0.5 && target != new Vector2(this.transform.position.x, this.transform.position.y))
        {
            rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, target, Step));
        }
        else
        {
            target = this.transform.position;
        }
    }

    public void CommandKnight(Vector2 Target)
    {
        target = Target;
    }
}
