using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    ProgressBar healthBar;
    public int maxHealth;
    public int currentHealth;
    public float healthbarHeight;

    EnemyBehaviour enemy;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
        healthBar = new ProgressBar(gameObject, new Vector3(100, 15, 1), new Vector3(90, 12, 1), new Vector2(0, healthbarHeight), Color.black, Color.green, true);
        enemy = GetComponent<EnemyBehaviour>();

        if(name == "Knight" && UpdateKnightHealth.knightHealth != 0)
        {
            currentHealth = UpdateKnightHealth.knightHealth;
            UpdateHealthbar();
        }
    }

    private void Update()
    {
        /*
        if (Input.GetMouseButtonDown(0))
        {
            TakeDamage(1);
        }
        */
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log(string.Format("Took {0} damage, current health is {1}/{2}", damage, currentHealth, maxHealth));
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            if(gameObject.name != "Player" && gameObject.name != "knight")
            {
                Destroy(gameObject);
            }
        }
        if (enemy)
        {
            enemy.WakeUp();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
    }

    public void UpdateHealthbar()
    {
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
    }

    public ProgressBar GetHealthbar()
    {
        return this.healthBar;
    }

    public bool HealthLow()
    {
        return ((float)currentHealth / (float)maxHealth) < 0.4;
    }

    public void HideHealth()
    {
        healthBar.ToggleVisible(false);
    }
}
