using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shot : MonoBehaviour
{
    public GameObject explode;
    private GameObject newExplode;
    private AudioSource audioSource;
    [SerializeField] private AudioClip explodes;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (!other.gameObject.CompareTag("obstacle")) return;
        print("obstacle");
        newExplode = Instantiate(explode, transform.position, transform.rotation);
        audioSource.PlayOneShot(explodes);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    
}
