using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Grunt : EnemyAI {
    public override void Start()
    {
        base.Start();
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

    public override void MovementUpdate(int beat)
    {
        base.MovementUpdate(beat);
    }

    public override void Update()
    {
        base.Update();

    }
}
