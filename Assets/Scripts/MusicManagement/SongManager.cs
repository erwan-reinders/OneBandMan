using System.IO;
using System.Text;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public static string currentSongName;
    public static string IN_SONG_FILE_NAME = "songNotes.json";

    public string songName;
    public SongChanelManager[] chanels;

    public static void SaveSongToJSON(string songName, SongSettings songSettings)
    {
        Util.WriteFile(Util.GetSongPath(songName) + "/" + IN_SONG_FILE_NAME, SongSettings.SaveToJSON(songSettings));
    }

    public static SongSettings LoadSongFromJSON(string songName)
    {
        return SongSettings.LoadFromJSON(Util.ReadFile(Util.GetSongPath(songName) + "/" + IN_SONG_FILE_NAME));
    }

    private void Start()
    {
        currentSongName = songName;
        if (songName.Length > 0) {
            SongSettings settings = LoadSongFromJSON(songName);

            int nbSongChannel = settings.songChanels.Count;
            if (chanels.Length != nbSongChannel)
            {
                Debug.LogWarning("Warning : Scene chanels (" + chanels.Length + ") does note match song file chanels (" + nbSongChannel + ")");
            }
            for (int c = 0; c < nbSongChannel; c++)
            {
                SongSettings.SongChanel songChanel = settings.songChanels[c];
                SongChanelManager channel = chanels[c];
                int nbNotes = songChanel.notes.Count;
                Note[] notes = new Note[nbNotes];
                for (int n = 0; n < nbNotes; n++)
                {
                    notes[n] = songChanel.notes[n].Construct(channel);
                }
                channel.SetNotes(notes);
            }

            Conductor.Instance.musicSource.clip = Resources.Load<AudioClip>(songName);
        }


        //Conductor.Instance.Play();
    }
}
