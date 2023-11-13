using UnityEngine;

public interface IMover 
{
    public void Initialize(Transform veichleTransform);

    public void PushForward();
}
