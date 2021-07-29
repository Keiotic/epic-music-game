using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_ZigZag : EnemyAI
{
    private Vector2 forwardVector;
    private Vector2 rightVector;
    private int movesUntilDirectionSwitch;
    private int forwardMoveAmount;
    [SerializeField] int firingDelay = 3;
    private int movesUntilFire;
    [SerializeField] private int direction = -1;

    [SerializeField] int leftLimit = 5;
    [SerializeField] int rightLimit = 5;
    [SerializeField] int forwardMoves = 3;
    [SerializeField] ProjectileAttack projectileAttack;

    public override void Start()
    {
        base.Start();
        forwardVector = transform.up.normalized;
        rightVector = transform.right.normalized;
        movesUntilFire = firingDelay;

        //Lock movement to up/down left/right movement
        if (Mathf.Abs(forwardVector.y) == 1)
        {
            rightVector = Vector2.right;
            if (direction > 0)
            {
                movesUntilDirectionSwitch = rightLimit;
            }
            else
            {
                movesUntilDirectionSwitch = leftLimit;
            }
        }
        else
        {
            rightVector = Vector2.up;
            if (direction > 0)
            {
                movesUntilDirectionSwitch = rightLimit;
            }
            else
            {
                movesUntilDirectionSwitch = leftLimit;
            }
        }

    }
    public override void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player, Vector2 spawnPos)
    {
        base.InitializeEnemy(gridManager, beatManager, gameManager, player, spawnPos);
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
        if (movesUntilDirectionSwitch != 0)
        {
            movesUntilDirectionSwitch -= 1;
            gridEntity.MoveRelativeToCurrentPosition(rightVector*direction);
            forwardMoveAmount = forwardMoves;
        }
        else
        {
            forwardMoveAmount -= 1;
            if (forwardMoveAmount <= 0)
            {
                if (direction > 0)
                    movesUntilDirectionSwitch = leftLimit;
                else
                    movesUntilDirectionSwitch = rightLimit;
                direction *= -1;
            }
            gridEntity.MoveRelativeToCurrentPosition(forwardVector);
        }


        if(movesUntilFire == 0)
        {
            movesUntilFire = firingDelay;
            projectileSource.FireProjectileAttack(projectileAttack, gridManager.GetGridSpeedCoefficient());
        }
        else
        {
            movesUntilFire -= 1;
        }
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
