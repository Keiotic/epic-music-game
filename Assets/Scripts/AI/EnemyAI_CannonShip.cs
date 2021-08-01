using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_CannonShip : EnemyAI
{
    [SerializeField] private int movesUntilWait = 10;
    [SerializeField] private int waitDuration = 3;
    [SerializeField] private Turret[] turrets;
    [SerializeField] private bool fireOneByOne = false;
    [SerializeField] private int timeBetweenOneByOne = 1;
    private int fireOneByOneIndex = 0;
    [SerializeField] private int timeBetweenFirings = 1;
    private int waitTime;
    private int movesTillWait;
    private int fireTime;
    [SerializeField] private float shipSpeedCoefficient = 0.5f;

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
        gridEntity.SetAutomaticInterpolation(false);
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
            if (movesTillWait == 0)
            {
                waitTime = waitDuration;
            }
        }
        else
        {
            if (waitTime > 0)
            {
                waitTime -= 1;
                if (waitTime == 0)
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
        fireTime = (int)Mathf.Clamp(fireTime - 1, 0, Mathf.Infinity);
        if (fireTime == 1)
            TelegraphAttack();
        if (fireTime == 0)
        {
            Attack();
        }
    }

    public void Attack()
    {

        if (fireOneByOne)
        {
            turrets[fireOneByOneIndex].Attack();
            fireOneByOneIndex += 1;
            fireTime = timeBetweenOneByOne;
            if (fireOneByOneIndex == turrets.Length)
            {
                fireTime = timeBetweenFirings+1;
                fireOneByOneIndex = 0;
            }
        }
        else
        {
            for (int i = 0; i < turrets.Length; i++)
            {
                turrets[i].Attack();
            }
            fireTime = timeBetweenFirings+1;
        }
    }

    public override void MovementUpdate(int beat)
    {
        base.MovementUpdate(beat);
    }

    public override void Update()
    {
        base.Update();
        if (player)
        {
            int xOrientation = 0;
            int yOrientation = 0;
            Vector2 targetDirectionLocal = transform.InverseTransformPoint(player.transform.position);
            xOrientation = (int)Mathf.Sign(targetDirectionLocal.x);
            yOrientation = (int)Mathf.Sign(targetDirectionLocal.y);
            
            for (int i = 0; i < turrets.Length; i++)
            {
                if (turrets[i] != null)
                {
                    turrets[i].RotateTowardsAngle(-90 * xOrientation);
                }
            }
        }
        if(movesTillWait>0)
        {
            transform.Translate(Vector3.up * speed * shipSpeedCoefficient * gridManager.GetGridBoxSize() / 2 / beatManager.GetTimeBetweenBeats() * Time.deltaTime);
        }
    }

    public override void TelegraphAttack()
    {

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
