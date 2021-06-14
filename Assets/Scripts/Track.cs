using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicTrack", menuName = "Music/Track")]
public class Track : ScriptableObject
{
    public int bpm = 180;

    public int beatsTillStart = 4;

    public int[] ignoredBeats;

    public AudioClip audio;

}


[System.Serializable]
public class Beat
{

}