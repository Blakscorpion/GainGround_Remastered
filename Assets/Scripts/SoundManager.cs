using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    public static SoundManager Instance;
    public AudioSource source;
    // Start is called before the first frame update
    
    void Awake()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        
    }
    
    public void PlaySound(AudioClip audio)
    {
        source.PlayOneShot(audio);
    }
}
