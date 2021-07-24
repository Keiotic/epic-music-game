using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        OnHealthUpdate();
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
    }

    public void ApplyDamage (int damage)
    {
        health = Mathf.Clamp(health - Mathf.Abs(damage), 0, maxHealth);
        OnHealthUpdate();
    }

    public void Heal(int healValue)
    {
        health = Mathf.Clamp(health + Mathf.Abs(healValue), 0, maxHealth);
        OnHealthUpdate();
    }

    private void OnHealthUpdate()
    {
        if(GetComponent<PlayerController>())
        GameEvents.current.UpdatePlayerHealth(health, maxHealth);
    }
}
