using UnityEngine;

public class JumpingBalls : MonoBehaviour
{
    [SerializeField] private Ball _ball1;
    [SerializeField] private Ball _ball2;
    [SerializeField] private Vector3 _pinnedDownSize;
    [SerializeField] private float _initialHeight;
    [SerializeField] private float _jumpingSpeed;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _transform.position = new Vector3(_transform.position.x, _initialHeight, _transform.position.z);
    }

    private void OnEnable() =>
        _ball1.TouchedGround += ReleaseSecondBall;

    private void OnDisable() =>
        _ball1.TouchedGround -= ReleaseSecondBall;

    private void Start()
    {
        _ball1.Initiate(_jumpingSpeed, _pinnedDownSize);
        _ball2.Initiate(_jumpingSpeed, _pinnedDownSize);
        _ball1.ActivateGravity();
    }

    private void ReleaseSecondBall() 
    {
        _ball2.ActivateGravity();
        _ball1.TouchedGround -= ReleaseSecondBall;
    }
}