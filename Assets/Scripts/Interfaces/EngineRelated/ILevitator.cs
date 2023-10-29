using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevitator
{
    public void Initialize(Transform veichleTransform);
    public void StartLevitation();
    public void StopLevitation();
}
