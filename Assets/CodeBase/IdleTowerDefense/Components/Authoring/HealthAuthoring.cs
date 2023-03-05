using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class HealthAuthoring : MonoBehaviour
    {
        public float Health;

        class Baker : Baker<HealthAuthoring>
        {
            public override void Bake(HealthAuthoring authoring)
            {
                AddComponent(new Health
                {
                    Value = authoring.Health
                });
            }
        }
    }
}