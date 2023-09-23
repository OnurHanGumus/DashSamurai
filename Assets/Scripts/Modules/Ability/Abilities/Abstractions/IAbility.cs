using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Activated();
    void Deactivated();
    string GetName();
    float GetDuration();
}
