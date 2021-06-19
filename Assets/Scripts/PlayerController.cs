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
    void Start()
    {
        beatManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<BeatManager>();
        gridEntity = GetComponent<GridEntity>();
        gridEntity.SetCaged(true);
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
        if (beatManager.GetTimingClass() != TimingClass.INVALID && !actionTakenThisBeat)
        {
            float h_input = Input.GetAxisRaw("Horizontal");
            if (h_input != 0)
            {
                if (prevHorizInputDirection != Mathf.Sign(h_input))
                {
                    DoValidityCheck();
                    prevHorizInputDirection = Mathf.Sign(h_input);
                    MoveRelative(new Vector2(prevHorizInputDirection, 0));
                    actionTakenThisBeat = true;
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
                    DoValidityCheck();
                    prevVertInputDirection = Mathf.Sign(v_input);
                    MoveRelative(new Vector2(0, -prevVertInputDirection));
                    actionTakenThisBeat = true;
                }
            }
            else
            {
                prevVertInputDirection = 0;
            }

            if(Input.GetButtonDown("Fire1"))
            {
                
            }

            if (Input.GetButtonDown("Fire2"))
            {

            }
        }
    }
    void DoValidityCheck ()
    {
        if (actionTakenThisBeat || beatManager.GetTimingClass() == TimingClass.INVALID)
        {
            InvalidInput();
        }
    }
    void InvalidInput()
    {
        print("Whoops");
    }

    void MoveRelative(Vector2 movement)
    {
        gridEntity.MoveRelativeToCurrentPosition(movement);

    }

    void MoveAbsolute()
    {

    }
}
