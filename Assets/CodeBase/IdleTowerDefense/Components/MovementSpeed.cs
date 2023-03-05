using Unity.Entities;

namespace CodeBase.IdleTowerDefense.Components
{
    public struct MovementSpeed : IComponentData
    {
        public float Value; // space units per sec
    }
}