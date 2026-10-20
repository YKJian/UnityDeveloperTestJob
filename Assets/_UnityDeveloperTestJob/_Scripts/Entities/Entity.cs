using Infrastructure.Observer;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(Health))]
    public abstract class Entity : Subject, ISimpleObserver
    {
        [Header("Health settings")]
        [SerializeField] private Health m_health;

        public Health health => m_health;

        private void OnEnable()
        {
            m_health.AddObserver(this);
        }

        private void OnDisable()
        {
            m_health.RemoveObserver(this);
        }

        public void OnNotify() =>
            OnDied(new DeathEventArgs(this));

        protected void ResetHealth() =>
            m_health.SetHealth();

        protected void OnDied(DeathEventArgs e) =>
            Notify(e);
    }
}