using UnityEngine;

public interface ILevitator
{
    public void Initialize(Transform vehicleTransform);

    public void StartLevitation();

    public void StopLevitation();
}