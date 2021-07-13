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
        gridEntity.MoveToAbsolutePosition(gridManager.FindNearestGridPos(transform.position));
        gridEntity.Warp();
        print(gridEntity.GetPosition());


        print(gridManager.FindNearestGridPos(transform.position).Equals(gridEntity.GetPosition()));
        SetNavigationTarget(gridManager.FindNearestGridPos(transform.position + transform.up * 100));
        SetPath();
        
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
        if (nav.path.Count > 0)
        {
            FollowPath();
        }
        else
        {
            SetNavigationTarget(gridManager.FindNearestGridPos(transform.position + transform.up * 100));
            SetPath();
            FollowPath();
        }
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
