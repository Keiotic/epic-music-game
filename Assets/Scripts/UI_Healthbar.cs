using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Healthbar : UI_Healthrenderer
{
    [Header("HealthBar")]
    [SerializeField] protected Sprite healthBarSprite;
    [SerializeField] protected Image healthBar;
    protected float widthUnitsPerHealth;

    [Header("Healthunits")]
    [SerializeField] protected Sprite[] StateRenders;
    [SerializeField] protected int healthPerUnit;
    [SerializeField] protected float padding;
    [SerializeField] protected int rows = 1;
    public override void Initialize(float health, float maxHealth)
    {
        throw new System.NotImplementedException();
    }
    public override void UpdateHealth(float health, float maxHealth)
    {

    }
    public override void Start()
    {
        
    }
    public override void Update()
    {
        
    }
}



