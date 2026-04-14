using System.Collections.Generic;
using UnityEngine;

public class BulletBouncePerk : MonoBehaviour, IAbilityTarget
{
    public List<Collider> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.gameObject.TryGetComponent<IPerkHandler>(out var perkHandler))
            {
                perkHandler.ActivatePerk();
                gameObject.SetActive(false);
                Destroy(gameObject);
                break;
            }
            else
                Debug.Log("ShootAbility isn't found");
        }
    }
}
