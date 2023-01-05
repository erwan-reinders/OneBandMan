using System.IO;
using System.Text;
using UnityEngine;

public static class Util
{
    public static void GetFloorFrac(float x, out float floor, out float frac)
    {
        floor = Mathf.Floor(x);
        frac = x - floor;
    }

    public static float EaseInCubic(float x)
    {
        return x * x * x;
    }

    public static float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            return n1 * (x -= 1.5f / d1) * x + 0.75f;
        }
        else if (x < 2.5 / d1)
        {
            return n1 * (x -= 2.25f / d1) * x + 0.9375f;
        }
        else
        {
            return n1 * (x -= 2.625f / d1) * x + 0.984375f;
        }
    }

    public static bool SameTime(float timeA, float timeB)
    {
        return Mathf.Abs(timeA - timeB) < 0.016f;
    }

    public static string FormatInt(string text)
    {
        int textLength = text.Length;
        for (int i = 3; i < textLength; i += 3)
        {
            text = text.Insert(textLength - i, " ");
        }
        return text;
    }

    public static string FormatPercent(float percent)
    {
        return Mathf.Round(percent * 1000f)/10f + "%";
    }


    public static string GetDataPath()
    {
        string dataPath = Application.dataPath + "/Data";
        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }
        return dataPath;
    }
    public static string GetSongPath(string songName)
    {
        string songPath = GetDataPath() + "/" + songName;
        if (!Directory.Exists(songPath))
        {
            Directory.CreateDirectory(songPath);
        }
        return songPath;
    }

    public static string ReadFile(string filePath)
    {
        FileStream fs;
        int readLen = 1024;
        if (File.Exists(filePath))
        {
            fs = File.OpenRead(filePath);

            byte[] readArr = new byte[readLen];
            int count;
            StringBuilder text = new StringBuilder();
            while ((count = fs.Read(readArr, 0, readLen)) > 0) {
                text.Append(Encoding.UTF8.GetString(readArr, 0, count));
            }

            fs.Close();

            return text.ToString();
        }

        return "";
    }

    public static void WriteFile(string filePath, string text)
    {
        FileStream fs;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        fs = File.Create(filePath);
        fs.Write(Encoding.UTF8.GetBytes(text));
        fs.Close();
    }
}
