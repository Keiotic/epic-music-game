﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrack", menuName = "Music/Track")]
public class Track : ScriptableObject
{
    [SerializeField] private int bpm = 180;

    [SerializeField] private int beatsTillStart = 4;

    [SerializeField] private int[] ignoredBeats;

    [SerializeField] private AudioClip audio;

    [SerializeField] private List<BeatEvent> beatEvents;


    public Track(int bpm, int beatsTillStart, int[] ignoredBeats, AudioClip audio, List<BeatEvent> beatEvents)
    {
        this.bpm = bpm;
        this.beatsTillStart = beatsTillStart;
        this.ignoredBeats = ignoredBeats;
        this.audio = audio;
        this.beatEvents = beatEvents;
    }


    public int GetBpm ()
    {
        return bpm;
    }

    public int GetBeatsTillStart ()
    {
        return beatsTillStart;
    }

    public int[] GetIgnoredBeats ()
    {
        return ignoredBeats;
    }
   
    public AudioClip GetAudio ()
    {
        return audio;
    }
    public List<BeatEvent> GetBeatEvents ()
    {
        return beatEvents;
    }
}