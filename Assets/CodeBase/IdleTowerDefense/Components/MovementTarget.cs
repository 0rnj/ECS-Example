using Unity.Entities;
using Unity.Mathematics;

namespace CodeBase.IdleTowerDefense.Components
{
    public struct MovementTarget : IComponentData
    {
        public Entity TargetEntity;
        public float3 TargetWorldPosition;
    }
}