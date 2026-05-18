using Projectiles.Impls;
using Projectiles;
using UnityEngine;

namespace Weapons.Impls
{
	public sealed class CrystalWeapon : Weapon
	{
        private Vector3 m_shootOffset = Vector3.up * 1.5f;

        protected override void ConfigureProjectile()
        {
            m_projectilePosition = transform.position + m_shootOffset;
            m_projectileRotation = Quaternion.identity;
        }

        protected override void LaunchProjectile(Projectile projectile)
        {
            GuidedProjectile guidedProjectile = projectile as GuidedProjectile;

            guidedProjectile.SetTarget(m_target.gameObject);
        }
    }
}