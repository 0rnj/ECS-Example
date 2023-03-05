using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class WeaponAuthoring : MonoBehaviour
    {
        public float Damage;
        public float Distance;
        public float FireRate;
        public bool IsRanged;
        public float AreaOfDamage;

        class Baker : Baker<WeaponAuthoring>
        {
            public override void Bake(WeaponAuthoring authoring)
            {
                AddComponent(new Weapon
                {
                    Damage = authoring.Damage,
                    Distance = authoring.Distance,
                    FireRate = authoring.FireRate,
                    IsRanged = authoring.IsRanged,
                    AreaOfDamage = authoring.AreaOfDamage,
                });
            }
        }
    }
}