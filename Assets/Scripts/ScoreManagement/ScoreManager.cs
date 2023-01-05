using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshPro scoreText;
    public TextMeshPro comboText;
    public TextMeshPro acuracyText;

    public uint[] scoreValuesPerTimingWindow;

    private uint score;
    private uint combo;
    private uint highCombo;
    private float acuracy;

    private uint[] timingCount;
    private uint scoreCount;
    private uint hitCount;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Error : There is more than one ScoreManager !");
        }
        Initialize();
    }

    public void Initialize()
    {
        combo = 0;
        highCombo = 0;
        score = 0;
        acuracy = 1.0f;
        timingCount = new uint[scoreValuesPerTimingWindow.Length + 1];
        scoreCount = 0;
        hitCount = 0;

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        scoreText.text = Util.FormatInt(score.ToString());
        comboText.text = Util.FormatInt(combo.ToString());
        acuracyText.text = Mathf.FloorToInt(acuracy * 100).ToString()+"%";
    }

    public void ComputeScore(int timingWindowIndex)
    {
        if (timingWindowIndex < -1)
        {
            Debug.LogError("Error : invalid timing window index : "+timingWindowIndex);
            return;
        }

        hitCount++;
        timingCount[timingWindowIndex+1]++;

        if (timingWindowIndex < 0)
        {
            combo = 0;
        }
        else
        {
            scoreCount += scoreValuesPerTimingWindow[timingWindowIndex];
            combo++;
            if (combo > highCombo)
            {
                highCombo = combo;
            }
            score += combo * scoreValuesPerTimingWindow[timingWindowIndex];
        }

        uint maxValue = scoreValuesPerTimingWindow[0] * hitCount;
        acuracy = scoreCount / (float)maxValue;

        UpdateDisplay();
    }

    public void SaveCurentScore()
    {
        SongScores.SongScore currentScore = new SongScores.SongScore();
        currentScore.time = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        currentScore.score = score;
        currentScore.combo = highCombo;
        currentScore.acuracy = acuracy;

        SongScores.AddScore(SongManager.currentSongName, currentScore);
    }
}
