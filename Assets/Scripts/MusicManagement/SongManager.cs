using System.IO;
using System.Text;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public string songName;
    public SongChanelManager[] chanels;

    public static string GetSongPathFromName(string songName)
    {
        return Application.dataPath + "/Resources/" + songName;
    }

    public static void SaveSongToJSON(string songName, SongSettings songSettings)
    {
        string path = GetSongPathFromName(songName);
        string filePath = path + "/songNotes.json";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenWrite(filePath);
        }
        else
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            file = File.Create(filePath);
        }

        string json = SongSettings.SaveToJSON(songSettings);
        file.Write(Encoding.ASCII.GetBytes(json));
    }

    public static SongSettings LoadSongFromJSON(string songName)
    {
        string path = songName + "/songNotes";
        TextAsset file = Resources.Load<TextAsset>(path);
        if (file == null)
        {
            Debug.LogError("Error : " + songName + " does not exists ("+ GetSongPathFromName(songName)+ " does not contain a \"songNotes.json\" file)");
            return SongSettings.LoadFromJSON("");
        }
        return SongSettings.LoadFromJSON(file.text);
    }

    public static AudioClip GetSong(string songName)
    {
        return Resources.Load<AudioClip>(songName + "/song");
    }

    private void Start()
    {
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

            Conductor.Instance.musicSource.clip = GetSong(songName);
        }


        //Conductor.Instance.Play();
    }
}
