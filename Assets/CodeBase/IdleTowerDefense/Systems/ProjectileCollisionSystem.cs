using CodeBase.IdleTowerDefense.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile, UpdateAfter(typeof(WeaponFireSystem))]
    public partial struct ProjectileCollisionSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            state.Dependency = new CollisionJob
            {
                Projectiles = SystemAPI.GetComponentLookup<Projectile>(),
                Enemies = SystemAPI.GetComponentLookup<EnemyTag>(),
                Healths = SystemAPI.GetComponentLookup<Health>()
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);

            state.Dependency.Complete();
        }

        private struct CollisionJob : ITriggerEventsJob
        {
            public ComponentLookup<Projectile> Projectiles;
            public ComponentLookup<EnemyTag> Enemies;
            public ComponentLookup<Health> Healths;

            public void Execute(TriggerEvent triggerEvent)
            {
                if (TryDamage(projectileEntity: triggerEvent.EntityA, enemyEntity: triggerEvent.EntityB) == false)
                {
                    TryDamage(projectileEntity: triggerEvent.EntityB, enemyEntity: triggerEvent.EntityA);
                }
            }

            private bool TryDamage(Entity projectileEntity, Entity enemyEntity)
            {
                if (Projectiles.HasComponent(projectileEntity) == false)
                {
                    return false;
                }

                if (Enemies.HasComponent(enemyEntity) == false || Healths.HasComponent(enemyEntity) == false)
                {
                    return false;
                }

                var projectile = Projectiles[projectileEntity];
                var health = Healths[enemyEntity];

                health.Value -= projectile.Damage;
                Healths[enemyEntity] = health;

                projectile.Damage = 0f;
                Projectiles[projectileEntity] = projectile;

                return true;
            }
        }
    }
}