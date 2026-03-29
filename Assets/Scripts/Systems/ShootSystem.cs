using Unity.Entities;

public class ShootSystem : ComponentSystem
{
    private EntityQuery _entityQuery;

    protected override void OnCreate()
    {
        _entityQuery = GetEntityQuery(typeof(InputData), typeof(ShootData), typeof(UserInputData));
    }
    protected override void OnUpdate()
    {
        Entities.With(_entityQuery).ForEach((Entity entity, UserInputData inputData, ref InputData input) =>
        {
            if(input.shoot >0f && inputData._attackScript != null && inputData._attackScript is IAbility ability) { ability.Execute(); }
        });
    }
}
