using UnityEngine.AI;
using UnityEngine;

namespace Entities.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
	public abstract class GuidedEnemy : Entity
    {
        private const float m_reachDistance = 0.3f;

        [Header("Movement settings")]
        [SerializeField][Min(0)] private float m_speed = 0.1f;
        [SerializeField] private NavMeshAgent m_agent;

        public Vector3 velocity => m_agent.desiredVelocity;

        private GameObject m_moveTarget;

        private void OnValidate()
        {
            m_agent = m_agent != null ? m_agent : GetComponent<NavMeshAgent>();
        }

        public void Initialize(GameObject moveTarget)
        {
            ResetHealth();

            m_agent.speed = m_speed;

            m_moveTarget = moveTarget;
        }

        private void Update()
		{
			if (m_moveTarget == null)
			{
				return;
			}

            m_agent.SetDestination(m_moveTarget.transform.position);

            if (!m_agent.pathPending && m_agent.remainingDistance <= m_reachDistance)
			{
                OnDied(new DeathEventArgs(this));
                return;
            }
		}
    }
}