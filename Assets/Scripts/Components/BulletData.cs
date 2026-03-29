using Unity.Entities;
using UnityEngine;

public class BulletData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveData { speed = Speed }) ;
        dstManager.AddComponentData(entity, new FreeFlight());
    } 
}

public struct FreeFlight : IComponentData
    {

    }
