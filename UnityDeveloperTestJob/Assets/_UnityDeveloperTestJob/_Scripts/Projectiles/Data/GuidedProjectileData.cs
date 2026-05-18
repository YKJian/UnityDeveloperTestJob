using UnityEngine;

namespace Projectiles.Data
{
    [CreateAssetMenu(fileName = "GuidedProjectileData", menuName = "Projectiles/Guided Projectile")]
    public class GuidedProjectileData : ProjectileData
    {
        [SerializeField][Min(0)] private float m_speed = 0.1f;

        public float speed => m_speed;
    }
}
