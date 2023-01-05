using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScoreMenuManager : MonoBehaviour
{
    public GameObject scoresParent;
    public ScoreItemManager scorePrefab;

    public void LoadSongInfo(string songName)
    {
        /*        SongScores tmpScores = new SongScores();
                SongScores.SongScore tmpScore = new SongScores.SongScore();
                tmpScore.time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                tmpScore.score = 123456789;
                tmpScore.combo = 1234;
                tmpScore.acuracy = 0.96f;
                tmpScores.scores = new List<SongScores.SongScore>();
                tmpScores.scores.Add(tmpScore);
                tmpScores.scores.Add(tmpScore);

                Debug.Log("saving json : " + songName);
                SongScores.SaveSongToJSON(songName, tmpScores);*/



        for (int i = scoresParent.transform.childCount - 1; i >= 0; --i)
        {
            Destroy(scoresParent.transform.GetChild(i).gameObject);
        }

        string path = Util.GetSongPath(songName) + "/scores.json";
        string text = Util.ReadFile(path);
        if (text.Equals(""))
        {
            Debug.LogWarning("There is no score file (" + path + ")");
        }
        else
        {
            SongScores songScores = SongScores.LoadFromJSON(text);

            for (int i = songScores.scores.Count-1; i >= 0; i--)
            {
                SongScores.SongScore score = songScores.scores[i];
                ScoreItemManager scoreItem = Instantiate(scorePrefab, scoresParent.transform);

                scoreItem.scoreText.text = Util.FormatInt(score.score.ToString());
                scoreItem.acuracyComboText.text = Util.FormatPercent(score.acuracy) + " | " + Util.FormatInt(score.combo.ToString());

                DateTimeOffset scoreTime = DateTimeOffset.FromUnixTimeMilliseconds(score.time);
                DateTimeOffset nowTime = DateTimeOffset.Now;
                bool displayDate = scoreTime.Day != nowTime.Day || scoreTime.Month != nowTime.Month || scoreTime.Year != nowTime.Year;
                string timeText = "";
                if (displayDate)
                {
                    timeText += scoreTime.Day + "/" + scoreTime.Month + "/" + scoreTime.Year;
                }
                scoreItem.timeText.text = timeText + " " + scoreTime.Hour + "h" + scoreTime.Minute;
            }
        }
    }
}
