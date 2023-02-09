using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    // array of gameObjects spawned at runtime which contain audiosource components
    [SerializeField]
    private GameObject[] audioPlayers = new GameObject[6];

    // array to hold respective audiosource components of gameobjects
    private AudioSource[] audioSources = new AudioSource[6];

    [SerializeField]
    private GameObject musicSlider;


    // Start is called before the first frame update
    void Start()
    {
        audioPlayers[0] = GameObject.Find("AudioOnMap 1(Clone)");
        audioPlayers[1] = GameObject.Find("AudioOnMap 2(Clone)");
        audioPlayers[2] = GameObject.Find("AudioOnMap 3(Clone)");
        audioPlayers[3] = GameObject.Find("AudioOnMap(Clone)");
        audioPlayers[4] = GameObject.Find("PuzzleRoomAudio");
        audioPlayers[5] = GameObject.Find("PuzzleRoomAudio");

        audioSources[0] = audioPlayers[0].GetComponent<AudioSource>();
        audioSources[1] = audioPlayers[1].GetComponent<AudioSource>();
        audioSources[2] = audioPlayers[2].GetComponent<AudioSource>();
        audioSources[3] = audioPlayers[3].GetComponent<AudioSource>();
        audioSources[4] = audioPlayers[4].GetComponent<AudioSource>();
        audioSources[5] = audioPlayers[5].GetComponent<AudioSource>();

        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 6; i++) 
        {
            audioSources[i].volume = musicSlider.GetComponent<Slider>().value;
        }
    }
}
