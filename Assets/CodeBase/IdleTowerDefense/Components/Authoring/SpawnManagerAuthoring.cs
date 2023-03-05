using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class SpawnManagerAuthoring : MonoBehaviour
    {
        public float SpawnPeriod;

        class Baker : Baker<SpawnManagerAuthoring>
        {
            public override void Bake(SpawnManagerAuthoring authoring)
            {
                AddComponent(new SpawnManager
                {
                    SpawnPeriod = authoring.SpawnPeriod
                });
            }
        }
    } 
}