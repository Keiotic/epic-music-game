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
    
    // Start is called before the first frame update
    void Start()
    {
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
        SpawnPlayer();
        yield return new WaitForSeconds(4);
    }

    private void DestroyPlayer()
    {
        
    }

    public void SpawnPlayer()
    {
        playerObject = Instantiate(playerPrefab, this.transform);
        GridEntity pEnt = playerObject.GetComponent<GridEntity>();
        pEnt.MoveToAbsolutePosition(new Vector2(Mathf.Round(gridManager.GetGridSize().x / 2), Mathf.Round(gridManager.GetGridSize().y / 2)));
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
