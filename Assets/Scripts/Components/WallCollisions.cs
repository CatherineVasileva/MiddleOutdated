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
            if (target.TryGetComponent<BulletData>(out var bullet))
            {
                if (bullet.isAbleToBounce) 
                {
                    var direction = Vector3.Reflect(target.transform.forward.normalized, ChosenDirection.normalized);
                    target.transform.forward = direction;
                }
                else 
                    Destroy(bullet.gameObject);
            }
            else
                Debug.Log("not a bullet");
        }
    }
   
    public enum WallOrientation
    {
        Horizontal,
        Vertical
    }
}
