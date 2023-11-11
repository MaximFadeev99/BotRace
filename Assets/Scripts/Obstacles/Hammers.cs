using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammers : MonoBehaviour
{
    [SerializeField] private float _timeBetweenCharges;
    [SerializeField] private float _holdTime;
    [SerializeField] private float _movementSpeed = 1f;

    private Animator _animator;
    private AudioSource _audioSource;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _animator.speed = _movementSpeed;
    }

    private void Start()
    {
        StartCoroutine(Work());
    }

    private IEnumerator Work()
    {
        while (true)
        {
            var timeBetweenCharges = new WaitForSeconds(_timeBetweenCharges);
            var holdTime = new WaitForSeconds(_holdTime);
            var animationEndTime = new WaitForSeconds(1f / _movementSpeed);
            yield return timeBetweenCharges;
            _animator.SetTrigger("isCharging");
            _audioSource.Play();
            yield return holdTime;
            _animator.SetTrigger("isBouncingBack");
            yield return animationEndTime;
        }
    }
}
