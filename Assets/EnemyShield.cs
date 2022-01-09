using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShield : MonoBehaviour
{
    [System.Serializable] public class ShieldState
    {
        public Vector2 offset;
        public float relativeRotation;
    }
    [SerializeField] private int shieldStateIndex = 0;
    private GameObject shieldHandler;
    [SerializeField] private GameObject shield;
    [SerializeField] private ShieldState[] shieldstates;
    private Shield[] shields;

    // Start is called before the first frame update
    void Start()
    {
        if(!shield)
        {
            throw new UnassignedReferenceException("Shield Prefab Not Assigned");
        }
        else
        {
            for(int i = 0; i < shieldstates.Length; i++)
            {
                ShieldState state = shieldstates[i];
                Vector2 v = transform.position + transform.up*state.offset.y + transform.right*state.offset.x;
                Vector3 localRotation = transform.rotation.eulerAngles;
                Quaternion rotation = Quaternion.Euler(0, 0, localRotation.z + state.relativeRotation);
                Instantiate(shield, v, rotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
