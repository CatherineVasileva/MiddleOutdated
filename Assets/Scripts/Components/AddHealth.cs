using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class AddHealth : MonoBehaviour, IAbilityTarget
{
    public List<Collider> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.gameObject.TryGetComponent<GameObjectEntity>(out var goEntity))
            {
                Entity entity = goEntity.Entity;
                Debug.Log(entity);
            }
            else
            { Debug.Log("Содержимое списка: " + string.Join(", ", Targets)); }
        }

        Debug.Log("Health added, it must be destroyed");
    }
}
