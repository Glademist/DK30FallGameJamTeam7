using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    ProgressBar healthBar;
    private int maxHealth = 10, currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar = new ProgressBar(gameObject, new Vector3(100, 15, 1), new Vector3(90, 12, 1), new Vector2(0, 0.5f), Color.black, Color.green, true);
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     TakeDamage(1);
        // }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(string.Format("Took {0} damage, current health is {1}/{2}", damage, currentHealth, maxHealth));
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Debug.Log("I have died");
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        healthBar.UpdateHealthbar(currentHealth, maxHealth);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
