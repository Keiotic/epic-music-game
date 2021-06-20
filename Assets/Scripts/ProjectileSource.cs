using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSource : MonoBehaviour
{
    public bool customized;
    public Customizations customizations;
    [System.Serializable]
    public class Customizations
    {
        public bool positionIsAbsolute = false;
        public Vector2 position;
        public bool rotationIsAbsolute = false;
        public float rotation;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject FireProjectile (GameObject projectile, Vector3 origin, float rotation, int damage, float speed, LayerMask projectileMask)
    {
        Vector3 rot = Vector3.zero;
        rot.z = rotation;
        return FireProjectile(projectile, origin, rot, damage, speed, projectileMask);
    }

    public GameObject FireProjectile (GameObject projectile, Vector3 origin, Vector3 rotation, int damage, float speed, LayerMask projectileMask)
    {
        GameObject proj = Instantiate(projectile, origin, Quaternion.Euler(rotation));
        if (proj.GetComponent<Projectile>())
        {
            Projectile pr = proj.GetComponent<Projectile>();
            pr.InitializeProjectile(speed, damage, projectileMask);
            return proj;
        }
        else
        {
            Destroy(proj);
            throw new MissingComponentException("The projectile: " + proj.name + " is missing a Projectile component!");
        }
    }
}
