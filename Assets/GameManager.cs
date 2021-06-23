using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    private GameObject playerObject;

    private int score;

    private GridManager gridManager;
    private BeatManager beatManager;
    private UIManager uiManager;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator StartGame()
    {

        yield return new WaitForSeconds(4);
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        playerObject = Instantiate(playerPrefab, this.transform);
        GridEntity pEnt = playerObject.GetComponent<GridEntity>();
        pEnt.MoveToAbsolutePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
