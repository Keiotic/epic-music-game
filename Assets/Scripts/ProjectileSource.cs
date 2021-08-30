using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileSource : MonoBehaviour
{
    private AudioSource audioSource;
    private BeatManager bm;
    private bool isFiring;
    void Start()
    {
        bm = BeatManager.current;
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();
        isFiring = false;
    }

    public void FireProjectileAttack(ProjectileAttack attack)
    {
        if (attack.burstInfo.isBurst)
        {
            isFiring = true;
            StartCoroutine(FireBurst(attack));
        }
        else
        {
            isFiring = true;
            FireAttack(attack);
            isFiring = false;
        }
    }

    private IEnumerator FireBurst(ProjectileAttack attack)
    {
        for (int i = 0; i < attack.burstInfo.burstAmount; i++)
        {

            FireAttack(attack);
            yield return new WaitForSeconds(bm.GetTimeBetweenBeats() * attack.burstInfo.beatsBetweenShots);
        }
        isFiring = false;
    }

    public bool IsFiring ()
    {
        return isFiring;
    }

    private void FireAttack(ProjectileAttack attack)
    {
        for (int i = 0; i < attack.spawns.Length; i++)
        {
            ProjectileAttack.SpawnInformation spawnInfo = attack.spawns[i];
            GameObject bullet = Instantiate(attack.prefab, transform.position, transform.rotation);
            Vector2 spawnPos = Vector2.zero;
            if (attack.positionIsRelativeToSelf)
            {
                spawnPos = (Vector2)transform.position
                    + spawnInfo.spawnPosition.x * (Vector2)transform.right
                    + spawnInfo.spawnPosition.y * (Vector2)transform.up;
            }
            else
            {
                bullet.transform.position = spawnInfo.spawnPosition;
            }

            if (attack.rotationIsRelativeToSelf)
            {
                float rotation = transform.rotation.eulerAngles.z + spawnInfo.spawnRotation;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            }
            else
            {
                float rotation = spawnInfo.spawnRotation;
                bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotation));
            }

            bullet.transform.position = spawnPos;

            Projectile proj = bullet.GetComponent<Projectile>();
            proj.InitializeProjectile(attack.speed * GridManager.current.GetGridSpeedCoefficient(), attack.damage, attack.layerMask);
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
    [SerializeField] internal BurstInformation burstInfo;

    [SerializeField] internal bool positionIsRelativeToSelf = true;
    [SerializeField] internal bool rotationIsRelativeToSelf = true;

    [SerializeField] internal float speed;
    [SerializeField] internal int damage;

    [SerializeField] internal LayerMask layerMask;

    [SerializeField] internal AudioClip audio;
    [SerializeField] internal float volume = 1;
    [SerializeField] internal float pitch = 1;
    [SerializeField] internal float pitchRange = 0.1f;

    [System.Serializable]
    public class SpawnInformation
    {
        [SerializeField] internal Vector2 spawnPosition;
        [SerializeField] internal float spawnRotation;
    }

    [System.Serializable]
    public class BurstInformation
    {
        [SerializeField] internal bool isBurst = false;
        [SerializeField] internal int burstAmount = 3;
        [SerializeField] internal float beatsBetweenShots = 1;
    }


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
