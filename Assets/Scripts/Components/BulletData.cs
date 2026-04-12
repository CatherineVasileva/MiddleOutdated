using Unity.Entities;
using UnityEngine;

public class BulletData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
    public bool isAbleToBounce = false;
    public float TimeToDestroy;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveData { speed = Speed }) ;
        dstManager.AddComponentData(entity, new FreeFlight());
    }

    public void Update()
    {
        TimeToDestroy -= Time.deltaTime;
        if(TimeToDestroy <= 0 )
        {
            Destroy(gameObject);
        }
    }
}

public struct FreeFlight : IComponentData
    {

    }

