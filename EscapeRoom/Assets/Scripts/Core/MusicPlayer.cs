using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] List<AudioClip> tracks;
    [SerializeField] bool isShuffled = true;
    [SerializeField] bool isLooping = true;

    List<AudioClip> currentPlaylist;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        if (tracks != null && tracks.Count > 0) StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        currentPlaylist = tracks;

        while (currentPlaylist.Count > 0)
        {
            audioSource.Stop();

            int index = 0;

            if (isShuffled) index = UnityEngine.Random.Range(0, tracks.Count);

            AudioClip track = currentPlaylist[index];
            audioSource.clip = track;

            currentPlaylist.RemoveAt(index);
            if (currentPlaylist.Count < 1 && isLooping) currentPlaylist = tracks;

            audioSource.Play();

            yield return new WaitForSeconds(track.length);
        }
    }
}
