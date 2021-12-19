using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Jumper: EnemyAI
{
    private Vector2 forwardVector;
    private Vector2 rightVector;
    private int movementSize;
    private int movesUntilDirectionSwitch;
    [SerializeField] int firingDelay = 3;
    private int movesUntilFire;
    private int direction = -1;

    [SerializeField] ProjectileAttack projectileAttack;

    public override void Start()
    {
        base.Start();
        forwardVector = transform.up.normalized;
        rightVector = transform.right.normalized;
        movesUntilFire = firingDelay;
    }
    public override void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player, Vector2 spawnPos, Vector2[] pathingArguments)
    {
        base.InitializeEnemy(gridManager, beatManager, gameManager, player, spawnPos, pathingArguments);
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
            gridEntity.MoveRelativeToCurrentPosition(rightVector * direction);
        }
        else
        {
            gridEntity.MoveRelativeToCurrentPosition(forwardVector);
            movesUntilDirectionSwitch = movementSize;
            direction *= -1;
        }
    }

    public void DoAttackCheck()
    {
        if (movesUntilFire == 0)
        {
            movesUntilFire = firingDelay;
            projectileSource.FireProjectileAttack(projectileAttack);
        }
        else
        {
            movesUntilFire -= 1;
            if (movesUntilFire == 0)
            {
                TelegraphAttack();
            }
        }
    }

    public override void TelegraphAttack()
    {
        throw new System.NotImplementedException();
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
