using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Invader : EnemyAI
{
    private Vector2 forwardVector;
    public override void Start()
    {
        base.Start();
        forwardVector = transform.forward.normalized;
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

    }

    public override void MovementUpdate()
    {
        base.MovementUpdate();
    }

    public override void Update()
    {
        base.Update();

    }
}
