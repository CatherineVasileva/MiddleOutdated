using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
{
    public float Speed;
   public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData<InputData>(entity, new InputData());
        dstManager.AddComponentData<MoveData>(entity, new MoveData { speed = Speed });
    }   
}

public struct InputData : IComponentData
    {
        public float2 move;
    }

public struct MoveData : IComponentData
{
    public float speed;
}
