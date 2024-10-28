using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public AudioClip BattleMusic;
    public AudioClip PeaceMusic;

    private AudioSource audioSource;

    private void Awake()
    {
        // Attach or find AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayBattleMusic()
    {
        if (BattleMusic != null)
        {
            audioSource.clip = BattleMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("BattleMusic clip is not assigned!");
        }
    }

    public void PlayPeaceMusic()
    {
        if (PeaceMusic != null)
        {
            audioSource.clip = PeaceMusic;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("PeaceMusic clip is not assigned!");
        }
    }

    public void StopMusic()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
