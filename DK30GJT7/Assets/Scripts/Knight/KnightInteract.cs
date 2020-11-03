using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightInteract : MonoBehaviour
{
    //combat
    [SerializeField]
    float attackSpeed = 1f, attackTime = 0f;
    [SerializeField]
    int damage = 1;
    bool attacking = false;
    Health targetHealth, knightHealth;



    // Start is called before the first frame update
    void Start()
    {
        knightHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            attackTime -= Time.deltaTime;
            if (attackTime <= 0)
            {
                AttackTarget();
            }
        }
    }

    void AttackTarget()
    {
        if (targetHealth)
        {
            targetHealth.TakeDamage(damage);
            attackTime = attackSpeed;
        }
    }

    public void StartAttacking(GameObject target)
    {
        attacking = true;
        attackTime = attackSpeed;
        targetHealth = target.GetComponent<Health>();
    }

    public void StopAttacking()
    {
        attacking = false;
    }

    public void EatFood(Pickup food)
    {
        food.EatFood(knightHealth);
    }

    public bool IsPickup(GameObject pickup)
    {
        if (pickup.GetComponent<Pickup>())
        {
            return true;
        }
        return false;
    }

    public bool IsFood(GameObject pickup)
    {
        if (IsPickup(pickup))
        {
            if (pickup.GetComponent<Pickup>().isFood)
            {
                return true;
            }
        }
        return false;
    }

    public bool IsGold(GameObject pickup)
    {
        if (pickup.GetComponent<CollectGold>())
        {
            return true;
        }
        return false;
    }

    public void CollectGold(GameObject gold)
    {
        gold.GetComponent<CollectGold>().LootGold();
        //reduce greed stat
    }
}
