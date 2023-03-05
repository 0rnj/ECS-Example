using Unity.Entities;

namespace CodeBase.IdleTowerDefense.Components
{
    public partial struct Spawner : IComponentData
    {
        public Entity Prefab;
    }
}