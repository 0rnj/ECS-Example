using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class TowerTagAuthoring : MonoBehaviour
    {
        class Baker : Baker<TowerTagAuthoring>
        {
            public override void Bake(TowerTagAuthoring authoring)
            {
                AddComponent(new TowerTag());
            }
        }
    }
}