using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class MovementAuthoring : MonoBehaviour
    {
        public float Speed;

        class Baker : Baker<MovementAuthoring>
        {
            public override void Bake(MovementAuthoring authoring)
            {
                AddComponent(new MovementSpeed { Value = authoring.Speed });

                AddComponent<MovementTarget>();
            }
        }
    }
}