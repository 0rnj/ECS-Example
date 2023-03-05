using CodeBase.IdleTowerDefense.Components;
using CodeBase.IdleTowerDefense.Components.Aspects;
using Unity.Burst;
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
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            foreach (var (transform, target, weapon) in SystemAPI
                         .Query<RefRO<WorldTransform>, RefRO<MovementTarget>, RefRW<Weapon>>())
            {
                if (weapon.ValueRO.NextFireTime > elapsedTime)
                {
                    continue;
                }

                var targetEntity = target.ValueRO.TargetEntity;
                var targetTransform = SystemAPI.GetComponent<WorldTransform>(targetEntity);
                var distanceSqr = math.distancesq(targetTransform.Position, transform.ValueRO.Position);
                var weaponDistanceSqr = weapon.ValueRO.Distance * weapon.ValueRO.Distance;
                var isInFireRange = weaponDistanceSqr >= distanceSqr;

                if (isInFireRange == false)
                {
                    continue;
                }

                if (weapon.ValueRO.IsRanged)
                {
                    // TODO: fire projectile
                    return;
                }

                weapon.ValueRW.NextFireTime = (float)(elapsedTime + weapon.ValueRO.FireRate);

                var health = state.EntityManager.GetComponentData<Health>(targetEntity);
                
                health.Value -= weapon.ValueRO.Damage;

                state.EntityManager.SetComponentData(targetEntity, health);
            }
        }
    }
}