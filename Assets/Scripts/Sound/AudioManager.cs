using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AudioManager;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;

        public bool loop;

        // Sliders in Inspector for volume and pitch
        [Range(0f, 1f)] public float volume = 1f;
        [Range(-3f, 3f)] public float pitch = 1f;

        [HideInInspector] public AudioSource source;
    }

    public List<Sound> sounds;

    private void Start()
    {
        Play("ShootArrow");
        Debug.Log("Entering Start method ");
    }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.loop = sound.loop;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
        }
    }

    public void Play(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            sound.source.Play();
            Debug.Log("Attempting to play sound: " + name);
        }
        else
        {
            Debug.LogWarning("-----Sound: " + name + " not found!-----");
        }
    }

    public void Pause(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            sound.source.Pause();
        }
        else
        {
            Debug.LogWarning("-----Sound: " + name + " not found!-----");
        }
    }

    public void Stop(string name)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            sound.source.Stop();
        }
        else
        {
            Debug.LogWarning("-----Sound: " + name + " not found!-----");
        }
    }

    public void SetVolume(string name, float volume)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            sound.source.volume = volume;
        }
        else
        {
            Debug.LogWarning("-----Sound: " + name + " not found!-----");
        }
    }

    public void SetPitch(string name, float pitch)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            sound.source.pitch = pitch;
        }
        else
        {
            Debug.LogWarning("-----Sound: " + name + " not found!-----");
        }
    }

    public void SetLoop(string name, bool loop)
    {
        Sound sound = sounds.Find(s => s.name == name);
        if (sound != null)
        {
            sound.source.loop = loop;
        }
        else
        {
            Debug.LogWarning("-----Sound: " + name + " not found!-----");
        }
    }

}
