using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject SpawnEnemy(GameObject enemy, Vector2 position, float zRotation)
    {
        GameObject go = Instantiate(enemy, position, Quaternion.Euler(0, 0, zRotation));
        EnemyAI ai = go.GetComponent<EnemyAI>();
        ai.InitializeEnemy(GetComponent<GridManager>(), GetComponent<BeatManager>(), GetComponent<GameManager>());
        return go;
    }
}
