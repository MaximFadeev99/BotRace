using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ParticleSoundSystem : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;
    private int _currentParticleCount = 0;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update() 
    {
        if (_particleSystem.particleCount > _currentParticleCount)
            _audioSource.Play();

        _currentParticleCount = _particleSystem.particleCount;
    }
}
