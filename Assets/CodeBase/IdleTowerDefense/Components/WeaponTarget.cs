using Unity.Entities;
using Unity.Mathematics;

namespace CodeBase.IdleTowerDefense.Components
{
    public struct WeaponTarget : IComponentData
    {
        public Entity TargetEntity;
        public float3 TargetWorldPosition;
    }
}