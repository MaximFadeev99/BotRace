using UnityEngine;
using System;

[RequireComponent(typeof(InvincibilityHandler))]
[RequireComponent(typeof(Engine))]
[RequireComponent(typeof(IInputHandler))]
public class Hover : MonoBehaviour
{   
    private CollisionHandler _collisionHandler;

    public InvincibilityHandler InvincibilityHandler { get; private set; } 
    public Engine Engine { get; private set; }
    public Transform Transform { get; private set; }
    public IInputHandler InputHandler { get; private set; }   

    public Action<Hover> Stopped;

    private void Awake()
    {
        Transform = transform;
        InvincibilityHandler = GetComponent<InvincibilityHandler>();
        Engine = GetComponent<Engine>();
        InputHandler = GetComponent<IInputHandler>();
        _collisionHandler = new(this);
    }

    private void OnEnable() =>
        Engine.SpeedNullified += () => Stopped.Invoke(this);

    private void OnDisable() =>
        Engine.SpeedNullified -= () => Stopped.Invoke(this);

    private void OnTriggerEnter(Collider other) =>
        _collisionHandler.AnalyzeCollision(other);

    private void OnTriggerExit(Collider other) =>
        _collisionHandler.OnCollisionEnded();
   
    public void StartRacing() =>
        Engine.StartMovement();

    public void EndRacing() =>
        StartCoroutine(Engine.GraduallyDecreaseSpeed());      
}