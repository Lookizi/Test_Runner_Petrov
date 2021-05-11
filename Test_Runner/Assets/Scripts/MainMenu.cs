using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Text bestScoreText;
    public AudioMixerGroup Mixer;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    public void UISound()
    {
        audioSource.Play();
    }

    public void ToggleMusic(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("MusicVolume", 0);
        }
        else
        {
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
        }
    }

    public void ChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80,0,volume));
    }
}
