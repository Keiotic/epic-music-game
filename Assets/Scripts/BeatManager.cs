using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BeatManager : MonoBehaviour
{
    public Track track;
    private UIManager uiManager;
    public AudioSource mySource;
    public AudioClip beatAudio;
    public List<BeatEvent> beatEvents;


    private bool musicStarted = false;
    public bool ignoreBeat;
    private float timePassed;
    private float timeBetweenBeats = 1/2f;
    private int currentBeat;
    public float[] borders = {0.1f, 0.2f, 0.3f};
    private int pastBeat;
    private int beatsToStart;
    private float beatIndSpeed;

    public Text text;
    public EnemyManager enemyManager;

    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        mySource = GetComponent<AudioSource>();
        uiManager = GetComponent<UIManager>();
        timePassed = 0;
        currentBeat = 0;
        pastBeat = -100;
        musicStarted = true;

        beatEvents = new List<BeatEvent>();
        for (int i = 0; i < Mathf.Ceil(/*track.audio.length*/ 120/timeBetweenBeats); i++)
        {
            beatEvents.Add(new BeatEvent());
        }
        for (int i = 0; i < track.beatEvents.Count; i++)
        {
            BeatEvent be = track.beatEvents[i];
            beatEvents[be.beat] = be;
        }
        Debug.Log(beatEvents.Count);

        beatIndSpeed = uiManager.GetBeatMovementSpeed();
        beatsToStart = (int)Mathf.Ceil(uiManager.GetBeatWrapperWidth() / beatIndSpeed);
        float diff = beatsToStart / ((uiManager.GetBeatWrapperWidth()) / beatIndSpeed);
        beatIndSpeed *= diff;
        timePassed = -(beatsToStart + 0.5f) * timeBetweenBeats;
    }

    TimingClass cls = TimingClass.INVALID;
    void Update()
    {
        if(musicStarted)
        {
            text.text = currentBeat.ToString();
            timePassed += Time.deltaTime;
            currentBeat = Mathf.RoundToInt((timePassed) / timeBetweenBeats);
            ignoreBeat = WillIgnoreBeat(currentBeat);

            if(cls != TimingClass.EXCELLENT && GetTimingClass() == TimingClass.EXCELLENT)
            {
                mySource.PlayOneShot(beatAudio);
                cls = TimingClass.EXCELLENT;
                DoBeatEventCheck();
            }
            else if (cls != TimingClass.INVALID && GetTimingClass() == TimingClass.INVALID)
            {
                cls = TimingClass.INVALID;
            }
            if(pastBeat != currentBeat)
            {
                pastBeat = currentBeat;
                NewBeat();
            }
            uiManager.BeatUpdate(timePassed, timeBetweenBeats, currentBeat);
        }
    }

    public void DoBeatEventCheck()
    {
        if (currentBeat >= 0 && beatEvents[currentBeat] != null)
        {
            BeatEvent currentEvent = beatEvents[currentBeat];
            if (!currentBeat.Equals(new BeatEvent()))
            {
                for (int i = 0; i < currentEvent.enemySpawns.Count; i++)
                {
                    EnemySpawnEvent espE = currentEvent.enemySpawns[i];
                    if (espE.prefab)
                    {
                        enemyManager.SpawnEnemy(espE.prefab, espE.spawnPosition, espE.spawnRotation);
                    }
                }
            }
        }
    }

    public TimingClass GetTimingClass()
    {
        int beat = currentBeat;
        if (ignoreBeat == false)
        {
            if (GetAbsRelativeBeatTime(beat) < timeBetweenBeats * borders[0])
                return TimingClass.EXCELLENT;
            else if (GetAbsRelativeBeatTime(beat) < timeBetweenBeats * borders[1])
                return TimingClass.GOOD;
            else if (GetAbsRelativeBeatTime(beat) < timeBetweenBeats * borders[2])
                return TimingClass.OK;
        }
        return TimingClass.INVALID;
    }

    public float GetAbsRelativeBeatTime(int beat)
    {
        return Mathf.Abs(GetRelativeBeatTime(beat)); 
    }

    public float GetRelativeBeatTime(int beat)
    {
        return beat * timeBetweenBeats - (timePassed);
    }

    public float GetTimeBetweenBeats()
    {
        return timeBetweenBeats;
    }

    public int GetCurrentBeat()
    {
        return currentBeat;
    }

    public bool WillIgnoreBeat(int beat)
    {
        bool skip = false;
        for(int i = 0; i < track.ignoredBeats.Length; i++)
        {
            if(track.ignoredBeats[i] == beat)
            skip = true;
        }
        return skip;
    }

    public void NewBeat ()
    {
        if(currentBeat + beatsToStart >= 0 && !WillIgnoreBeat(currentBeat+beatsToStart))
            uiManager.CreateBeatIndicator(currentBeat, beatsToStart);

        if (currentBeat > 0)
            return;
        ;
    }

    public void UpdateUI ()
    {

    }

    public void PlayTrack(Track track)
    {

    }
}

public enum TimingClass
{
    INVALID,
    OK,
    GOOD,
    EXCELLENT
}
