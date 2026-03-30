using Unity.Entities;
using UnityEngine;

public class CharacterMoveSystem : ComponentSystem
{
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
       _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<MoveData>(), typeof(Transform));
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach((Entity entity, Transform transform, ref InputData inputData,ref MoveData moveData) =>
        {
            var pos = transform.position;
            var direction = new Vector3(inputData.move.x, 0, inputData.move.y);
            pos += direction * moveData.speed;
            transform.position = pos;
            if(direction != Vector3.zero)
            {
                transform.forward = direction;
            }
        });
    }
}
