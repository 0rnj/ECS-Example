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
            var towerPosition = default(float3);

            foreach (var towerAspect in SystemAPI.Query<TowerAspect>())
            {
                towerPosition = towerAspect.TransformAspect.WorldPosition;
                break;
            }

            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (localTransform, speed) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MovementSpeed>>())
            {
                var direction = math.normalize(towerPosition - localTransform.ValueRO.Position);

                localTransform.ValueRW.Position += direction * speed.ValueRO.Value * deltaTime;
            }
        }
    }
}