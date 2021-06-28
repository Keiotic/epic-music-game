using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    protected GameObject player;
    protected Vector2 targetVector;
    protected GridManager gridManager;
    protected BeatManager beatManager;
    protected GameManager gameManager;
    protected bool isInitiated;
    protected bool hasMovedThisTurn;

    public int speed = 1;
    
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

    public virtual void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player)
    {
        this.gridManager = gridManager;
        this.beatManager = beatManager;
        this.gameManager = gameManager;
        this.player = player;
        isInitiated = true;
    }

    public virtual void CheckForPlayer()
    {

    }

    public bool hasMovedThisBeat ()
    {
        return hasMovedThisTurn;
    }

    public void UpdateMovementBoolean ()
    {

    }

    public void SetTargetPosition(Vector2 targetpos)
    {
        this.targetVector = targetpos;
    }

    public Vector2 FindPlayerGridPosition()
    {
        if (player)
            return gridManager.FindClampedNearestGridPos(player.transform.position);
        else
            throw new MissingReferenceException("No Player Object given");
    }

    public virtual void Update()
    {

    }
}
