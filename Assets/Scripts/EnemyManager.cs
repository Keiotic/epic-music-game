using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private BeatManager beatManager;
    private GridManager gridManager;
    private GameManager gameManager;
    public void Start()
    {
        beatManager = GetComponent<BeatManager>();
        gridManager = GetComponent<GridManager>();
        gameManager = GetComponent<GameManager>();
    }
    public GameObject SpawnEnemy(GameObject enemy, Vector2 position, float zRotation, Vector2[] pathingArguments)
    {
        GameObject go = Instantiate(enemy, position, Quaternion.Euler(0, 0, zRotation));
        EnemyAI ai = go.GetComponent<EnemyAI>();
        ai.InitializeEnemy(gridManager, beatManager, gameManager, gameManager.GetPlayer(), position, pathingArguments);
        return go;
    }
}
