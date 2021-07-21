using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ProjectileSource : MonoBehaviour
{
    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void FireSingleProjectile(GameObject prefab, Vector2 spawnPosition, float spawnRotation, bool positionIsRelativeToSelf, bool rotationIsRelativeToSelf, float speed, int damage, LayerMask layerMask, AudioClip audio, float volume, float pitch, float pitchRange)
    {

    }

    public void FireProjectileAttack(ProjectileAttack attack, float gridSpeedCoefficient)
    {
        for (int i = 0; i < attack.spawns.Length; i++)
        {
            GameObject bullet = Instantiate(attack.prefab, transform.position, transform.rotation);
            if (attack.positionIsRelativeToSelf)
            {
                bullet.transform.position = (Vector2)transform.position + attack.spawns[i].spawnPosition;
            }
            else
            {
                bullet.transform.position = attack.spawns[i].spawnPosition;
            }
            if (attack.rotationIsRelativeToSelf)
            {
                float rotation = transform.rotation.eulerAngles.z + attack.spawns[i].spawnRotation;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            }
            else
            {
                float rotation = attack.spawns[i].spawnRotation;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            }
            Projectile proj = bullet.GetComponent<Projectile>();
            proj.InitializeProjectile(attack.speed * gridSpeedCoefficient, attack.damage, attack.layerMask);
        }
    }

    public void PlayFiringSound(AudioClip audio, float volume, float pitch, float randompitch)
    {
        if (audio)
        {
            audioSource.clip = audio;
            audioSource.volume = volume;
            audioSource.pitch = pitch + Random.Range(-randompitch, randompitch);
            audioSource.Play();
        }
    }
}

[System.Serializable]
public class ProjectileAttack
{
    [SerializeField] internal GameObject prefab;
    [SerializeField] internal SpawnInformation[] spawns;

    [System.Serializable]
    public class SpawnInformation
    {
        [SerializeField] internal Vector2 spawnPosition;
        [SerializeField] internal float spawnRotation;
    }

    [SerializeField] internal bool positionIsRelativeToSelf = true;
    [SerializeField] internal bool rotationIsRelativeToSelf = true;

    [SerializeField] internal float speed;
    [SerializeField] internal int damage;

    [SerializeField] internal LayerMask layerMask;

    [SerializeField] internal AudioClip audio;
    [SerializeField] internal float volume = 1;
    [SerializeField] internal float pitch = 1;
    [SerializeField] internal float pitchRange = 0.1f;


    public ProjectileAttack(GameObject prefab, SpawnInformation[] spawns, bool positionIsRelativeToSelf, bool rotationIsRelativeToSelf, float speed, int damage, LayerMask layerMask, AudioClip audio, float volume, float pitch, float pitchRange)
    {
        this.prefab = prefab;
        this.spawns = spawns;
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

    public GameObject GetPrefab()
    {
        return prefab;
    }
    public SpawnInformation[] GetSpawns()
    {
        return spawns;
    }
    public bool GetPositionIsRelativeToSelf()
    {
        return positionIsRelativeToSelf;
    }
    public bool GetRotationIsRelativeToSelf()
    {
        return rotationIsRelativeToSelf;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public int GetDamage()
    {
        return damage;
    }
    public LayerMask GetLayerMask()
    {
        return layerMask;
    }
    public AudioClip GetAudioClip()
    {
        return audio;
    }
    public float GetVolume()
    {
        return volume;
    }
    public float GetPitch()
    {
        return pitch;
    }
    public float GetPitchRange()
    {
        return pitchRange;
    }
}
