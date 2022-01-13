using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHandler : MonoBehaviour
{
    [System.Serializable] public class ShieldState
    {
        public Vector2 offset;
        public float relativeRotation;
    }
    [SerializeField] private int shieldStateIndex = 0;
    private GameObject shieldHandler;
    [SerializeField] private GameObject shield;
    private Shield[] shields;
    
    // Start is called before the first frame update
    void Start()
    {
        shieldHandler = Instantiate(new GameObject(), transform);
        transform.localPosition = Vector3.zero;
    }
  
    public void InitializeShield (int shieldAmount)
    {
        shields = new Shield[shieldAmount];
    }

    public void AddShield (Vector2 offset, float rotation, GameObject shield, int index)
    {
        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, rotation));
        if(shield.GetComponent<Shield>())
        {
            Shield s = Instantiate(shield, offset, rot).GetComponent<Shield>();
            if (shields[index])
            {
                Destroy(shields[index]);
            }
            shields[index] = s;
            return;
        }
        throw new MissingComponentException("Shield Component Not Assigned");
    }

    public void UpdateShieldPosition (int index, Vector2 position)
    {
        if(shields[index])
        {
            shield.transform.localPosition = position;
        }
    }

    public void UpdateShieldRotation (int index, Quaternion rotation)
    {
        if(shields[index])
        {
            shield.transform.rotation = rotation;
        }
    }

    public void RotateShieldHandler (float rotationChange)
    {
        shieldHandler.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationChange + shieldHandler.transform.rotation.eulerAngles.z));
    }
}
