using Infrastructure.Observer;
using UnityEngine;

namespace Entities
{
    public class Health : Subject, IDamageable
    {
        [SerializeField][Min(0)] private int m_maxHP = 30;
        
        private int m_hp;

        public int hp
        {
            get => m_hp;
            set
            {
                if (value <= 0)
                {
                    m_hp = 0;
                    Notify();
                }
                else
                {
                    m_hp = value;
                }
            }
        }

        public void SetHealth()
        {
            hp = m_maxHP;
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                return;
            }

            hp -= damage;
        }
    }
}
