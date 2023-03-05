using CodeBase.IdleTowerDefense.Components;
using CodeBase.IdleTowerDefense.Components.Aspects;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Systems
{
    [UpdateBefore(typeof(CompanionGameObjectUpdateSystem))]
    public partial struct EnemySpawnSystem : ISystem
    {
        private uint _spawnersCount;

        void ISystem.OnCreate(ref SystemState state)
        {
            var spawnersQuery = new EntityQueryBuilder(state.WorldUpdateAllocator)
                .WithAll<Spawner>()
                .Build(ref state);

            _spawnersCount = (uint)spawnersQuery.CalculateEntityCount();
        }

        void ISystem.OnDestroy(ref SystemState state) { }

        void ISystem.OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;
            
            var spawnersQuery = new EntityQueryBuilder(state.WorldUpdateAllocator)
                .WithAll<Spawner>()
                .Build(ref state);

            _spawnersCount = (uint)spawnersQuery.CalculateEntityCount();

            foreach (var spawnManager in SystemAPI.Query<RefRW<SpawnManager>>())
            {
                if (elapsedTime < spawnManager.ValueRO.NextSpawnTime)
                {
                    return;
                }

                spawnManager.ValueRW.NextSpawnTime = elapsedTime + spawnManager.ValueRO.SpawnPeriod; 
            }

            var random = Random.CreateFromIndex((uint)elapsedTime);
            var index = random.NextUInt(_spawnersCount);

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
                break;
            }
        }
    }
}