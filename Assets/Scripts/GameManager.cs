using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager main;
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerObject;

    [SerializeField] private int score;

    private GridManager gridManager;
    private BeatManager beatManager;
    private UIManager uiManager;

    private int playerHealth;
    private int playerMaxHealth;

    private int playerLives;
    private int playerMaxLives = 3;

    private Vector2 playerRespawnPosition;
    private GridEntity pEnt;

    // Start is called before the first frame update
    void Start()
    {
        playerLives = playerMaxLives;
        main = this;
        gridManager = GetComponent<GridManager>();
        beatManager = GetComponent<BeatManager>();
        uiManager = GetComponent<UIManager>();
        StartCoroutine(StartGame());
        GameEvents.current.onDestroyEnemy += DestroyEnemy;
        GameEvents.current.onDestroyPlayer += DestroyPlayer;
        uiManager.UpdateScore(score);
    }

    public IEnumerator StartGame()
    {
        yield return new WaitForEndOfFrame(); //Crude code to ascertain we do not do this before the initialization process
        SpawnPlayer(gridManager.GetMiddle());
        yield return new WaitForSeconds(4);
    }

    public void Update()
    {

    }

    public void DestroyPlayer()
    {
        if (pEnt)
            playerRespawnPosition = pEnt.GetPosition();
        else
            playerRespawnPosition = gridManager.GetMiddle();
        if (playerLives > 0)
        {
            playerLives--;
            StartCoroutine(ReInitializePlayer());
        }
        else
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log("Death comes for all");
    }

    public IEnumerator ReInitializePlayer ()
    {
        yield return new WaitForSeconds(3);
        SpawnPlayer(playerRespawnPosition);
    }

    public void SpawnPlayer(Vector2 spawnPos)
    {
        playerObject = Instantiate(playerPrefab, this.transform);
        pEnt = playerObject.GetComponent<GridEntity>();
        pEnt.MoveToAbsolutePosition(spawnPos);
        pEnt.Warp();
    }

    public void DestroyEnemy(int score)
    {
        this.score += score;
        UpdateUIScore();
    }

    public void UpdateUIScore()
    {
        uiManager.UpdateScore(score);
    }

    public GameObject GetPlayer ()
    {
        if(playerObject)
            return playerObject;
        return null;
    }
}
