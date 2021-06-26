using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected GameObject player;
    protected Vector2 targetVector;
    protected GridManager gridManager;
    protected BeatManager beatManager;
    protected bool isInitiated;
    // Start is called before the first frame update
    void Start()
    {
        if(!isInitiated)
        {
            gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
            beatManager = gridManager.GetComponent<BeatManager>();
            isInitiated = true;
        }
    }

    public virtual void InitializeEnemy(GridManager gridManager, BeatManager beatManager)
    {
        this.gridManager = gridManager;
        this.beatManager = beatManager;
        isInitiated = true;
    }

    public virtual void CheckForPlayer()
    {

    }

    public void SetPlayerAsTargetPosition()
    {
        targetVector = gridManager.FindClampedNearestGridPos(player.transform.position);
    }

    public virtual void Update()
    {

    }
}
