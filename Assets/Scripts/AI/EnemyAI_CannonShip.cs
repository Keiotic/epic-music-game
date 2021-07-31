using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_CannonShip : EnemyAI
{
    [SerializeField] private int movesUntilWait = 10;
    [SerializeField] private int waitDuration = 3;
    [SerializeField] private ProjectileAttack[] attacks;
    [SerializeField] private bool fireOneByOne = false;
    private int fireOneByOneIndex = 0;
    [SerializeField] private int timeBetweenFirings = 1;
    private int waitTime;
    private int movesTillWait;
    private int fireTime;

    public override void Start()
    {
        base.Start();
        movesTillWait = movesUntilWait;
        waitTime = waitDuration;
        fireTime = timeBetweenFirings;
    }
    public override void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player, Vector2 spawnPos)
    {
        base.InitializeEnemy(gridManager, beatManager, gameManager, player, spawnPos);

        float interpolationSpeed = (float)speed/beatManager.GetTimeBetweenBeats();
        gridEntity.SetInterpolationSpeed(interpolationSpeed);
    }

    public override void DoTargetlessUpdate()
    {
        DoMovement();
    }

    public override void DoTargetUpdate()
    {
        DoMovement();
        DoAttackCheck();
    }

    public void DoMovement()
    {
        if (movesTillWait > 0)
        {
            movesTillWait -= 1;
            gridEntity.MoveRelativeToCurrentPosition(transform.up * (float)speed);
            if (movesTillWait == 0)
            {
                waitTime = waitDuration;
            }
        }
        else
        {
            if(waitTime > 0)
            {
                waitTime -= 1;
                if(waitTime == 0)
                {
                    movesTillWait = movesUntilWait;
                }
            }

        }
    }

    public void DoAttackCheck()
    {
        if (movesTillWait > 0)
        {
            if (attackType == AttackType.ON_MOVE || attackType == AttackType.UNBOUND)
            {
                DoFireCheck();
            }
        }
        else
        {
            if (waitTime > 0)
            {
                if (attackType == AttackType.ON_WAIT || attackType == AttackType.UNBOUND)
                {
                    DoFireCheck();
                }
            }
        }
    }

    public void DoFireCheck()
    {
        fireTime = (int)Mathf.Clamp(fireTime-1, 0, timeBetweenFirings);
        if(fireTime == 0 || fireOneByOne && fireOneByOneIndex != attacks.Length)
        {
            fireTime = timeBetweenFirings;
            Attack();
        }
    }

    public void Attack()
    {
        int xOrientation = 0;
        if (player != null)
        {
            Vector2 targetDirectionLocal = transform.InverseTransformPoint(player.transform.position);
            xOrientation = -(int)Mathf.Sign(targetDirectionLocal.x);
        }
        if (fireOneByOne)
        {
            if (fireOneByOneIndex == attacks.Length)
                fireOneByOneIndex = 0;
            if(xOrientation != 0)
                for (int j = 0; j < attacks[fireOneByOneIndex].spawns.Length; j++)
                {
                    Vector2 spawnpos = attacks[fireOneByOneIndex].spawns[j].spawnPosition;
                    attacks[fireOneByOneIndex].spawns[j].spawnRotation = Mathf.Abs(attacks[fireOneByOneIndex].spawns[j].spawnRotation) * xOrientation;
                    spawnpos.x = Mathf.Abs(spawnpos.x) * Mathf.Sign(xOrientation);
                }
            projectileSource.FireProjectileAttack(attacks[fireOneByOneIndex], gridManager.GetGridSpeedCoefficient());
            fireOneByOneIndex += 1;
        }
        else
        {
            for(int i = 0; i < attacks.Length; i++)
            {
                if (xOrientation != 0)
                    for (int j = 0; j < attacks[i].spawns.Length; j++)
                    {
                        Vector2 spawnpos = attacks[i].spawns[j].spawnPosition;
                        attacks[i].spawns[j].spawnRotation = Mathf.Abs(attacks[i].spawns[j].spawnRotation)*xOrientation;
                        spawnpos.x = Mathf.Abs(spawnpos.x) * xOrientation;
                    }
                projectileSource.FireProjectileAttack(attacks[i], gridManager.GetGridSpeedCoefficient());
            }
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

    /*
    float GetLeftRightDir(Vector3 forward, Vector3 targetDirection, Vector3 up)
    {
        Vector3 perpendicular = Vector3.Cross(forward, targetDirection);
        float direction = Vector3.Dot(perpendicular, up);

        if (direction > 0f)
        {
            return 1f;
        }
        else if (direction < 0f)
        {
            return -1f;
        }
        else
        {
            return 0f;
        }
    }
    */
}
