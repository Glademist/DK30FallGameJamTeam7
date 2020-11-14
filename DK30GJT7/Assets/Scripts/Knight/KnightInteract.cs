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

            //chance to stop attacking

        }
        else
        {
            StopAttacking();
        }
    }

    public void StartAttacking(GameObject target)
    {
        attacking = true;
        attackTime = attackSpeed;
        targetHealth = target.GetComponent<Health>();
    }

    public int GetTargetHealth()
    {
        if (targetHealth)
        {
            return targetHealth.currentHealth;
        }
        return 0;
    }

    public void StopAttacking()
    {
        attacking = false;
    }

    public bool IsAttacking()
    {
        return attacking;
    }

    public void EatFood(GameObject food)
    {
        food.GetComponentInChildren<Pickup>().EatFood(knightHealth);
    }

    public bool IsPickup(GameObject pickup)
    {
        if (pickup.GetComponentInChildren<Pickup>())
        {
            return true;
        }
        return false;
    }

    public bool IsFood(GameObject pickup)
    {
        if (IsPickup(pickup))
        {
            if (pickup.GetComponentInChildren<Pickup>().isFood)
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
    }

    public bool IsChest(GameObject pickup)
    {
        if (pickup.GetComponent<Chest>())
        {
            return true;
        }
        return false;
    }

    public void OpenChest(GameObject chest)
    {
        chest.GetComponent<Chest>().OpenChest();
    }
}
