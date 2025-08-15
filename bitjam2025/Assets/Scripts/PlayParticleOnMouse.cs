using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticleOnMouse : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _particleSystem.Play();
        }
    }
}
