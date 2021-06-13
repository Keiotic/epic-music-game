using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatManager : MonoBehaviour
{
    private bool musicStarted = true;
    private float timePassed;
    private float timeBetweenBeats = 0.25f;
    private int currentBeat;
    private float[] borders = {0.05f, 0.1f, 0.25f};

    int pastBeat;
    void Start()
    {
        timePassed = 0;
        currentBeat = 0;
        pastBeat = -1;
    }

    void Update()
    {
        if(musicStarted)
        {
            timePassed += Time.deltaTime;
            currentBeat = Mathf.RoundToInt(timePassed / timeBetweenBeats);
            if(pastBeat != currentBeat)
            {
                pastBeat = currentBeat;
            }
        }
    }

    public TimingClass GetTimingClass(int beat)
    {
        if (GetAbsRelativeBeatTime(beat) < timeBetweenBeats * 0.05f)
            return TimingClass.EXCELLENT;
        else if (GetAbsRelativeBeatTime(beat) < timeBetweenBeats * 0.1f)
            return TimingClass.GOOD;
        else if (GetAbsRelativeBeatTime(beat) < timeBetweenBeats * 0.25f)
            return TimingClass.OK;
        return TimingClass.INVALID;
    }

    public float GetAbsRelativeBeatTime(int beat)
    {
        return Mathf.Abs(GetRelativeBeatTime(beat)); 
    }

    public float GetRelativeBeatTime(int beat)
    {
        return beat*timeBetweenBeats-timePassed;
    }

    public int GetCurrentBeat()
    {
        return currentBeat;
    }
}

public enum TimingClass
{
    INVALID,
    OK,
    GOOD,
    EXCELLENT
}
