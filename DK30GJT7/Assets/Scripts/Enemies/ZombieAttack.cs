using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ZombieAttack : MonoBehaviour
{
    public List<Collider2D> Collisions;
    public GameObject Target;
    public float Distance = 20;
    public bool CanAttack;
    public float AttackTimer = 0;
    public float AttackTime = 0.1f;
    public float AttackCooldown = 1;
    public bool Attacking = false;
    public Vector2 AttackOrigin;
    public float speed = 0.5f;
    public Rigidbody2D rigid2d;
    public Tilemap Walls;
    // Start is called before the first frame update
    void Start()
    {
        rigid2d = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ContactFilter2D Filter = new ContactFilter2D();
        Filter = Filter.NoFilter();
        this.GetComponent<CircleCollider2D>().OverlapCollider(Filter, Collisions);
        if (Collisions.Count > 1)
        {
            if (!Attacking)
            {
                FindTarget();
            }
        }
        else
        {
            Target = null;
        }

        if (CanAttack && Target != null)
        {
            AttackTimer = AttackCooldown;
            Attacking = true;
            AttackOrigin = this.transform.position;
            CanAttack = false;
        }
        else if (!CanAttack)
        {
            AttackTimer -= Time.deltaTime;
            if (AttackTimer <= 0)
            {
                CanAttack = true;
            }
        }
        if (Attacking)
        {
            Attack(Target);
        }
    }

    public void FindTarget()
    {
        Target = null;
        if (Collisions.Count > 1)
        {
            foreach (Collider2D Collider in Collisions)
            {
                if (Vector2.Distance(this.transform.position, Collider.gameObject.transform.position) < Distance && Collider.gameObject != this.gameObject && Collider.gameObject != Walls.gameObject)
                {
                    Target = Collider.gameObject;
                }
            }
        }
    }

    public void Attack(GameObject target)
    {
        if (AttackTimer > AttackCooldown - AttackTime / 2)
        {
            rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, Target.transform.position, speed));
        }

        else
        {
            rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, AttackOrigin, speed));
        }

        if (AttackTimer <= AttackCooldown - AttackTime)
        {
            Attacking = false;
            rigid2d.MovePosition(Vector2.MoveTowards(this.transform.position, AttackOrigin, speed));
        }
    }
}