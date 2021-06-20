using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public LayerMask mask;
    private float rayRange = 0.5f;
    private float lifeTime = 5;

    public void InitializeProjectile (float speed, int damage, LayerMask mask)
    {
        this.speed = speed;
        this.damage = damage;
        this.mask = mask;
        this.lifeTime = 5;
    }

    public void InitializeProjectile(float speed, int damage, LayerMask mask, float lifeTime)
    {
        this.speed = speed;
        this.damage = damage;
        this.mask = mask;
        this.lifeTime = lifeTime;
    }

    void Start()
    {
        
    }
    void Update()
    {
        transform.Translate(transform.up * Time.deltaTime * speed);
    }
}
