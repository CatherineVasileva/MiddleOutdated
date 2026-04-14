using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBounceAbility : IAbility
{
    bool IsAbleToBounce { get; set; }
    void ReflectDirection(Vector3 direction);
}
