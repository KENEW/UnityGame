using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManaga : MonoBehaviour
{
    public AudioSource musicPlayer;
    public AudioClip background;
    //public AudioClip 
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        playSound(background, musicPlayer);
    }

    void Update()
    {
        
    }
    private void playSound(AudioClip clip, AudioSource audioPlayer)
    {
        audioPlayer.Stop();
        audioPlayer.clip = clip;
        audioPlayer.loop = true;
        audioPlayer.time = 0;
        audioPlayer.Play();
    }
}
