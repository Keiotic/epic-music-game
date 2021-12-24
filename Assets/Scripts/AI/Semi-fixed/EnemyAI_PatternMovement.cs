using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_PatternMovement : EnemyAI
{

    [SerializeField] private int stepsUntilFire = 2;
    private int stepsUntilFire_;
    [SerializeField] private int pauseStepsBeforeFire = 1;
    [SerializeField] private ProjectileAttack projectileAttack;
    [SerializeField] private Vector2[] movementvectors = { new Vector2(0, 1) };
    private int movementvectorIndex = 0;

    public override void Start()
    {
        base.Start();
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
    public override void TelegraphAttack()
    {
        throw new System.NotImplementedException();
    }

    public void DoMovement()
    {
        if (stepsUntilFire_ > pauseStepsBeforeFire)
        {
            Vector2 movementVector = movementvectors[movementvectorIndex];
            Vector2 transformation = transform.right * movementVector.x + transform.up * movementVector.y;
            gridEntity.MoveRelativeToCurrentPosition(transformation);
            movementvectorIndex++;
            if(movementvectorIndex>=movementvectors.Length)
            {
                movementvectorIndex = 0;
            }
        }
        else
        {
            if(stepsUntilFire_ <= 0)
            {
                projectileSource.FireProjectileAttack(projectileAttack);
                stepsUntilFire_ = stepsUntilFire + pauseStepsBeforeFire;
            }
            else if (stepsUntilFire == 1)
            {
                TelegraphAttack();
            }
        }
        stepsUntilFire_ --;
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
