using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        //This allows the audio manager to keep playing throughout scenes, it also deletes the second one that occurs
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
            Play("Theme");
        
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name); // lets find sound in sound array where sound.name == name
        if (s == null)
        {
            Debug.Log("WARNING! Sound not Found :O");
            return;
        }
            
        s.source.Play();
    }



}
