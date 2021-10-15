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
    public override void TelegraphAttack()
    {

    }

    public void DoMovement()
    {
        switch (youseiState)
        {
            case YouseiState.ENTERING:

                break;
            case YouseiState.EXITING:
                
                break;
            case YouseiState.FIRING:

                break;
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
