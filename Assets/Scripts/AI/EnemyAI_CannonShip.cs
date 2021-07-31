using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_CannonShip : EnemyAI
{
    [SerializeField] private int movesUntilWait = 10;
    [SerializeField] private int waitDuration = 3;
    [SerializeField] private ProjectileAttack[] attacks;
    [SerializeField] private bool fireOneByOne = false;
    [SerializeField] private int timeBetweenFirings = 1;
    private int waitTime;
    private int movesTillWait;
    private int fireTime;

    public override void Start()
    {
        base.Start();
    }
    public override void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player, Vector2 spawnPos)
    {
        base.InitializeEnemy(gridManager, beatManager, gameManager, player, spawnPos);
        float interpolationSpeed = (gridManager.GetGridBoxSize()*speed)/beatManager.GetTimeBetweenBeats();
        gridEntity.SetInterpolationSpeed(interpolationSpeed);
    }

    public override void DoTargetlessUpdate()
    {
        DoMovement();
    }

    public override void DoTargetUpdate()
    {
        DoMovement();
    }

    public void DoMovement()
    {
        if (movesTillWait > 0)
        {
            movesTillWait -= 1;
            gridEntity.MoveRelativeToCurrentPosition(transform.forward.normalized * speed);
        }
        else
        {
            if(waitTime > 0)
            {
                waitTime -= 1;
                if(attackType == AttackType.ON_WAIT||attackType == AttackType.UNBOUND)
                {
                    DoFireCheck();
                }
                if(waitTime == 0)
                {
                    movesTillWait = movesUntilWait;
                }
            }

        }
    }

    public void DoFireCheck()
    { 
        
    }

    public override void MovementUpdate(int beat)
    {
        base.MovementUpdate(beat);
    }

    public override void Update()
    {
        base.Update();

    }
}
