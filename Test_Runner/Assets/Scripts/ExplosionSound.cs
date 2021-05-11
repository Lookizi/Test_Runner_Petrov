using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplosionSound : MonoBehaviour
{
    
    [SerializeField] private AudioClip[] explosions;
    private AudioSource explodeAudioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        explodeAudioSource = GetComponent<AudioSource>();
        Explode();
    }

    // Update is called once per frame

    private void Explode()
    {
        AudioClip explode = GetRandomClip();
        explodeAudioSource.PlayOneShot(explode);
    }
    
    private AudioClip GetRandomClip()
    {
        return explosions[Random.Range(0, explosions.Length)];
    }
}
