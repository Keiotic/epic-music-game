using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(GridEntity))]
public abstract class EnemyAI : MonoBehaviour
{
    [SerializeField] protected GameObject player;
    protected Vector2 targetPos;
    protected GridManager gridManager;
    protected BeatManager beatManager;
    protected GameManager gameManager;
    protected bool isInitiated;
    protected bool hasMovedThisTurn;
    protected GridEntity gridEntity;
    protected Navigation nav = new Navigation();
    protected bool movesDiagonally = false;
    protected ProjectileSource projectileSource;
    [SerializeField] protected Telegraphing telegraphingSettings;
    [SerializeField] protected AudioSource audioSource;
    public class Telegraphing
    {
        [SerializeField] protected int telegraphDuration;
        [SerializeField] protected AudioClip telegraphAudio;
    }
    protected class Navigation
    {
        public Pathfinder pather;
        public List<PathNode> path = new List<PathNode>();
        public Vector2 target = new Vector2();
    }
    [SerializeField] protected int speed = 1;
    private int currentBeat;
    [SerializeField] protected AttackType attackType;

    public enum AttackType
    {
        ON_WAIT,
        ON_MOVE,
        UNBOUND
    }

    private Vector2[] gridpath;

    // Start is called before the first frame update
    public virtual void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        projectileSource = GetComponent<ProjectileSource>();
        gridEntity = GetComponent<GridEntity>();
        if (!isInitiated)
        {
            gridManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GridManager>();
            gameManager = gridManager.GetComponent<GameManager>();
            beatManager = gridManager.GetComponent<BeatManager>();
            isInitiated = true;
            currentBeat = beatManager.GetCurrentBeat();
            CreatePathfinder(gridManager);
        }
        GameEvents.current.onBeat += MovementUpdate;
    }

    private void OnDestroy()
    {
        GameEvents.current.onBeat -= MovementUpdate;
    }

    public void CreatePathfinder(GridManager gridManager)
    {
        nav.pather = new Pathfinder(gridManager.GetGrid(), movesDiagonally);
    }

    public void SetNavigationTarget(Vector2 gridPos)
    {
        nav.target = gridPos;
    }

    public void SetPath()
    {
        int posX = Mathf.Clamp((int)gridEntity.GetPosition().x, 0, (int)gridManager.GetGrid().GetSize().x);
        int posY = Mathf.Clamp((int)gridEntity.GetPosition().y, 0, (int)gridManager.GetGrid().GetSize().y);

        int tposX = Mathf.Clamp((int)nav.target.x, 0, (int)gridManager.GetGrid().GetSize().x);
        int tposY = Mathf.Clamp((int)nav.target.y, 0, (int)gridManager.GetGrid().GetSize().y);

        nav.path = nav.pather.FindPath(posX, posY, tposX, tposY);
        nav.path.RemoveAt(nav.path.Count - 1);
    }

    public void FollowPath()
    {
        if (nav.path != null && nav.path.Count > 0)
        {
            PathNode node = nav.path[nav.path.Count - 1];
            gridEntity.MoveToAbsolutePosition(node.position);
            gridEntity.Warp();
            nav.path.Remove(node);
        }
        else
        {
            print("All points done!");
        }
    }

    public virtual void InitializeEnemy(GridManager gridManager, BeatManager beatManager, GameManager gameManager, GameObject player, Vector2 spawnPosition, Vector2[] pathing)
    {
        this.gridManager = gridManager;
        this.beatManager = beatManager;
        this.gameManager = gameManager;
        this.player = player;
        this.gridpath = pathing;
        gridEntity = GetComponent<GridEntity>();
        CreatePathfinder(gridManager);
        spawnPosition = gridManager.ClampToGridSize(spawnPosition);
        gridEntity.MoveToAbsolutePosition(spawnPosition);
        gridEntity.Warp();
        isInitiated = true;
    }

    public virtual void MovementUpdate(int beat)
    {
        currentBeat = beat;
        if (player)
            DoTargetUpdate();
        else
            DoTargetlessUpdate();
    }

    public virtual void Update()
    {
        gridEntity.LinearilyInterpolatePosition();
    }

    public abstract void DoTargetlessUpdate();

    public abstract void DoTargetUpdate();

    public abstract void TelegraphAttack();

    public bool hasMovedThisBeat()
    {
        return hasMovedThisTurn;
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
}
