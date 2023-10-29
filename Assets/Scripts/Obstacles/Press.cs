using System.Collections;
using UnityEngine;

public class Press : MonoBehaviour
{
    [SerializeField] private float _timeBetweenCharges;
    [SerializeField] private float _holdTime;
    [SerializeField] private float _movementSpeed = 1f;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
            yield return holdTime;
            _animator.SetTrigger("isBouncingBack");
            yield return animationEndTime;
        }       
    }

}
