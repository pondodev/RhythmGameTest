using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    // all code based off article https://www.gamasutra.com/blogs/GrahamTattersall/20190515/342454/Coding_to_the_Beat__Under_the_Hood_of_a_Rhythm_Game_in_Unity.php
    public float songBpm;
    public float secPerBeat;
    private int currentBeat = 0;

    public float songPosition;
    public float songPositionInBeats;
    public float dspSongTime; // we want to base our timing off the song itself instead of the more inconsistent Time class
    public float firstBeatOffset; // time in seconds that we wish to offset the first beat

    public AudioSource musicSource;
    public AudioSource clickTrack;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        secPerBeat = 60f / songBpm;
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();

        Debug.Log("Song Started");
    }

    // Update is called once per frame
    void Update()
    {
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset); // get our current song time based on the track's dsp time
        songPositionInBeats = songPosition / secPerBeat; // track current song time in beats, which is useful for the actual rhythm part of the game

        // just checking if we've advanced another beat to play a click
        if (Mathf.Floor(songPositionInBeats) > currentBeat)
        {
            currentBeat++;
            clickTrack.Play();
        }

        // check if the song has indeed ended
        if (!musicSource.isPlaying)
            Debug.Log("Song Ended");
    }
}
