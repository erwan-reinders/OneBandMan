using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance;


    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public double songBpm;

    //The number of seconds for each song beat
    public double secPerBeat;
    public double invSecPerBeat;

    //Current song position, in seconds
    public float songPosition;

    //Current song position, in beats
    public float songPositionInBeats;

    //How many seconds have passed since the song started
    public double songStartTime;
    //How many seconds of offset in the music
    public double songOffset;
	//0.095

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Only one Conductor must exist!");
        }

        //Calculate the number of seconds in each beat
        secPerBeat = 60d / songBpm;
        invSecPerBeat = 1d / secPerBeat;

        //Record the time when the music starts
        songStartTime = AudioSettings.dspTime;

        //Start the music
        musicSource.Play();
    }

    public double getPosInBeatFromTime(double time)
    {
        return (time - songStartTime - songOffset) * invSecPerBeat;
    }

    // Update is called once per frame
    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - songStartTime - songOffset);

        //determine how many beats since the song started
        songPositionInBeats = (float)(songPosition * invSecPerBeat);
    }
}
