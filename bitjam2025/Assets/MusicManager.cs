using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private List<AudioClip> musicTracks = new List<AudioClip>();
    [SerializeField] private Vector2 minMaxPauseBetweenMusicTracks;

    private int currentIndex;

    private void Start()
    {
        currentIndex = -1;
        PlayNewTrack();
    }

    private void PlayNewTrack()
    {
        currentIndex++;
        currentIndex %= musicTracks.Count;

        audioSource.clip = musicTracks[currentIndex];
        audioSource.Play();

        Invoke(nameof(MakePause), musicTracks[currentIndex].length);
    }

    private void MakePause()
    {
        Invoke(nameof(PlayNewTrack), Random.Range(minMaxPauseBetweenMusicTracks.x, minMaxPauseBetweenMusicTracks.y));
    }
}
