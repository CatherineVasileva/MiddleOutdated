using System.Collections.Generic;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IAbility
{
    private List<Collider> detectedCollisions = new List<Collider> ();

    public List<MonoBehaviour> collisionActions;
    private List<IAbilityTarget> collisionActionsAbilities = new List<IAbilityTarget>();

    public void Start()
    {
        foreach(var action in collisionActions)
        {
            if (action is IAbilityTarget ability)
            {
                collisionActionsAbilities.Add(ability);
            }
            else
                Debug.LogError("Monobehaviour in coliisionActions must derive from IAbillityTarget");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        detectedCollisions.Add(collision.collider);
        Execute();
    }

    public void Execute()
    {
        foreach(var action in collisionActionsAbilities)
        {
            action.Targets = detectedCollisions;
            action.Execute();
        }
    }
}