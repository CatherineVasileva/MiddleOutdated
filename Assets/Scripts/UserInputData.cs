using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public MonoBehaviour _attackScript;
    public float Speed;
    public float JerkSpeed;
    public float JerkDuration;
    public float TimeToJerkAgain;
   // public float Health;

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
            canJerk = true,
            timeToJerkAgain = TimeToJerkAgain
        });
       //dstManager.AddComponentData(entity, new PlayerHealth { health = Health });
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
    public float timerJerkDuration;
    public bool canJerk;
    public float timeToJerkAgain;
    public float timerBetweenJerks;
}

public struct PlayerHealth : IComponentData
{
    public float health;
}
