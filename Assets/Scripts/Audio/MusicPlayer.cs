using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] clips;
    private AudioSource audioSource;
    public static MusicPlayer instance;

    void Start() {
        audioSource = FindObjectOfType<AudioSource>();
        audioSource.loop = false;
    }

    private AudioClip GetRandomClip() {
        return clips[Random.Range(0, clips.Length)];
    }

    void Update() {
        if (!audioSource.isPlaying) 
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();

        }
    }

    private void Awake() 
    {
        if (instance == null) 
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
}
