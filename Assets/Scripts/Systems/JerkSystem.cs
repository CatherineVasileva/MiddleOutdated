using UnityEngine;
using Unity.Entities;
using System.Threading;

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
            if(jerkData.timerBetweenJerks > 0)
            {
                jerkData.timerBetweenJerks -= Time.DeltaTime;
            }

            if(input.jerk > 0 && jerkData.canJerk && jerkData.timerJerkDuration <= 0 && jerkData.timerBetweenJerks <=0)
            {
                jerkData.timerJerkDuration = jerkData.jerkDuration;
                jerkData.timerBetweenJerks = jerkData.timeToJerkAgain;
                jerkData.canJerk = false; 
            }

            if(input.jerk <= 0)
            {
                jerkData.canJerk = true;
            }

            if (jerkData.timerJerkDuration > 0)
            {
                transform.Translate(Vector3.forward * jerkData.jerkSpeed * Time.DeltaTime);
                jerkData.timerJerkDuration -= Time.DeltaTime;
                    Debug.Log("jerk is performing");
            }
        });
    }
}
