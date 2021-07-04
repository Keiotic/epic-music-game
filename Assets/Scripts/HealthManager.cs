using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;   
    }

    // Update is called once per frame
    void Update()
    {
        if(health == 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        if(GetComponent<PlayerController>())
        {

        }
        else
        {
            
        }
        Destroy(gameObject);
    }

    public void ApplyDamage (int damage)
    {
        health = Mathf.Clamp(health - Mathf.Abs(damage), 0, maxHealth);
    }

    public void Heal(int healValue)
    {
        health = Mathf.Clamp(health + Mathf.Abs(healValue), 0, maxHealth);
    }
}
