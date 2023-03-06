using CodeBase.IdleTowerDefense.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile]
    public partial struct WeaponFireSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            var entityCommandBuffer = new EntityCommandBuffer(Allocator.Temp);
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            foreach (var (transform, weaponTarget, weapon) in SystemAPI
                         .Query<WorldTransform, WeaponTarget, RefRW<Weapon>>())
            {
                var targetEntity = weaponTarget.TargetEntity;
                if (targetEntity == Entity.Null || weapon.ValueRO.NextFireTime > elapsedTime)
                {
                    continue;
                }

                var targetTransform = SystemAPI.GetComponent<WorldTransform>(targetEntity);
                var distanceSqr = math.distancesq(targetTransform.Position, transform.Position);
                var weaponDistanceSqr = weapon.ValueRO.Distance * weapon.ValueRO.Distance;
                var isInFireRange = weaponDistanceSqr >= distanceSqr;

                if (isInFireRange == false)
                {
                    continue;
                }

                weapon.ValueRW.NextFireTime = (float)(elapsedTime + weapon.ValueRO.FireRate);

                if (weapon.ValueRO.IsRanged)
                {
                    var direction = math.normalize(targetTransform.Position - transform.Position);
                    var targetPosition = direction * 999f;
                    var projectile = state.EntityManager.Instantiate(weapon.ValueRO.ProjectilePrefab);

                    state.EntityManager.SetComponentData(projectile, LocalTransform.FromPosition(transform.Position));
                    state.EntityManager.SetComponentData(projectile,
                        new MovementTarget { TargetWorldPosition = targetPosition });
                    state.EntityManager.SetComponentData(projectile, new Projectile { Damage = weapon.ValueRO.Damage });
                    return;
                }

                var health = state.EntityManager.GetComponentData<Health>(targetEntity);

                health.Value -= weapon.ValueRO.Damage;

                state.EntityManager.SetComponentData(targetEntity, health);
            }

            entityCommandBuffer.Playback(state.EntityManager);

            entityCommandBuffer.Dispose();
        }
    }
}