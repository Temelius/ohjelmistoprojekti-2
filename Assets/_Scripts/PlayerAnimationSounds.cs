using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationSounds : MonoBehaviour
{

    public AudioSource runSound1;
    public AudioSource runSound2;
    public AudioSource jumpSound;

    void Start()
    {
        AudioSource[] audio = GetComponents<AudioSource>();
        runSound1 = audio[0];
        runSound2 = audio[1];
        jumpSound = audio[2];
    }

    void Update()
    {
     
    }

    private void PlayerStepSound()
    {
        runSound1.Play();
        
    }

    private void PlayerStepSoundTwo()
    {
        runSound2.Play();
      
    }

    private void PlayerJumpSound()
    {
        jumpSound.Play();
      
    }


    private void StopSounds()
    {
        jumpSound.Stop();
        runSound1.Stop();
        runSound2.Stop();

    }



}
