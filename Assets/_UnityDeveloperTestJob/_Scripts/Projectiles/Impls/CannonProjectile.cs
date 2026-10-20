using Projectiles.Data;
using UnityEngine;

namespace Projectiles.Impls
{
	public sealed class CannonProjectile : Projectile
	{
		public override void Initialize(ProjectileData projectileData)
		{
			CannonProjectileData data = projectileData as CannonProjectileData;

			base.Initialize(data);

            m_rigidbody.linearVelocity = Vector3.zero;
            m_rigidbody.angularVelocity = Vector3.zero;
        }
    }
}