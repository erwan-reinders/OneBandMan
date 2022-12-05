using UnityEngine;

public class Conductor : MonoBehaviour
{
    public static Conductor Instance;


    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public double songBpm = 1.0d;

    //The number of seconds for each song beat
    public double secPerBeat;
    public double invSecPerBeat;

    //Current song position, in seconds
    public float songPosition;
    public float songPositionUI;

    //Current song position, in beats
    public float songPositionInBeats;
    public float songPositionInBeatsUI;

    //How many seconds have passed since the song started
    public double songStartTime;
    //How many seconds of offset in the music
    public double songOffset;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public bool musicPlaying;

    void OnEnable()
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
    }

    public void Play()
    {
        if (!musicPlaying)
        {
            //Calculate the number of seconds in each beat
            secPerBeat = 60d / songBpm;
            invSecPerBeat = 1d / secPerBeat;

            //Record the time when the music starts
            songStartTime = AudioSettings.dspTime;

            //Start the music
            musicSource.Play();

            musicPlaying = true;

            songPosition = 0;
            songPositionUI = 0;
            songPositionInBeats = 0;
            songPositionInBeatsUI = 0;
        }
    }

    public double TimeToBeat(double time)
    {
        return (time - songStartTime - songOffset) * invSecPerBeat;
    }
    public double BeatToTime(double beat)
    {
        return (beat * secPerBeat) + songOffset;
    }

    void Update()
    {
        //determine how many seconds since the song started
        songPosition = (float)(AudioSettings.dspTime - songStartTime - songOffset);
        songPositionUI = songPosition - (float)songOffset;

        //determine how many beats since the song started
        songPositionInBeats = (float)(songPosition * invSecPerBeat);
        songPositionInBeatsUI = (float)(songPositionUI * invSecPerBeat);

        if (!musicSource.isPlaying)
        {
            musicPlaying = false;
        }
    }
}
