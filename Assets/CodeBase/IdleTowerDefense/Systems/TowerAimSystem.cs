using CodeBase.IdleTowerDefense.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile]
    public partial struct TowerAimSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            foreach (var (_, weaponTarget, towerTransform) in SystemAPI
                         .Query<TowerTag, RefRW<WeaponTarget>, WorldTransform>())
            {
                var closestEntity = Entity.Null;
                var closestDistanceSqr = float.MaxValue;

                foreach (var (_, enemyTransform, enemyEntity) in SystemAPI
                             .Query<RefRO<EnemyTag>, WorldTransform>().WithEntityAccess())
                {
                    var distanceSqr = math.distancesq(enemyTransform.Position, towerTransform.Position);

                    if (closestEntity != Entity.Null && distanceSqr >= closestDistanceSqr)
                    {
                        continue;
                    }

                    closestEntity = enemyEntity;
                    closestDistanceSqr = distanceSqr;
                }

                weaponTarget.ValueRW.TargetEntity = closestEntity;
            }
        }
    }
}