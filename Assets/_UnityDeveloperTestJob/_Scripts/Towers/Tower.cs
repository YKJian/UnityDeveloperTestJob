using CreationSystem;
using UnityEngine;
using Entities;

namespace Towers
{
    public class Tower : MonoBehaviour, ISlot, IPlaceable
    {
        private const int MaxTargets = 10;

        [Header("Tower settings")]
        [SerializeField][Min(0)] private float m_range = 10f;

        public bool isOccupied { get; set; } = false;

        private Collider[] m_hits = new Collider[MaxTargets];

        public Entity GetClosestTarget(LayerMask targetLayer)
        {
            Entity target = null;

            float minSqrDistance = Mathf.Infinity;
            int hitCount = Physics.OverlapSphereNonAlloc(transform.position, m_range, m_hits, targetLayer);
            
            for (int i = 0; i < hitCount; i++)
            {
                if (m_hits[i].TryGetComponent(out Entity entity))
                {
                    float sqrDistance = (entity.transform.position - transform.position).sqrMagnitude;
                    if (sqrDistance < minSqrDistance)
                    {
                        minSqrDistance = sqrDistance;
                        target = entity;
                    }
                }
            }

            return target;
        }
    }
}
