using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class SongParameters
{
    public double BPM;
    public string description;

    private static JsonSerializerSettings GetSettings()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };
    }

    public static SongParameters LoadFromJSON(string json)
    {
        SongParameters songParameters = JsonConvert.DeserializeObject<SongParameters>(json, GetSettings());
        return songParameters;
    }
}
