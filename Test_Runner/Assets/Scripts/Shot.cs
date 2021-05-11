using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shot : MonoBehaviour
{
    public GameObject explode;
    private GameObject newExplode;

    private void Start()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (!other.gameObject.CompareTag("obstacle")) return;
        print("obstacle");
        newExplode = Instantiate(explode, transform.position, transform.rotation);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
    
}
