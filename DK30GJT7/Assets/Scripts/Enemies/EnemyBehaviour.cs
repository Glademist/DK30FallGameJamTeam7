using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    float aggroRange = 20f, attackRange = 4f, moveSpeed = 1f;
    [SerializeField]
    GameObject currentTarget;
    public List<Collider2D> Collisions;
    Vector3 homePosition;
    CircleCollider2D sightRange;

    [SerializeField]
    bool sleeping = false, hiding = false;
    ProgressBar healthbar;
    [SerializeField]
    float waitTime = 5f;

    //combat
    [SerializeField]
    float attackSpeed = 1f, attackTime = 0f;
    [SerializeField]
    int damage = 1;
    Health targetHealth;

    enum State {Sleeping, Wandering, Seeking, Attacking, Returning}
    [SerializeField]
    State currentState;
    [SerializeField]
    LayerMask walls;

    Animator anim;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.Wandering;
        healthbar = GetComponent<Health>().GetHealthbar();
        if (sleeping)
        {
            currentState = State.Sleeping;
            healthbar.ToggleVisible(false);
        }
        
        homePosition = transform.position;
        sightRange = GetComponentInChildren<CircleCollider2D>();

        GameObject vfx = transform.GetChild(0).gameObject;
        anim = vfx.GetComponent<Animator>();
        rend = vfx.GetComponent<SpriteRenderer>();
        if (hiding)
        {
            healthbar.ToggleVisible(false);
            rend.enabled = false;
        }
    }

    private void Update()
    {
        if(currentState == State.Attacking)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                AttackTarget();
            }

            if (Vector2.Distance(currentTarget.transform.position, transform.position) > attackRange)
            {
                currentState = State.Seeking;
                //chase target to get back into attack range
            }
        }       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sleeping || anim.GetCurrentAnimatorStateInfo(0).IsName("Imp_Appear2"))   //wait for imp appear anim to finish
        {
            return;
        }

        switch (currentState)
        {
            case State.Wandering:
                //while wandering enemy will path randomly in a radius

                //if(Health.recentlyTakenDamage)    add some damage taken boolean which determines if damage was taken recently
                //{
                //path back away from trap, use previously taken path if possible
                //}
                Wander();
                
                break;
            case State.Seeking:

                Seek();

                break;
            case State.Attacking:

                attackTime -= Time.deltaTime;
                if(attackTime <= 0)
                {
                    AttackTarget();
                }

                if (Vector2.Distance(currentTarget.transform.position, transform.position) > attackRange)
                {
                    currentState = State.Seeking;
                    //chase target to get back into attack range
                }
                break;
            case State.Returning:
                transform.position += (homePosition - transform.position) * moveSpeed * Time.deltaTime;

                if (Vector2.Distance(transform.position, homePosition) < 1f)
                {
                    currentState = State.Wandering;
                }
                break;
        }
    }

    void FindTarget()
    {
        foreach (Collider2D Collider in Collisions)
        {
            if (Collider.gameObject.name == "Player" || Collider.gameObject.name == "Knight")
            {
                if (Vector2.Distance(this.transform.position, Collider.gameObject.transform.position) < aggroRange && Collider.gameObject != this.gameObject
                    && !Physics2D.Linecast((Vector2)transform.position, (Vector2)Collider.gameObject.transform.position, walls))
                {
                    if (hiding)
                    {
                        anim.SetTrigger("Appear");
                        
                        hiding = false;
                        rend.enabled = true;
                    }
                    else
                    {
                        anim.SetBool("Running", true);
                    }
                    currentTarget = Collider.gameObject;
                    currentState = State.Seeking;
                }
            }
        }
    }

    void Seek()
    {
        transform.position += (currentTarget.transform.position - transform.position) * moveSpeed * Time.deltaTime; //path towards current target

        //if enemy leaves aggro range or sightline blocked return to home
        if (Vector2.Distance(transform.position, homePosition) > aggroRange
            || Physics2D.Linecast((Vector2)transform.position, (Vector2)currentTarget.transform.position, walls)
            || Physics2D.Linecast((Vector2)transform.position, (Vector2)homePosition, walls))
        {
            currentState = State.Returning;
            anim.SetBool("Running", false);
        }

        else if (Vector2.Distance(currentTarget.transform.position, transform.position) < attackRange)  //if target in attack range start attacking
        {
            StartAttacking();
        }
    }

    void Wander()
    {
        ContactFilter2D Filter = new ContactFilter2D();
        Filter = Filter.NoFilter();
        sightRange.OverlapCollider(Filter, Collisions);
        if (Collisions.Count > 1)
        {
            FindTarget();
        }
    }

    void StartAttacking()
    {
        currentState = State.Attacking;
        attackTime = attackSpeed;
        targetHealth = currentTarget.GetComponent<Health>();
    }

    void AttackTarget()
    {
        if (targetHealth)
        {
            targetHealth.TakeDamage(damage);
            attackTime = attackSpeed;
        }
    }

    public void WakeUp()    //wake up a sleeping enemy
    {
        if (sleeping)
        {
            anim.SetBool("Running", true);
            currentState = State.Wandering;
            sleeping = false;
            healthbar.ToggleVisible(true);
        }
    }

    public void WakeUp(GameObject victim)    //wake up a sleeping enemy
    {
        if (sleeping)
        {
            WakeUp();
            currentTarget = victim;
            StartAttacking();
            AttackTarget();
        }
    }
}
