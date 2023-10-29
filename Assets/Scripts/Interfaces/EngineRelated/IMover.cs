using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover 
{
    public void Initialize(Transform veichleTransform);
    public void PushForward();
}
