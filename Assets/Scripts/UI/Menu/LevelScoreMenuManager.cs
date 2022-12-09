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
        string path = songName + "/scores";
        TextAsset file = Resources.Load<TextAsset>(path);
        if (file == null)
        {
            Debug.LogWarning("There is no score file (" + path + ")");
        }
        else
        {
            SongScores songScores = SongScores.LoadFromJSON(file.text);
            foreach (SongScores.SongScore score in songScores.scores)
            {
                ScoreItemManager scoreItem = Instantiate(scorePrefab, scoresParent.transform);

                scoreItem.scoreText.text = score.score.ToString();
                scoreItem.acuracyComboText.text = score.acuracy + " | " + score.combo;

                DateTimeOffset scoreTime = DateTimeOffset.FromUnixTimeMilliseconds(score.time);
                DateTimeOffset nowTime = DateTimeOffset.Now;
                bool displayDate = scoreTime.Day == nowTime.Day && scoreTime.Month == nowTime.Month && scoreTime.Year == nowTime.Year;
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
