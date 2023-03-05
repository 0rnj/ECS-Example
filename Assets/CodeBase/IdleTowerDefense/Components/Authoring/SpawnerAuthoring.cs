using Unity.Entities;
using UnityEngine;

namespace CodeBase.IdleTowerDefense.Components.Authoring
{
    public class SpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;

        private void OnDrawGizmos()
        {
            var color = Gizmos.color;
            var newColor = Color.green;
            newColor.a = 0.25f;

            Gizmos.color = newColor;

            Gizmos.DrawSphere(transform.position, 1f);

            Gizmos.color = color;
        }
    }

    public class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            AddComponent(new Spawner
            {
                Prefab = GetEntity(authoring.Prefab)
            });
        }
    }
}