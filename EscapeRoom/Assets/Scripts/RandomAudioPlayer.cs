using System;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float pitchVariation = 0.1f;
    [SerializeField] bool onAwake = false;
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (onAwake) PlayRandomAudio();
    }

    public void PlayRandomAudio()
    {
        audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        audioSource.pitch = UnityEngine.Random.Range(1 - pitchVariation, 1 + pitchVariation);
        audioSource.Play();
    }
}

