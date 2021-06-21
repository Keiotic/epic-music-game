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

    public bool Is2D = true;

    public void InitializeProjectile (float speed, int damage, LayerMask mask)
    {
        this.speed = speed;
        this.damage = damage;
        this.mask = mask;
        lifeTime = 5;
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
        
        if(Is2D)
        {
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, rayRange, mask);
            if(hit2D)
            {
                RegisterHit(hit2D.collider.gameObject);
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(new Ray(transform.position, transform.forward), out hit, rayRange, mask))
            {
                RegisterHit(hit.collider.gameObject);
            }
        }

    }

    public void RegisterHit(GameObject hit)
    {
        hit.GetComponent<HealthManager>().ApplyDamage(damage);
        Destroy(this.gameObject);
    }
}
