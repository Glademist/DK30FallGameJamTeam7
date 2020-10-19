using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMove : MonoBehaviour
{
    public Vector2 target;
    Camera Cam;
    public Vector2 mousePos;
    public float speed = 0.5f;
    public Rigidbody2D rigid2d;
    public Pathfinding pathfinding;
    public List<Vector2Int> pathTarget;
    // Start is called before the first frame update
    void Start()
    {
        pathfinding = this.GetComponent<Pathfinding>();
        Cam = Camera.main;
        rigid2d = this.GetComponent<Rigidbody2D>();
        target = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
            CommandKnight(new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f));
        }
        if (pathTarget.Count > 3)
        {
            Vector2 nextPos = new Vector2(pathTarget[pathTarget.Count - 2].x + 0.5f, pathTarget[pathTarget.Count - 2].y + 0.5f);
            if (Vector2.Distance(this.transform.position, nextPos) >= 0.5f)
            {
                Debug.Log(nextPos);
                rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, nextPos, speed));
                DrawPath();
            }
            else
            {
                pathTarget = pathfinding.LoadAStar(new Vector2Int((int)Mathf.Floor(this.transform.position.x), (int)Mathf.Floor(this.transform.position.y)), new Vector2Int((int)Mathf.Floor(pathTarget[0].x), (int)Mathf.Floor(pathTarget[0].y)));
            }
        }
        else
        {
            if (Vector2.Distance(this.transform.position, target) >= 0.001f)
            {
                rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, target, speed));
            }
            else if (target != new Vector2(this.transform.position.x, this.transform.position.y))
            {
                rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, target, speed));
                target = this.transform.position;
            }
        }
    }

    public void DrawPath()
    {
        for (int i = 0; i < pathTarget.Count - 1; i++)
        {
            Debug.DrawLine(new Vector2((pathTarget[i].x + 0.5f), (pathTarget[i].y + 0.5f)), new Vector2((pathTarget[i + 1].x + 0.5f), (pathTarget[i + 1].y + 0.5f)), Color.blue);
        }
    }
    public void CommandKnight(Vector2 Target)
    {
        //pathfinding.Target = new Vector2Int((int)Target.x, (int)Target.y);
        target = Target;
        pathTarget = pathfinding.LoadAStar(new Vector2Int((int)Mathf.Floor(this.transform.position.x), (int)Mathf.Floor(this.transform.position.y)), new Vector2Int((int)Mathf.Floor(Target.x), (int)Mathf.Floor(Target.y)));
        Debug.Log(new Vector2Int((int)Mathf.Floor(Target.x), (int)Mathf.Floor(Target.y)));
    }
}
