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
    private int currentBeat;

    // Start is called before the first frame update
    void Start()
    {
        if(!isInitiated)
        {
            gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
            beatManager = gridManager.GetComponent<BeatManager>();
            isInitiated = true;
            currentBeat = beatManager.GetCurrentBeat();
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

    public abstract void DoTargetlessUpdate();

    public abstract void DoTargetUpdate();

    public bool hasMovedThisBeat ()
    {
        return hasMovedThisTurn;
    }

    public void MovementUpdate()
    {
        if (currentBeat != beatManager.GetCurrentBeat())
        {
            if (beatManager.GetTimingClass() == TimingClass.EXCELLENT)
            {
                currentBeat = beatManager.GetCurrentBeat();
                if (player)
                    DoTargetUpdate();
                else
                    DoTargetlessUpdate();
            }
        }
    }

    public void SetTargetPosition(Vector2 targetpos)
    {
        this.targetVector = targetpos;
    }

    public bool HasTarget()
    {
        if (player)
            return true;
        return false;
    }

    public Vector2 FindPlayerGridPosition()
    {
        if (HasTarget())
            return gridManager.FindClampedNearestGridPos(player.transform.position);
        else
            throw new MissingReferenceException("No Player Object given");
    }

    public virtual void Update()
    {

    }
}
