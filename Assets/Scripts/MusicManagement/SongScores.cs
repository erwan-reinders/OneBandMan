using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[Serializable]
public class SongScores
{
    public static string IN_SONG_FILE_NAME = "scores.json";

    public List<SongScore> scores;

    public SongScores()
    {
        scores = new List<SongScore>();
    }

    private static JsonSerializerSettings GetSettings()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };
    }

    public static SongScores LoadFromJSON(string json)
    {
        SongScores songScores = JsonConvert.DeserializeObject<SongScores>(json, GetSettings());
        return songScores;
    }

    public static void SaveSongToJSON(string songName, SongScores songScores)
    {
        Util.WriteFile(Util.GetSongPath(songName) + "/" + IN_SONG_FILE_NAME, JsonConvert.SerializeObject(songScores, GetSettings()));
    }

    public static void AddScore(string songName, SongScore score)
    {
        string path = Util.GetSongPath(songName) + "/" + IN_SONG_FILE_NAME;
        string text = Util.ReadFile(path);
        SongScores scores;
        if (text.Equals(""))
        {
            scores = new SongScores();
        }
        else
        {
            scores = LoadFromJSON(text);
        }

        scores.scores.Add(score);

        Util.WriteFile(path, JsonConvert.SerializeObject(scores, GetSettings()));
    }

    [Serializable]
    public class SongScore
    {
        public long time;
        public uint score;
        public uint combo;
        public float acuracy;
    }
}
