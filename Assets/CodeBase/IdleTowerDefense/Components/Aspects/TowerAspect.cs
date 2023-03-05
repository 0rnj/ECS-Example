using Unity.Entities;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Components.Aspects
{
    public readonly partial struct TowerAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;
        public readonly RefRW<Health> Health;
        public readonly RefRW<Weapon> Weapon;
        private readonly RefRO<TowerTag> _towerTag;
    }
}