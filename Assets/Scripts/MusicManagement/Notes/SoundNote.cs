using UnityEngine;

public class SoundNote : SimpleNote
{

    private string soundName;

    public SoundNote(double beat, SongChanelManager songChanelManager, string soundName) : base(beat, songChanelManager)
    {
        this.soundName = soundName;
    }

    protected override void OnAction(int timingWindow)
    {
        //Todo play effects

        if (timingWindow >= 0)
        {
            songChanelManager.audioSource.PlayOneShot(SoundManager.instance.GetClip(soundName));
        }

        songChanelManager.pool.DeleteNote(note);

        isPlaying = false;

    }
}
