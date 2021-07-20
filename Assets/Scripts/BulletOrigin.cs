using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BulletOrigin : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void FireSingleBullet(GameObject prefab, Vector2 spawnPosition, float spawnRotation, bool positionIsRelativeToSelf, bool rotationIsRelativeToSelf, float speed, int damage, LayerMask layerMask, AudioClip audio, float volume, float pitch, float pitchRange)
    {
        
    }
}

public class ProjectileAttack
{
    [SerializeField] public GameObject prefab;
    [SerializeField] public Vector2 spawnPosition;
    [SerializeField] public float spawnRotation;
    [SerializeField] public bool positionIsRelativeToSelf;
    [SerializeField] public bool rotationIsRelativeToSelf;

    [SerializeField] public float speed;
    [SerializeField] public int damage;

    [SerializeField] public LayerMask layerMask;

    [SerializeField] public AudioClip audio;
    [SerializeField] public float volume = 1;
    [SerializeField] public float pitch = 1;
    [SerializeField] public float pitchRange = 0.1f;


    public ProjectileAttack (GameObject prefab, Vector2 spawnPosition, float spawnRotation, bool positionIsRelativeToSelf, bool rotationIsRelativeToSelf, float speed, int damage, LayerMask layerMask, AudioClip audio, float volume, float pitch, float pitchRange)
    {
        this.prefab = prefab;
        this.spawnPosition = spawnPosition;
        this.spawnRotation = spawnRotation;
        this.positionIsRelativeToSelf = positionIsRelativeToSelf;
        this.rotationIsRelativeToSelf = rotationIsRelativeToSelf;
        this.speed = speed;
        this.damage = damage;
        this.layerMask = layerMask;
        this.audio = audio;
        this.volume = volume;
        this.pitch = pitch;
        this.pitchRange = pitchRange;
    }
}
