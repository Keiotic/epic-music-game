using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Grunt : EnemyAI
{
    public List<Vector2> line = new List<Vector2>();
    private Vector2 movementVector;
    private Vector2 currentPoint;
    private Vector2 startPoint;
    public override void Start()
    {
        base.Start();
        movementVector = transform.up.normalized;
        print(movementVector);
        currentPoint = transform.position;
        startPoint = currentPoint;
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
        currentPoint = currentPoint + movementVector*speed*gridManager.GetGridBoxSize()/2;
        print(currentPoint);
        gridEntity.MoveToAbsolutePosition(gridManager.FindNearestGridPos(currentPoint));
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
