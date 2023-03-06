using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class ProjectileAuthoring : MonoBehaviour
    {
        class Baker : Baker<ProjectileAuthoring>
        {
            public override void Bake(ProjectileAuthoring authoring)
            {
                AddComponent(new Projectile());
            }
        }
    }
}