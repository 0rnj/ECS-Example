using CodeBase.IdleTowerDefense.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile, UpdateAfter(typeof(ProjectileCollisionSystem))]
    public partial struct ProjectileDestructionSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.TempJob);

            foreach (var (projectile, entity) in SystemAPI.Query<Projectile>().WithEntityAccess())
            {
                if (projectile.Damage <= 0f)
                {
                    entityCommandBuffer.DestroyEntity(entity);
                }
            }
            
            entityCommandBuffer.Playback(state.EntityManager);
            
            entityCommandBuffer.Dispose();
        }
    }
}