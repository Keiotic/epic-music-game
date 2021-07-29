using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health;
    [SerializeField] private int destructionScore = 10;
    private bool dead = false;
    private bool isPlayer = false;


    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<PlayerController>())
        {
            isPlayer = true;
        }
        health = maxHealth;
        OnHealthUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0 && !dead)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isPlayer)
        {
            GameEvents.current.DestroyPlayer();
        }
        else
        {
            GameEvents.current.DestroyEnemy(destructionScore);
        }
        Destroy(gameObject);
    }

    public void ApplyDamage(int damage)
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
        if (isPlayer)
            GameEvents.current.UpdatePlayerHealth(health, maxHealth);
    }
}
