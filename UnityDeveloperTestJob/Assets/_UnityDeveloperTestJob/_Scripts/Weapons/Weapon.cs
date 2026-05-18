using Infrastructure.ObjectPool;
using Infrastructure.Observer;
using Projectiles.Data;
using CreationSystem;
using UnityEngine;
using Projectiles;
using Entities;
using Towers;

namespace Weapons
{
    public abstract class Weapon : MonoBehaviour, IWeapon, IPlaceable, IRequiringExternalDataObserver
    {
        [Header("Projectile settings")]
        [SerializeField] private ProjectileData m_projectileData;

        [Header("Weapon settings")]
        [SerializeField] private LayerMask m_targetLayer;
        [SerializeField][Min(0)] private float m_shootInterval = 0.5f;

        protected Projectile m_projectilePrefab;
        protected Vector3 m_projectilePosition;
        protected Quaternion m_projectileRotation;
        protected Entity m_target;

        private CustomObjectPool<Projectile> m_projectilesPool;
        private float m_cooldownTimer;
        private Tower m_tower;

        public void Initialize(Tower tower)
        {
            m_projectilesPool = new CustomObjectPool<Projectile>();
            m_tower = tower;
            m_cooldownTimer = m_shootInterval;

            m_projectilePrefab = m_projectileData.projectile.GetComponent<Projectile>();
            ConfigureProjectile();

            m_projectilesPool.Initialize(
                m_projectilePrefab, 
                m_projectilePosition, 
                m_projectileRotation);
        }

        private void Update()
        {
            if (!CanShoot())
            {
                return;
            }

            m_target = m_tower.GetTarget(m_targetLayer);

            if (m_target == null)
            {
                return;
            }

            Aim(m_target);

            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
                return;
            }

            Shoot();

            m_cooldownTimer = m_shootInterval;
        }

        public void OnNotify<T>(T e)
        {
            Projectile projectile = e as Projectile;

            projectile.RemoveObserver(this);

            m_projectilesPool.Release(projectile);
        }

        protected virtual bool CanShoot() => 
            m_projectileData.projectile != null;


        protected virtual void Shoot()
        {
            Projectile projectile = m_projectilesPool.Get();

            projectile.Initialize(m_projectileData);

            projectile.AddObserver(this);

            LaunchProjectile(projectile);
        }

        protected virtual void Aim(Entity target) { }

        protected abstract void ConfigureProjectile();

        protected abstract void LaunchProjectile(Projectile projectile);
    }
}

