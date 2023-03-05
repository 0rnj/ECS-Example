using Unity.Entities;

namespace CodeBase.SpawnerExample
{
    public class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            AddComponent(new Spawner
            {
                Prefab = GetEntity(authoring.Prefab),
                SpawnPosition = authoring.transform.position,
                NextSpawnTime = 0f,
                SpawnRate = authoring.SpawnRate
            });
        }
    }
}