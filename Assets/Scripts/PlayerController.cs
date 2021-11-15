using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float prevHorizInputDirection = 0;
    private float prevVertInputDirection = 0;
    private bool actionTakenThisBeat;
    private int currentBeat;
    private GridEntity gridEntity;
    private BeatManager beatManager;
    private ProjectileSource projectileSource;
    private GridManager gridManager;
    [SerializeField] private ProjectileAttack primaryAttack;

    void Start()
    {
        beatManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BeatManager>();
        gridManager = beatManager.GetComponent<GridManager>();
        gridEntity = GetComponent<GridEntity>();
        gridEntity.SetCaged(true);
        projectileSource = GetComponent<ProjectileSource>();
    }

    void Update()
    {
        if (currentBeat != beatManager.GetCurrentBeat())
        {
            actionTakenThisBeat = false;
            currentBeat = beatManager.GetCurrentBeat();
        }
        CheckInputs();
        gridEntity.LinearilyInterpolatePosition();
    }

    void CheckInputs()
    {
        float h_input = Input.GetAxisRaw("Horizontal");
        if (h_input != 0)
        {
            if (prevHorizInputDirection != Mathf.Sign(h_input))
            {
                if (ActionIsValid())
                {
                    prevHorizInputDirection = Mathf.Sign(h_input);
                    MoveRelative(new Vector2(prevHorizInputDirection, 0));
                    actionTakenThisBeat = true;
                }
            }
        }
        else
        {
            prevHorizInputDirection = 0;
        }

        float v_input = Input.GetAxisRaw("Vertical");
        if (v_input != 0)
        {
            if (prevVertInputDirection != Mathf.Sign(v_input))
            {
                if (ActionIsValid())
                {
                    prevVertInputDirection = Mathf.Sign(v_input);
                    MoveRelative(new Vector2(0, prevVertInputDirection));
                    actionTakenThisBeat = true;
                }
            }
        }
        else
        {
            prevVertInputDirection = 0;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if(ActionIsValid())
            FireWeapon();
            actionTakenThisBeat = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (ActionIsValid())
                actionTakenThisBeat = true;
        }
    }
    bool ActionIsValid()
    {
        if (actionTakenThisBeat || beatManager.GetTimingClass() == TimingClass.INVALID)
        {
            InvalidInput();
            return false;
        }
        return true;
    }
    void InvalidInput()
    {
        
    }

    void MoveRelative(Vector2 movement)
    {
        gridEntity.MoveRelativeToCurrentPosition(movement);

    }

    void MoveAbsolute()
    {

    }

    public void FireWeapon()
    {
        projectileSource.FireProjectileAttack(primaryAttack);
        projectileSource.PlayFiringSound(primaryAttack.audio, primaryAttack.volume, primaryAttack.pitch, primaryAttack.pitchRange);
    }
}
