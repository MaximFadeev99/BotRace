using UnityEngine;

public interface IDirectionChanger
{
    public void Initialize(Transform vehicleTransform);

    public void ChangeDirection(float newYRotation);

    public void ResetZRotation();
}