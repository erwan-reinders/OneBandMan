using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class SongScores
{
    public long time;
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

    [Serializable]
    public class SongScore
    {
        public long time;
        public uint score;
        public uint combo;
        public uint acuracy;
    }
}
