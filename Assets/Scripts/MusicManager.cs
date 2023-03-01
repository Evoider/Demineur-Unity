using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    [SerializeField] AudioClip[] musicClips;
    AudioSource musicSource;
    float volume;
    // Start is called before the first frame update
    void Start()
    {
        
        musicSource= GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        musicSource.volume = FindObjectOfType<Parameter>().musicVolume;

        if (!musicSource.isPlaying)
        {
            StartMusic();
        }
    }

    void StartMusic()
    {
        musicSource.clip= musicClips[Random.Range(0,musicClips.Length)];
        musicSource.Play();
    }
}
