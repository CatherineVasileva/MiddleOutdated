using UnityEngine;
using Unity.Entities;

public class JerkSystem : ComponentSystem
{
    protected EntityQuery _query;
    protected override void OnCreate()
    {
        _query = GetEntityQuery(ComponentType.ReadOnly<JerkData>(), typeof(Transform), ComponentType.ReadOnly<InputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Transform transform, ref JerkData jerkData,ref InputData input) =>
        {
            if(input.jerk > 0 && jerkData.timer <= 0)
            {
                jerkData.timer = jerkData.jerkDuration;
            }
            if(jerkData.timer > 0)
            {
                transform.Translate(Vector3.forward * jerkData.jerkSpeed * Time.DeltaTime);Debug.Log("jerk is performing");
                jerkData.timer -= Time.DeltaTime;
            }
        });
    }
}
