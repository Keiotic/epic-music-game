using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UI_Healthrenderer : MonoBehaviour
{
    [SerializeField] protected Sprite backgroundSprite;
    [SerializeField] protected Sprite foregroundSprite;
 

    [SerializeField] protected Image background;
    [SerializeField] protected Image foreground;

    public virtual void UpdateHealth(float health, float maxHealth)
    {

    }
    public abstract void Initialize(float health, float maxHealth);
    public abstract void Start();
    public abstract void Update();

}



