﻿using System.Collections;
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
        gridManager = GetComponent<GridManager>();
        beatManager = GetComponent<BeatManager>();
        uiManager = GetComponent<UIManager>();
        StartCoroutine(StartGame());
    }

    public IEnumerator StartGame()
    {
        SpawnPlayer();
        print("Started Couroutine");
        yield return new WaitForSeconds(4);
    }

    public void SpawnPlayer()
    {
        playerObject = Instantiate(playerPrefab, this.transform);
        GridEntity pEnt = playerObject.GetComponent<GridEntity>();
        pEnt.MoveToAbsolutePosition(new Vector2(Mathf.Round(gridManager.GetGridSize().x / 2), Mathf.Round(gridManager.GetGridSize().y / 2)));
    }

    public GameObject GetPlayer ()
    {
        if(playerObject)
            return playerObject;
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
