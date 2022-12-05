using UnityEngine;

public class SoundNote : SimpleNote
{

    private string soundName;
    private AudioClip soundClip;

    public SoundNote(double beat, SongChanelManager songChanelManager, string soundName) : base(beat, songChanelManager)
    {
        this.soundName = soundName;
        soundClip = SoundManager.instance.GetClip(soundName);
    }

    protected override void OnAction(int timingWindow)
    {
        //Todo play effects

        if (timingWindow >= 0)
        {
            if (soundName.Length > 0)
            {
                songChanelManager.audioSource.PlayOneShot(soundClip);
            }
            else
            {
                songChanelManager.audioSource.PlayOneShot(songChanelManager.hitSound);
            }
        }

        NotePoolManager.instance.pools[poolID].DeleteNote(note);

        isPlaying = false;

    }
}
