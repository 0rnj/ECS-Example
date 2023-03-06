using CodeBase.IdleTowerDefense.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile, UpdateAfter(typeof(WeaponFireSystem)), UpdateAfter(typeof(ProjectileCollisionSystem))]
    public partial struct KillSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);

            foreach (var (health, entity) in SystemAPI.Query<Health>().WithEntityAccess())
            {
                if (health.Value <= 0f)
                {
                    entityCommandBuffer.DestroyEntity(entity);
                }
            }
            
            state.Dependency.Complete();
            
            entityCommandBuffer.Playback(state.EntityManager);
            
            entityCommandBuffer.Dispose();
        }
    }
}