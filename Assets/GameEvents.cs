using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int, int> onUpdatePlayerHealth;
    public void UpdatePlayerHealth (int health, int maxHealth)
    {
        if(onUpdatePlayerHealth != null)
        {
            onUpdatePlayerHealth(health, maxHealth);
        }
    }
}
