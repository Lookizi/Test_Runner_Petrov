using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepsScript : MonoBehaviour
{
    // private CharacterController characterController;
    private Animator animator;
    [SerializeField] private AudioClip[] footsteps;
    [SerializeField] private AudioSource footstepsAudioSource;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioSource jumpAudioSource;
    [SerializeField] private AudioClip scream;
    [SerializeField] private AudioClip bodyFall;
    [SerializeField] private AudioSource deathAudioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Step()
    {
        AudioClip clip = GetRandomClip();
        footstepsAudioSource.PlayOneShot(clip);
    }

    private void JumpSound()
    {
        jumpAudioSource.PlayOneShot(jump);
    }

    private void BodyFall()
    {
        deathAudioSource.PlayOneShot(bodyFall);
    }

    private void Scream()
    {
        deathAudioSource.PlayOneShot(scream);
    }

    private AudioClip GetRandomClip()
    {
        return footsteps[Random.Range(0, footsteps.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
