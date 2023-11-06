using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Hover : MonoBehaviour
{
    [SerializeField] private float _invincibilityTime;

    private CollisionHandler _collisionHandler;
    private Timer _invincibilityTimer = new();

    public Engine Engine { get; private set; }
    public Transform Transform { get; private set; }
    public IInputHandler InputHandler { get; private set; }
    public bool IsInvincible {get; private set;}

    //класс получает сигнал от RaceManager и говорит двигателю двигать машину вперед

    private void Awake()
    {
        Transform = transform;
        Engine = GetComponent<Engine>();
        InputHandler = GetComponent<IInputHandler>();
        _collisionHandler = new(this);
    }

    private void Update()
    {
        if (_invincibilityTimer.IsActive)
            _invincibilityTimer.Tick();
    }

    private void OnTriggerEnter(Collider other)
    {
        _collisionHandler.AnalyzeCollision(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _collisionHandler.OnCollisionEnded();
    }   

    private void DeactivateInvincibility() 
    {
        IsInvincible = false;
        _invincibilityTimer.TimeIsUp -= DeactivateInvincibility;
    }

    public void ActivateInvincibility()
    {
        if (IsInvincible)
        {
            _invincibilityTimer.Start(_invincibilityTime);
            return;
        }

        IsInvincible = true;
        _invincibilityTimer.Start(_invincibilityTime);
        _invincibilityTimer.TimeIsUp += DeactivateInvincibility;
    }

    public void StartRacing() 
    {
        Engine.StartMovement();
    }
}
