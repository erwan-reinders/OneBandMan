using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class UserSettings
{
    public bool canPause;
    public float globalVolume;
    public float musicVolume;
    public float soundVolume;
    public Dictionary<string, InstrumentSetting> instrumentSettings;

    public UserSettings()
    {
        canPause = true;
        globalVolume = 1.0f;
        musicVolume = 1.0f;
        soundVolume = 1.0f;

        instrumentSettings = new Dictionary<string, InstrumentSetting>();
       
        InstrumentSetting setting = new InstrumentSetting();
        setting.latency = 0;
        setting.originX = -0.08f;
        setting.originY = -0.2f;
        setting.originZ = 0.15f;
        setting.length = 0.3f;
        instrumentSettings.Add("Trombone", setting);

        setting = new InstrumentSetting();
        setting.latency = 0;
        setting.originX = 0.0f;
        setting.originY = 0.0f;
        setting.originZ = 0.0f;
        setting.length = 0.0f;
        instrumentSettings.Add("Maracas", setting);

        setting = new InstrumentSetting();
        setting.latency = 0;
        setting.originX = 0.0f;
        setting.originY = 0.0f;
        setting.originZ = 0.0f;
        setting.length = 0.0f;
        instrumentSettings.Add("Drum", setting);

        setting = new InstrumentSetting();
        setting.latency = 0;
        setting.originX = -0.08f;
        setting.originY = -0.2f;
        setting.originZ = 0.15f;
        setting.length = 0.3f;
        instrumentSettings.Add("Violin", setting);

        setting = new InstrumentSetting();
        setting.latency = 0;
        setting.originX = 0.0665588379f;
        setting.originY = -0.294502258f;
        setting.originZ = 0.101745605f;
        setting.length = 0.4f;
        instrumentSettings.Add("Guitar", setting);
    }

    private static JsonSerializerSettings GetSettings()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };
    }

    public static string SaveToJSON(UserSettings userSettings)
    {
        string json = JsonConvert.SerializeObject(userSettings, GetSettings());
        return json;
    }

    public static UserSettings LoadFromJSON(string json)
    {
        UserSettings userSettings = JsonConvert.DeserializeObject<UserSettings>(json, GetSettings());
        return userSettings;
    }


    [Serializable]
    public class InstrumentSetting
    {
        public int latency;
        public float originX;
        public float originY;
        public float originZ;
        public float length;
    }
}
