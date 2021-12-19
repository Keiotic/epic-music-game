using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI_Yousei : EnemyAI
{
    [SerializeField] private Vector2 firePosition; //FirePosition in grid coordinates
    [SerializeField] private Vector2 exitPosition; //ExitPosition in grid coordinates
    [SerializeField] YouseiState youseiState;

    public enum YouseiState
    {
        ENTERING,
        FIRING,
        EXITING
    }
    public override void Start()
    {
 
        base.Start();
    }
    public override void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player, Vector2 spawnPos, Vector2[] pathingArguments)
    {
        base.InitializeEnemy(gridManager, beatManager, gameManager, player, spawnPos, pathingArguments);
        if(pathingArguments.Length>1)
        {
            firePosition = pathingArguments[0];
            exitPosition = pathingArguments[1];
        }
        else
        {
            firePosition = Vector2.zero;
            exitPosition = Vector2.zero;
        }
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

    }

    private Vector2 startPosition;
    public override void MovementUpdate(int beat)
    {
        base.MovementUpdate(beat);
    }

    public override void Update ()
    {
        gridEntity.LinearilyInterpolateToPosition(firePosition);
    }

    public void DoMovement()
    {
        switch (youseiState)
        {
            case YouseiState.ENTERING:
                targetPos = firePosition;
                break;
            case YouseiState.FIRING:
                targetPos = gridManager.FindNearestGridPos(transform.position);
                break;
            case YouseiState.EXITING:
                targetPos = exitPosition;
                break;
        }
    }

}
