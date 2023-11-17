using UnityEngine;

public interface IMover 
{
    public void Initialize(Transform vehicleTransform);

    public void PushForward();
}
