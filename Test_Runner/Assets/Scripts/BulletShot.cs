using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletShot : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shot;
    [SerializeField] private AudioClip emptyAmmo;
    [SerializeField] private GameObject projectile;
    private GameObject newProjectile;
    [SerializeField] private ParticleSystem gunExplode;
    [SerializeField] private float fireDelta = 0.5F;
    [SerializeField] private float power;
    private float nextFire = 0.5F;
    private float myTime = 0.5F;
    
    public int bullets;
    
    // Start is called before the first frame update
    void Start()
    {
        // audioSource.volume = 0.3f;
        animator = GetComponentInParent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Destroy(newProjectile,1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myTime = myTime + Time.deltaTime;
        bullets = PlayerPrefs.GetInt("bullets");
        
        
        //if ammo is empty
        if (bullets == 0 && SwipeController.tap && myTime > nextFire)
        {
            audioSource.volume = 0.3f;        
            audioSource.PlayOneShot(emptyAmmo);
        }
        //Player shooting
        if (SwipeController.tap && myTime > nextFire && bullets > 0 && PlayerPrefs.GetInt("health") > 0)
        {
            bullets -= 1;
            PlayerPrefs.SetInt("bullets", bullets);
            animator.SetTrigger("Attack");
            gunExplode.Play();
            audioSource.volume = 0.5f;        
            audioSource.pitch = Random.Range(0.9f, 1.1f);
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
