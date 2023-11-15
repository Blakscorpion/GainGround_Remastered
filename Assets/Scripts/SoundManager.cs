using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance;
    public AudioClip DefaultMusicForThisLevel;
    public AudioSource AudioSourceForMusic;
    public AudioSource AudioSourceForSoundFX;
    public AudioSource AudioSourceForInteraction;
    // Start is called before the first frame update
    
    void Awake(){
        Instance = this;
    }

    void Start(){
        PlayMusic(DefaultMusicForThisLevel);
    }
    
    public void PlayMusic(AudioClip audio)
    {
        AudioSourceForMusic.PlayOneShot(audio);
    }
    
    public void PlaySoundFX(AudioClip audio)
    {
        AudioSourceForSoundFX.PlayOneShot(audio);
    }
    
    public void PlayInteractionSound(AudioClip audio)
    {
        AudioSourceForInteraction.PlayOneShot(audio);
    }
}
