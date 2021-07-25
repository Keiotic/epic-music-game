using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Invader : EnemyAI
{
    private Vector2 forwardVector;
    private Vector2 rightVector;
    private int movementSize;
    private int movesUntilDirectionSwitch;
    [SerializeField] int firingDelay = 3;
    private int movesUntilFire;
    private int direction;

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
            movementSize = (int)gridManager.GetInnerGridSize().x-1;
            direction = -(int)Mathf.Sign(gridEntity.GetPosition().x - gridManager.GetGridSize().x/2);
            int gridPos = (int)(gridEntity.GetPosition().x - gridManager.GetGridOffset().x);
            if (direction > 0)
            {
                movesUntilDirectionSwitch = Mathf.RoundToInt(gridManager.GetInnerGridSize().x - (gridPos + 1));
            }
            else
            {
                movesUntilDirectionSwitch = Mathf.RoundToInt(gridPos);
            }
        }
        else
        {
            print(forwardVector);
            rightVector = Vector2.up;
            movementSize = (int)gridManager.GetInnerGridSize().y-1;
            direction = -(int)Mathf.Sign(gridEntity.GetPosition().y - gridManager.GetGridSize().y / 2);
            int gridPos = (int)(gridEntity.GetPosition().y - gridManager.GetGridOffset().y);
            if (direction > 0)
            {
                movesUntilDirectionSwitch = Mathf.RoundToInt(gridManager.GetInnerGridSize().y - (gridPos + 1));
            }
            else
            {
                movesUntilDirectionSwitch = Mathf.RoundToInt(gridPos);
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
        }
        else
        {
            gridEntity.MoveRelativeToCurrentPosition(forwardVector);
            movesUntilDirectionSwitch = movementSize;
            direction *= -1;
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
