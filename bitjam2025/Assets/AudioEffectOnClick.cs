using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectOnClick : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Vector2 minMaxPitchVariation;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource.pitch = Random.Range(minMaxPitchVariation.x, minMaxPitchVariation.y);
            audioSource.Play();
        }
    }
}
