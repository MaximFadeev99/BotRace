using UnityEngine;

public interface IDirectionChanger
{
    public void Initialize(Transform veichleTransform);

    public void ChangeDirection(float newYRotation);

    public void ResetZRotation();
}