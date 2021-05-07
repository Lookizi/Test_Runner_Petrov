using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text bestScoreText;
    private void Start()
    {
        int lastGameScore = PlayerPrefs.GetInt("lastGameScore");
        int bestScore = PlayerPrefs.GetInt("bestScore");

        if (lastGameScore > bestScore)
        {
            bestScore = lastGameScore;
            PlayerPrefs.SetInt("bestScore",bestScore);
            bestScoreText.text = bestScore.ToString();
        }
        else
        {
            bestScoreText.text = bestScore.ToString();
        }
    }

    public void PLayGame()
    {
        SceneManager.LoadScene(0);
    }
}
