using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

[Serializable]
public class SongScores
{
    public List<SongScore> scores;

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
        string path = Application.dataPath + "/Resources/" + songName;
        string filePath = path + "/scores.json";
        FileStream file;
        if (File.Exists(path))
        {
            File.Delete(path);
            File.Delete(path + ".meta");
        }
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        file = File.Create(filePath);

        string json = JsonConvert.SerializeObject(songScores, GetSettings());
        file.Write(Encoding.ASCII.GetBytes(json));
        file.Close();
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
