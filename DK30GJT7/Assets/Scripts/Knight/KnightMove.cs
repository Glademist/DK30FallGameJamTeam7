using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class KnightMove : MonoBehaviour
{
    public Vector2 target;
    Camera Cam;
    public Vector2 mousePos;
    public float speed = 0.35f;
    public Rigidbody2D rigid2d;
    Pathfinding pathfinding;
    public List<Vector2Int> pathTarget;
    public List<Room> rooms;
    public Room currentRoom;
    public bool DrawGizmos = false;
    public Interest TargetInterest;

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
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            mousePos = Cam.ScreenToWorldPoint(Input.mousePosition);
            CommandKnight(new Vector2(Mathf.Floor(mousePos.x) + 0.5f, Mathf.Floor(mousePos.y) + 0.5f));
        }
        if (pathTarget.Count > 3)
        {
            Vector2 nextPos = new Vector2(pathTarget[pathTarget.Count - 2].x + 0.5f, pathTarget[pathTarget.Count - 2].y + 0.5f);
            if (Vector2.Distance(this.transform.position, nextPos) >= 0.5f)
            {
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
            if (Vector2.Distance(this.transform.position, target) >= 0.5f)
            {
                rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, target, speed));
            }
            else if (target != new Vector2(this.transform.position.x, this.transform.position.y))
            {
                rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, target, speed));
                target = this.transform.position;
            }
        }
        IsInRoom();
    }

    public bool IsInRoom()
    {
        Vector2Int Origin = new Vector2Int(Mathf.RoundToInt(this.transform.position.x), Mathf.RoundToInt(this.transform.position.y));
        foreach (Room Room in rooms)
        {
            RectInt room = Room.room;
            if (Mathf.Abs(room.center.x - Origin.x) < room.width / 2 && Mathf.Abs(room.center.y - Origin.y) < room.height / 2)
            {
                currentRoom = Room;
                return true;
            }
        }
        currentRoom = null;
        return false;
    }

    public void DrawPath()
    {
        for (int i = 0; i < pathTarget.Count - 1; i++)
        {
            Debug.DrawLine(new Vector2((pathTarget[i].x + 0.5f), (pathTarget[i].y + 0.5f)), new Vector2((pathTarget[i + 1].x + 0.5f), (pathTarget[i + 1].y + 0.5f)), Color.blue);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 1.0f, 0.0f);
        if (DrawGizmos)
        {
            DrawRooms();
        }
    }

    public void DrawRooms()
    {
        
        foreach (Room Room in rooms)
        {
            RectInt room = Room.room;
            Random.seed = (int)room.center.x + (int)room.center.y;
            Gizmos.color = new Color(Random.value, Random.value, Random.value);
            Gizmos.DrawCube(room.center, new Vector3(room.size.x, room.size.y, 10));
        }
    }

    public void CommandKnight(Vector2 Target)
    {
        target = Target;
        pathTarget = pathfinding.LoadAStar(new Vector2Int((int)Mathf.Floor(this.transform.position.x), (int)Mathf.Floor(this.transform.position.y)), new Vector2Int((int)Mathf.Floor(Target.x), (int)Mathf.Floor(Target.y)));
    }
}
