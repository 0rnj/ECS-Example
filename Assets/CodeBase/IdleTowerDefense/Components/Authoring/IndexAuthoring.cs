using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class IndexAuthoring : MonoBehaviour
    {
        public uint Index;

        class Baker : Baker<IndexAuthoring>
        {
            public override void Bake(IndexAuthoring authoring)
            {
                AddComponent(new Index
                {
                    Value = authoring.Index
                });
            }
        }
    }
}