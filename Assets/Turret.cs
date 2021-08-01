using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileSource))]
public class Turret : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90;
    [SerializeField] private ProjectileAttack attack;
    [SerializeField] private ProjectileSource projectileSource;
    private AudioSource source;
    private void Start()
    {
        projectileSource = GetComponent<ProjectileSource>();
        source = gameObject.AddComponent<AudioSource>();
    }
    public void RotateTowardsPosition(Vector2 targetPos)
    {
        Vector2 delta = ((Vector2)targetPos - (Vector2)transform.position).normalized;
        float angleToTarget = Mathf.Atan2(delta.x, delta.y) * -Mathf.Rad2Deg + 90;
        float angleRotation = angleToTarget * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(angleRotation, transform.forward);
    }

    public void RotateTowardsAngle(float angle)
    {
        Vector3 euler = transform.rotation.eulerAngles;
        if(transform.parent)
        {
            euler.z = transform.parent.rotation.eulerAngles.z + angle;
        }
        Quaternion targetRotation = Quaternion.Euler(euler);
        Quaternion interpolatedRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        print(angle + ":" + interpolatedRotation.eulerAngles.z);
        transform.rotation = interpolatedRotation;
    }

    public void Telegraph()
    {

    }

    public void Attack()
    {
        projectileSource.FireProjectileAttack(attack);
    }
}
