using System.Collections.Generic;
using UnityEngine;

public class WallCollisions : MonoBehaviour, IAbilityTarget
{
    public List<Collider> Targets { get; set; }
    public WallOrientation orientation;
    
    private Vector3 ChosenDirection => orientation == WallOrientation.Horizontal ? Vector3.forward : Vector3.right;

    public void Execute()
    {
        foreach (var target in Targets)
        { 
            if (target.TryGetComponent<IBounceAbility>(out var bounceAbility))
            {
                if (bounceAbility.IsAbleToBounce)
                {
                    bounceAbility.ReflectDirection(ChosenDirection);
                }
                else
                    bounceAbility.Execute();
            }
        }
    }
   
    public enum WallOrientation
    {
        Horizontal,
        Vertical
    }
}
