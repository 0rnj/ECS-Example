using CodeBase.IdleTowerDefense.Components;
using CodeBase.IdleTowerDefense.Components.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile, CreateAfter(typeof(EnemySpawnSystem))]
    public partial struct EnemyMovementSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        [BurstCompile]
        void ISystem.OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, movementTarget, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MovementTarget>, RefRO<MovementSpeed>>())
            {
                var pos = SystemAPI.GetComponent<WorldTransform>(movementTarget.ValueRO.TargetEntity);
                
                var direction = math.normalize(pos.Position - localTransform.ValueRO.Position);

                localTransform.ValueRW.Position += direction * speed.ValueRO.Value * deltaTime;
            }
        }
    }
}