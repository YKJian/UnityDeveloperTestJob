using Projectiles.Data;
using UnityEngine;

namespace Projectiles.Impls
{
    public sealed class GuidedProjectile : Projectile
    {
        private GameObject m_target;
        private Collider m_targetCollider;
        private float m_speed;

        public GameObject target 
        { 
            get => m_target;
            private set
            {
                if (value == null)
                {
                    OnDestroyed(this);
                }
                else
                {
                    m_target = value;
                }
            }
        }

        public override void Initialize(ProjectileData projectileData)
        {
            GuidedProjectileData data = projectileData as GuidedProjectileData;

            base.Initialize(data);

            m_speed = data.speed;
        }

        public void SetTarget(GameObject newTarget)
        {
            target = newTarget;
            m_targetCollider = newTarget.GetComponent<Collider>();
        }

        private void Update()
		{
			if (m_target == null)
            {
                OnDestroyed(this);
                return;
			}

            Move();
        }

        private void Move()
        {
            Vector3 targetCenter = m_targetCollider.bounds.center;
            Vector3 direction = (targetCenter - transform.position).normalized;

            transform.Translate(direction * m_speed * Time.deltaTime);
        }
    }
}