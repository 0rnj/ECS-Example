using CodeBase.IdleTowerDefense.Components;
using CodeBase.IdleTowerDefense.Components.Aspects;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Systems
{
    [BurstCompile, UpdateBefore(typeof(CompanionGameObjectUpdateSystem))]
    public partial struct EnemySpawnSystem : ISystem
    {
        void ISystem.OnCreate(ref SystemState state) { }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            var spawnersQuery = new EntityQueryBuilder(state.WorldUpdateAllocator)
                .WithAll<Spawner>()
                .Build(ref state);

            var spawnersCount = (uint)spawnersQuery.CalculateEntityCount();

            foreach (var spawnManager in SystemAPI.Query<RefRW<SpawnManager>>())
            {
                if (elapsedTime < spawnManager.ValueRO.NextSpawnTime)
                {
                    return;
                }

                spawnManager.ValueRW.NextSpawnTime = elapsedTime + spawnManager.ValueRO.SpawnPeriod;
            }

            var towerEntity = GetTowerEntity(ref state);

            var random = Random.CreateFromIndex((uint)(elapsedTime * 956216814));
            var index = random.NextUInt(spawnersCount);

            SpawnEnemy(ref state, index, towerEntity);
        }

        private void SpawnEnemy(ref SystemState state, uint index, Entity movementTarget)
        {
            foreach (var spawnerAspect in SystemAPI.Query<SpawnerAspect>())
            {
                if (spawnerAspect.Index != index)
                {
                    continue;
                }

                var position = spawnerAspect.WorldTransform.Position;
                var entity = state.EntityManager.Instantiate(spawnerAspect.Prefab);
                var localTransform = LocalTransform.FromPosition(position);

                state.EntityManager.SetComponentData(entity, localTransform);
                state.EntityManager.SetComponentData(entity, new MovementTarget { TargetEntity = movementTarget });
                state.EntityManager.SetComponentData(entity, new WeaponTarget { TargetEntity = movementTarget });
                break;
            }
        }

        private Entity GetTowerEntity(ref SystemState state)
        {
            var towerEntity = Entity.Null;

            foreach (var towerAspect in SystemAPI.Query<TowerAspect>())
            {
                towerEntity = towerAspect.Entity;
                break;
            }

            return towerEntity;
        }
    }
}