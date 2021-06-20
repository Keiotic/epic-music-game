﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class BeatManager : MonoBehaviour
{
    public Track track;
    private UIManager uiManager;
    private List<Beat> beats = new List<Beat>();
    public AudioSource mySource;
    public AudioClip beatAudio;


    private bool musicStarted = false;
    public bool ignoreBeat;
    private float timePassed;
    private float timeBetweenBeats = 1/2f;
    private int currentBeat;
    private float[] borders = {0.1f, 0.2f, 0.3f};
    private int pastBeat;
    private int beatsToStart;
    private float beatIndSpeed;

    public Text text;

    void Start()
    {
        mySource = GetComponent<AudioSource>();
        uiManager = GetComponent<UIManager>();
        timePassed = 0;
        currentBeat = 0;
        pastBeat = -100;
        musicStarted = true;

        beatIndSpeed = uiManager.GetBeatMovementSpeed();
        print(beatsToStart = (int)Mathf.Ceil(uiManager.GetBeatWrapperWidth() / beatIndSpeed));
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
            //uiManager.RemoveBeat(currentBeat - 1);
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
