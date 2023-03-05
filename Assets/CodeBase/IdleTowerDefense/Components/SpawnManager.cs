using Unity.Entities;

namespace CodeBase.IdleTowerDefense.Components
{
    public partial struct SpawnManager : IComponentData
    {
        public float SpawnPeriod;
        public double NextSpawnTime;
    }
}