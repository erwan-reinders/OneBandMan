using Newtonsoft.Json;
using System;
using System.Collections.Generic;

[Serializable]
public class SongSettings
{
    public double BPM;

    public List<SongChanel> songChanels;

    public void CreateTest()
    {
        BPM = 120;
        songChanels = new List<SongChanel>();

        SongChanel chanel0 = new SongChanel();
        for (int i = 0; i < 4; i++)
        {
            chanel0.notes.Add(new SongSimpleNote(i + 5));
        }
        for (int i = 0; i < 4; i++)
        {
            chanel0.notes.Add(new SongSimpleNote(i * 0.5 + 9));
        }
        chanel0.notes.Add(new SongSimpleNote(11.5));
        for (int i = 0; i < 4; i++)
        {
            chanel0.notes.Add(new SongSimpleNote(i + 12));
        }
        for (int i = 0; i < 4; i++)
        {
            chanel0.notes.Add(new SongSimpleNote(i * 0.5 + 16));
        }
        for (int i = 0; i < 2; i++)
        {
            chanel0.notes.Add(new SongSimpleNote(i * 0.5 + 18.5));
        }
        chanel0.notes.Add(new SongSimpleNote(20));
        songChanels.Add(chanel0);


        SongChanel chanel1 = new SongChanel();
        for (int i = 0; i < 9; i++)
        {
            chanel1.notes.Add(new SongSimpleNote(i + 4));
        }
        songChanels.Add(chanel1);

        SongChanel chanel2 = new SongChanel();
        for (int i = 1; i < 5; i++)
        {
            chanel2.notes.Add(new SongHideChannel(i * 4));
        }
        songChanels.Add(chanel2);
    }

    private static JsonSerializerSettings GetSettings()
    {
        return new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
        };
    }

    public static string SaveToJSON(SongSettings songSettings)
    {
        string json = JsonConvert.SerializeObject(songSettings, GetSettings());
        return json;
    }

    public static SongSettings LoadFromJSON(string json)
    {
        SongSettings songSettings = JsonConvert.DeserializeObject<SongSettings>(json, GetSettings());
        return songSettings;
    }

    [Serializable]
    public class SongChanel
    {
        public List<SongNote> notes;

        public SongChanel()
        {
            notes = new List<SongNote>();
        }
    }

    [Serializable]
    public abstract class SongNote
    {
        public double beat;

        public virtual Note Construct(SongChanelManager songChanelManager)
        {
            return null;
        }
    }

    [Serializable]
    public class SongSimpleNote : SongNote
    {
        public SongSimpleNote(double beat)
        {
            this.beat = beat;
        }
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new SimpleNote(beat, songChanelManager);
        }
    }

    [Serializable]
    public class SongLongNote : SongNote
    {
        public double beatDuration;
        public SongLongNote(double beat, double beatDuration)
        {
            this.beat = beat;
            this.beatDuration = beatDuration;
        }
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new LongNote(beat, beatDuration, songChanelManager);
        }
    }

    [Serializable]
    public class SongGuitarStrumNote : SongNote
    {
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new GuitarStrumNote(beat, songChanelManager);
        }
    }

    [Serializable]
    public class SongSoundNote : SongNote
    {
        public string sound;
        public SongSoundNote(double beat, string sound)
        {
            this.beat = beat;
            this.sound = sound;
        }
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new SoundNote(beat, songChanelManager, sound);
        }
    }

    [Serializable]
    public class SongHideChannel : SongNote
    {
        public SongHideChannel(double beat)
        {
            this.beat = beat;
        }
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new HideChannel(beat, songChanelManager);
        }
    }
    [Serializable]
    public class SongShowChannel : SongNote
    {
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new ShowChannel(beat, songChanelManager);
        }
    }

    [Serializable]
    public class SongInstrumentVisibility: SongNote
    {
        public bool visible;
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new InstrumentVisibility(beat, songChanelManager, visible);
        }
    }

    [Serializable]
    public class SongEndSong : SongNote
    {
        public override Note Construct(SongChanelManager songChanelManager)
        {
            return new EndSong(beat, songChanelManager);
        }
    }
}
