using UnityEngine;

namespace Projectiles.Data
{
    public abstract class ProjectileData : ScriptableObject
    {
        [SerializeField] private GameObject m_projectile;
        [SerializeField][Min(0)] private int m_damage = 10;

        public GameObject projectile => m_projectile;
        public int damage => m_damage;
    }
}
