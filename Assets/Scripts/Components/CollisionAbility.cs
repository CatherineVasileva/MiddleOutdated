using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class CollisionAbility : MonoBehaviour, IAbility, IConvertGameObjectToEntity
{
    public Collider Collider;
    [HideInInspector] public List<Collider> detectedCollisions;

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
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        float3 position = transform.position;
        switch (Collider)
        {
            case SphereCollider sphere:
                sphere.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Sphere,
                    SphereCenter = sphereCenter - position,
                    SphereRadius = sphereRadius,
                    initialTakeOff = true
                });
                break;
            case CapsuleCollider capsule:
                capsule.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd, out var capsuleRadius);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Capsule,
                    CapsuleStart = capsuleStart - position,
                    CapsuleEnd = capsuleEnd - position,
                    CapsuleRadius = capsuleRadius,
                    initialTakeOff = true
                });
                break;
            case BoxCollider box:
                box.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                dstManager.AddComponentData(entity, new ActorColliderData
                {
                    ColliderType = ColliderType.Box,
                    BoxCenter = boxCenter - position,
                    BoxHalfExtents = boxHalfExtents,
                    BoxOrientation = boxOrientation,
                    initialTakeOff = true
                });
                break;
        } 
        Collider.enabled = false;
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

public struct ActorColliderData : IComponentData
{
    public ColliderType ColliderType;
    public float3 SphereCenter;
    public float SphereRadius;
    public float3 CapsuleStart;
    public float3 CapsuleEnd;
    public float CapsuleRadius;
    public float3 BoxCenter;
    public float3 BoxHalfExtents;
    public quaternion BoxOrientation;
    public bool initialTakeOff;
}

public enum ColliderType
{
    Sphere,
    Capsule,
    Box
}