using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lose : MonoBehaviour
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

    public void RestartLevel()
 {
     SceneManager.LoadScene(0);
 }

 public void ToMenu()
 {
     SceneManager.LoadScene(1);
 }
 
}
