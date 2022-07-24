using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManeger: MonoBehaviour 
{
    [SerializeField] TextMeshProUGUI HighScore_Text;

    private int Score;

    private int HighScore = 0;

    private void HighScoreGet()
    {
        if (Score > HighScore)
        {
            HighScore = Score;
        }
    }
}
