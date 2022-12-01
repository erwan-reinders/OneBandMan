using System.IO;
using System.Text;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    public string songName;
    public SongChanelManager[] chanels;

    public static string GetSongPathFromName(string songName)
    {
        return Application.dataPath + "/Songs/" + songName;
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
        string path = GetSongPathFromName(songName) + "/songNotes.json";
        FileStream file;
        if (File.Exists(path))
        {
            file = File.OpenRead(path);
        }
        else
        {
            Debug.LogError("Error : " + songName + " does not exists (" + path+")");
            return null;
        }

        byte[] b = new byte[1024];
        int readLen;
        StringBuilder stringBuilder = new StringBuilder();
        while ((readLen = file.Read(b, 0, b.Length)) > 0)
        {
            stringBuilder.Append(Encoding.ASCII.GetString(b, 0, readLen));
        }

        return SongSettings.LoadFromJSON(stringBuilder.ToString());
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
        }


        //Conductor.Instance.Play();
    }
}
