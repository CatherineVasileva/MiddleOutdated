using Unity.Entities;
using UnityEngine;

public class BulletMoveSystem : ComponentSystem
{
    protected EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<FreeFlight>(), typeof(Transform));
    }
    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Transform transform,ref MoveData moveData) =>
        {
            transform.Translate(Vector3.forward * moveData.speed * Time.DeltaTime);
        });
    }
}
