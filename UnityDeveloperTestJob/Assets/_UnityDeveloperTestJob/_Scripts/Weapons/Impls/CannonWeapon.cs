using Entities.Enemies;
using Projectiles;
using UnityEngine;
using Entities;
using Utils;

namespace Weapons.Impls
{
	public sealed class CannonWeapon : Weapon
    {
        private const float m_shootForce = 50f;

        [SerializeField] private GameObject m_cannon;
        [SerializeField] private Transform m_shootPoint;
        [SerializeField][Min(0)] private float m_rotationSpeed = 1f;

        private float m_initialProjectileSpeed;

        protected override bool CanShoot() => 
            base.CanShoot() && m_shootPoint != null;

        protected override void ConfigureProjectile()
        {
            m_initialProjectileSpeed = m_shootForce / m_projectilePrefab.rb.mass;

            m_projectilePosition = m_shootPoint.position;
            m_projectileRotation = m_shootPoint.rotation;
        }

        protected override void Aim(Entity target)
        {
            Vector3 targetPosition = target.transform.position;

            switch (target)
            {
                case GuidedEnemy enemy:
                    float time = MathOperator.GetIntersectionTime(
                        m_initialProjectileSpeed, 
                        transform.position,
                        enemy.velocity, 
                        enemy.transform.position);

                    if (time <= 0f)
                    {
                        break;
                    }
                    Vector3 targetDisplacement = enemy.velocity * time;
                    targetPosition += targetDisplacement;
                    break;
            }

            Vector3 direction = (targetPosition - transform.position).normalized;
            
            float lookRotationY = Quaternion.LookRotation(direction).eulerAngles.y;
            Quaternion newRotation = Quaternion.Euler(0f, lookRotationY, 0f);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                newRotation,
                Time.deltaTime * m_rotationSpeed);
        }

        protected override void LaunchProjectile(Projectile projectile)
        {
            Rigidbody projectileRigidbody = projectile.rb;

            projectileRigidbody.AddForce(
                m_cannon.transform.forward * m_shootForce,
                ForceMode.Impulse);
        }
    }
}