using Unity.Entities;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Components.Aspects
{
    public readonly partial struct SpawnerAspect : IAspect
    {
        private readonly RefRO<Spawner> _spawner;
        private readonly TransformAspect _transformAspect;
        private readonly RefRO<Index> _index;

        public Entity Prefab => _spawner.ValueRO.Prefab;
        public WorldTransform WorldTransform => _transformAspect.WorldTransform;
        public uint Index => _index.ValueRO.Value;
    }
}