using Unity.Entities;
using UnityEngine;

public class BulletData : MonoBehaviour, IConvertGameObjectToEntity, IBounceAbility
{
    [SerializeField] float Speed;
    private float TimeToDestroy;

    public bool IsAbleToBounce { get; set; } = false;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveData { speed = Speed }) ;
        dstManager.AddComponentData(entity, new FreeFlight());
    }

    public void Execute()
    {
        Destroy(gameObject);
    }

    public void ReflectDirection(Vector3 chosenDirection)
    {
        var direction = Vector3.Reflect(transform.forward.normalized, chosenDirection.normalized);
        transform.forward = direction;
        Debug.Log("ReflectDirection");
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

