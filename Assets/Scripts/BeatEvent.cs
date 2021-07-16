using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatEvent
{
    public int beat;
    public List<EnemySpawnEvent> enemySpawns;
    public BackgroundEvent backgroundEvent;
}

[System.Serializable]
public class EnemySpawnEvent
{
    public GameObject prefab;
    public Vector2 spawnPosition;
    public float spawnRotation;
}

[System.Serializable]
public class BackgroundEvent
{
    public Color colorSwap;
    public float colorSwapSpeed;
}


