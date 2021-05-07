using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShot : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip shot;
    public GameObject projectile;
    private GameObject newProjectile;
    public ParticleSystem gunExplode;
    public float fireDelta = 0.5F;
    public float power;
    private float nextFire = 0.5F;
    private float myTime = 0.0F;
    
    public int bullets;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Destroy(newProjectile,1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myTime = myTime + Time.deltaTime;
        bullets = PlayerPrefs.GetInt("bullets");

        if (SwipeController.tap && myTime > nextFire && bullets > 0 && PlayerPrefs.GetInt("health") > 0)
        {
            bullets -= 1;
            PlayerPrefs.SetInt("bullets", bullets);
            animator.SetTrigger("Attack");
            gunExplode.Play();
            audioSource.PlayOneShot(shot);
            nextFire = myTime + fireDelta;
            var rotation = Quaternion.Euler (0,90,0);
            newProjectile = Instantiate(projectile, transform.position,rotation) as GameObject;
            newProjectile.GetComponent<Rigidbody>().AddForce(Vector3.forward * power);
            
            nextFire = nextFire - myTime;
            myTime = 0.0F;
            
        }
    }
}
