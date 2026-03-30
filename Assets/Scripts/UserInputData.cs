using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public MonoBehaviour _attackScript;
    public float Speed;
    public float JerkSpeed;
    public float JerkDuration;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
        dstManager.AddComponentData(entity, new MoveData { speed = Speed });
        if (_attackScript != null && _attackScript is IAbility)
        {
            dstManager.AddComponentData(entity, new ShootData());
        }
        dstManager.AddComponentData(entity, new JerkData
        {
            jerkSpeed = JerkSpeed,
            jerkDuration = JerkDuration,
            canJerk = true
        });
    }   
}

public struct InputData : IComponentData
    {
        public float2 move;
        public float shoot;
        public float jerk;
    }

public struct MoveData : IComponentData
{
    public float speed;
}

public struct ShootData : IComponentData
{

}

public struct JerkData : IComponentData
{
    public float jerkSpeed;
    public float jerkDuration;
    public float timer;
    public bool canJerk;
}
