using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BeatEvent
{
    public int beat;
    public List<EnemySpawnEvent> enemySpawns;
    public BackgroundEvent backgroundEvent;
    public BeatEvent () {
        beat = 0;
        enemySpawns = new List<EnemySpawnEvent>();
        backgroundEvent = new BackgroundEvent();
    }
    public BeatEvent(int beat, List<EnemySpawnEvent> enemySpawns, BackgroundEvent backgroundEvent)
    {
        this.beat = beat;
        this.enemySpawns = enemySpawns;
        this.backgroundEvent = backgroundEvent;
    }
}

[System.Serializable]
public class EnemySpawnEvent
{
    public GameObject prefab;
    public Vector2 spawnPosition;
    public float spawnRotation;
    public Vector2[] pathingArguments;

    public EnemySpawnEvent ()
    {
        prefab = null;
        spawnPosition = Vector2.zero;
        spawnRotation = 0;
        pathingArguments = new Vector2[0];
    }
}

[System.Serializable]
public class BackgroundEvent
{
    public Color colorSwap;
    public float colorSwapSpeed;
    public BackgroundEvent ()
    {
        colorSwap = Color.red;
        colorSwapSpeed = 0;
    }
}


