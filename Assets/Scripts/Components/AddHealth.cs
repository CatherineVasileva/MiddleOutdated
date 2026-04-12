using System.Collections.Generic;
using UnityEngine;

public class AddHealth : MonoBehaviour, IAbilityTarget
{
    [SerializeField] float Damage;
    public List<Collider> Targets { get; set; }

    public void Execute()
    {
        foreach (var target in Targets)
        {
            if (target.gameObject.TryGetComponent<CharacterHealth>(out var targetHealth))
            {
                targetHealth.Health -= Damage;
                Destroy(gameObject);
            }
            else
                Debug.Log("CharacterHealth isn't found");
        }
    }
}
