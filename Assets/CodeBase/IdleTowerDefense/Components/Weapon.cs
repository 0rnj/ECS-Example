using Unity.Entities;

namespace CodeBase.IdleTowerDefense.Components
{
    public struct Weapon : IComponentData
    {
        public float Damage;
        public float FireRate;
        public float Distance;
        public float AreaOfDamage;
        public bool IsRanged;
        
        public float NextFireTime;
    }
}