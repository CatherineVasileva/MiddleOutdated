using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBouncePerk : MonoBehaviour, IAbilityTarget
{
    public List<Collider> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.gameObject.TryGetComponent<ShootAbility>(out var shootAbility))
            {
                shootAbility.BulBouncePerkStart = Time.time;
                Destroy(gameObject);
            }
            else
                Debug.Log("ShootAbility isn't found");
        }
    }
}
