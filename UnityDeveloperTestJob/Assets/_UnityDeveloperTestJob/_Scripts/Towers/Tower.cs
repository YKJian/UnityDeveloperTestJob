using CreationSystem;
using System.Linq;
using UnityEngine;
using Entities;

namespace Towers
{
    public class Tower : MonoBehaviour, ISlot, IPlaceable
    {
        [Header("Tower settings")]
        [SerializeField][Min(0)] private float m_range = 10f;

        public bool isOccupied { get; set; } = false;

        public Entity GetTarget(LayerMask targetLayer) => 
            ClosestTarget(FindTargets(targetLayer));

        private Entity[] FindTargets(LayerMask targetLayer)
        {
            Entity[] targets = Physics.OverlapSphere(transform.position, m_range, targetLayer)
                .Select(x => x.GetComponent<Entity>())
                .ToArray();

            return targets;
        }

        private Entity ClosestTarget(Entity[] targets)
        {
            return targets
                .OrderBy(x => (x.transform.position - transform.position).sqrMagnitude)
                .FirstOrDefault();
        }
    }
}
