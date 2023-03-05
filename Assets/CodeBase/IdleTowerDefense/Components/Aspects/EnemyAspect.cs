using Unity.Entities;
using Unity.Transforms;

namespace CodeBase.IdleTowerDefense.Components.Aspects
{
    public readonly partial struct EnemyAspect : IAspect
    {
        public readonly Entity Entity;
        public readonly TransformAspect TransformAspect;
        public readonly RefRW<Health> Health;
        public readonly RefRO<Weapon> Weapon;
        public readonly RefRO<MovementSpeed> MovementSpeed;
        public readonly RefRO<MovementTarget> MovementTarget;
        private readonly RefRO<EnemyTag> _enemyTag;
    }
}