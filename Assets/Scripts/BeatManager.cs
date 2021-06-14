using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public Track track;
    private UIManager uiManager;
    private List<Beat> beats = new List<Beat>();


    private bool musicStarted = false;
    private bool ignoreBeat;
    private float timePassed;
    private float timeBetweenBeats = 1f;
    private int currentBeat;
    private float[] borders = {0.05f, 0.1f, 0.3f};
    private int pastBeat;
    private int beatHeadstart;
    private float beatIndSpeed;



    void Start()
    {
        uiManager = GetComponent<UIManager>();
        timePassed = 0;
        currentBeat = 0;
        pastBeat = -100;
        musicStarted = true;

        beatIndSpeed = uiManager.GetBeatMovementSpeed();
        beatHeadstart = (int)Mathf.Ceil(uiManager.GetBeatWrapperWidth() / 2 * beatIndSpeed);
        float diff = beatHeadstart/uiManager.GetBeatWrapperWidth() / 2 * beatIndSpeed;
        beatIndSpeed *= diff;
    }


    void Update()
    {
        if(musicStarted)
        {
            timePassed += Time.deltaTime;
            currentBeat = Mathf.RoundToInt((timePassed-beatHeadstart*timeBetweenBeats) / timeBetweenBeats);
            ignoreBeat = WillIgnoreBeat();
            if(pastBeat != currentBeat)
            {
                pastBeat = currentBeat;
                print(currentBeat + ": " + ignoreBeat);
                NewBeat();
            }
        }
    }

    public TimingClass GetTimingClass()
    {
        int beat = currentBeat;
        if (!ignoreBeat)
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
        return beat * timeBetweenBeats - timePassed;
    }

    public int GetCurrentBeat()
    {
        return currentBeat;
    }

    public bool WillIgnoreBeat()
    {
        return currentBeat < 0;
    }

    public void NewBeat ()
    {
        uiManager.NewBeat(currentBeat);
        if (currentBeat + beatHeadstart >= 0)
        {
            uiManager.CreateBeatIndicator(currentBeat + beatHeadstart);
        }
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
