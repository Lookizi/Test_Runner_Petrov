using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSystem : MonoBehaviour
{
    [SerializeField] private GameObject gameMusic;
    [SerializeField] private GameObject menuMusic;
    private AudioSource gameMusicSource;
    [SerializeField] private GameObject menu;
    private float deltaVolume = 0.007f;
    // Start is called before the first frame update
    void Start()
    {
        gameMusicSource = gameMusic.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("health") <= 0)
        {
            
                gameMusicSource.volume -= deltaVolume;
            
        }
    }
}
