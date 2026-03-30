using UnityEngine;
using Unity.Entities;

public class JerkSystem : ComponentSystem
{
    protected EntityQuery _query;

    protected override void OnCreate()
    {
        _query = GetEntityQuery(typeof(JerkData), typeof(Transform), typeof(InputData));
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Transform transform, ref JerkData jerkData,ref InputData input) =>
        {
            if(input.jerk > 0 && jerkData.canJerk && jerkData.timer <= 0)
            {
                jerkData.timer = jerkData.jerkDuration;
                jerkData.canJerk = false; 
            }

            if(input.jerk <= 0)
            {
                jerkData.canJerk = true;
            }

            if (jerkData.timer > 0)
            {
                transform.Translate(Vector3.forward * jerkData.jerkSpeed * Time.DeltaTime);
                jerkData.timer -= Time.DeltaTime;
                    Debug.Log("jerk is performing");
            }
        });
    }
}
