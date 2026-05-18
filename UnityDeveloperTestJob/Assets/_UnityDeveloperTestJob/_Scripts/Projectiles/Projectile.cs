using Infrastructure.Observer;
using Projectiles.Data;
using UnityEngine;
using Entities;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Projectile : Subject, IProjectile
    {
        [SerializeField] private Rigidbody m_rigidbody;

        public Rigidbody rb => m_rigidbody;
        
        private int m_damage;

        private void OnValidate()
        {
            m_rigidbody = m_rigidbody != null ? m_rigidbody : GetComponent<Rigidbody>();
        }

        public virtual void Initialize(ProjectileData projectileData)
        {
            m_rigidbody.linearVelocity = Vector3.zero;

            m_damage = projectileData.damage;
        }

        private void OnTriggerEnter(Collider collision)
        {
            OnDestroyed(this);

            Entity entity = collision.gameObject.GetComponent<Entity>();
            if (entity == null)
            {
                return;
            }

            entity.health.TakeDamage(m_damage);
        }

        protected virtual void OnDestroyed(Projectile projectile)
        {
            Notify(projectile);
        }
    }
}
