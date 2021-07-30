using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BeatManager : MonoBehaviour
{
    public static BeatManager current;
    [SerializeField] private Track track;
    private UIManager uiManager;
    [SerializeField] private AudioSource mySource;
    [SerializeField] private AudioClip beatAudio;
    [SerializeField] private List<BeatEvent> beatEvents;


    private bool musicStarted = false;
    [SerializeField] private bool ignoreBeat;
    private float timePassed;
    private float timeBetweenBeats = 1/2f;
    private int currentBeat;
    [SerializeField] private float[] borders = {0.1f, 0.2f, 0.3f};
    private int pastBeat;
    private int beatsToStart;
    private float beatIndSpeed;

    public Text text;
    public EnemyManager enemyManager;
    private int nextBeat;
    private int totalBeats;

    void Start()
    {
        current = this;
        enemyManager = GetComponent<EnemyManager>();
        mySource = GetComponent<AudioSource>();
        uiManager = GetComponent<UIManager>();
        timePassed = 0;
        currentBeat = 0;
        pastBeat = -100;
        musicStarted = true;

        float bpm = track.GetBpm();
        timeBetweenBeats = (float)60 / bpm;

        totalBeats = Mathf.RoundToInt(track.GetAudio().length/timeBetweenBeats);

        beatEvents = new List<BeatEvent>();
        for (int i = 0; i < Mathf.Ceil(totalBeats); i++)
        {
            beatEvents.Add(new BeatEvent());
        }

        List<BeatEvent> savedBeatEvents = track.GetBeatEvents();
        for (int i = 0; i < savedBeatEvents.Count; i++)
        {
            BeatEvent be = savedBeatEvents[i];
            beatEvents[be.beat] = be;
        }


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
            if (timePassed < track.GetAudio().length)
            {
                text.text = (nextBeat - 1).ToString();
                timePassed += Time.deltaTime;
                currentBeat = Mathf.RoundToInt((timePassed) / timeBetweenBeats);
                ignoreBeat = WillIgnoreBeat(currentBeat);

                if (currentBeat >= 0 && nextBeat * timeBetweenBeats < timePassed)
                {
                    if (currentBeat == 0)
                    {
                        mySource.clip = track.GetAudio();
                        mySource.Play();
                    }
                    mySource.PlayOneShot(beatAudio);
                    DoBeatEventCheck();
                    GameEvents.current.Beat(currentBeat);
                    nextBeat += 1;
                }

                if (pastBeat != currentBeat)
                {
                    pastBeat = currentBeat;
                    NewBeat();
                }
            }
            uiManager.BeatUpdate(timePassed, timeBetweenBeats, currentBeat);
        }
    }

    public void DoBeatEventCheck()
    {
        if (currentBeat >= 0 && currentBeat < totalBeats && beatEvents[currentBeat] != null)
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
        int[] ignoredBeats = track.GetIgnoredBeats();
        for(int i = 0; i < ignoredBeats.Length; i++)
        {
            if(ignoredBeats[i] == beat)
            skip = true;
        }
        return skip;
    }

    public void NewBeat ()
    {
        if(currentBeat + beatsToStart >= 0 && !WillIgnoreBeat(currentBeat+beatsToStart)&& currentBeat+beatsToStart<=totalBeats)
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
