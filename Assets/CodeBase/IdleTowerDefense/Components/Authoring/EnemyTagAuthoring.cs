using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class EnemyTagAuthoring : MonoBehaviour
    {
        class Baker : Baker<EnemyTagAuthoring>
        {
            public override void Bake(EnemyTagAuthoring authoring)
            {
                AddComponent(new EnemyTag());
            }
        }
    }
}