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
    public double testStartOffset;

    //an AudioSource attached to this GameObject that will play the music.
    public AudioSource musicSource;

    public bool musicStarted;
    public bool musicPlaying;
    public bool musicPaused = false;

    private double pauseTime;

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
        songStartTime = AudioSettings.dspTime - testStartOffset;
    }

    public void Play()
    {
        if (!musicPlaying)
        {
            //Calculate the number of seconds in each beat
            secPerBeat = 60d / songBpm;
            invSecPerBeat = 1d / secPerBeat;

            //Record the time when the music starts
            songStartTime = AudioSettings.dspTime - testStartOffset;

            //Start the music
            if (testStartOffset >= 0)
            {
                musicSource.time = (float)testStartOffset;
                StartMusic();
            }
            else
            {
                musicSource.time = 0.0f;
                Invoke("StartMusic", -(float)testStartOffset);
            }

            musicPlaying = true;

            songPosition = 0;
            songPositionUI = 0;
            songPositionInBeats = 0;
            songPositionInBeatsUI = 0;

            musicStarted = true;
        }
    }

    private void StartMusic()
    {
        musicSource.Play();
    }

    public void Stop(bool stopSong)
    {
        if (musicPlaying)
        {
            if (stopSong)
            {
                musicSource.Stop();
            }
            musicPlaying = false;
            musicStarted = false;
        }
    }

    public void SetPause(bool pause)
    {
        if (pause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    public void Pause()
    {
        musicSource.Pause();
        musicPlaying = false;
        musicPaused = true;
        pauseTime = AudioSettings.dspTime;
    }

    public void Resume()
    {
        musicSource.UnPause();
        musicPlaying = true;
        musicPaused = false;
        double unPauseTime = AudioSettings.dspTime;
        double pauseTimeDiff = unPauseTime - pauseTime;
        songStartTime += pauseTimeDiff;
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
        if (!musicPaused)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - songStartTime - songOffset);
            songPositionUI = songPosition - (float)songOffset;

            //determine how many beats since the song started
            songPositionInBeats = (float)(songPosition * invSecPerBeat);
            songPositionInBeatsUI = (float)(songPositionUI * invSecPerBeat);
        }

/*        if (!musicSource.isPlaying)
        {
            musicPlaying = false;
        }*/
    }
}
