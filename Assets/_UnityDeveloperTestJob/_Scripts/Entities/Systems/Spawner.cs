using Infrastructure.ObjectPool;
using Infrastructure.Observer;
using Entities.Enemies;
using UnityEngine;

namespace Entities.Systems
{
	public class Spawner : MonoBehaviour, IRequiringExternalDataObserver
    {
        [Header("Entity settings")]
        [SerializeField] private Entity m_entity;
		[SerializeField] private GameObject m_moveTarget;

        [Header("Spawn settings")]
        [SerializeField][Min(0)] private float m_spawnInterval = 3;

        private CustomObjectPool<Entity> m_entitiesPool;
		private float m_cooldownTimer;

        private void Awake()
        {
            m_entitiesPool = new CustomObjectPool<Entity>();

            m_entitiesPool.Initialize(
                m_entity,  
                transform);
        }

        private void Update()
		{
            if (m_cooldownTimer > 0)
            {
                m_cooldownTimer -= Time.deltaTime;
				return;
            }

            Entity entity = m_entitiesPool.Get();

            switch (entity)
            {
                case GuidedEnemy monster: monster.Initialize(m_moveTarget); break;
            }

            entity.AddObserver(this);

            m_cooldownTimer = m_spawnInterval;
        }

        public void OnNotify<T>(T e)
        {
            DeathEventArgs eventArgs = e as DeathEventArgs;

            eventArgs.deadEntity.RemoveObserver(this);

            m_entitiesPool.Release(eventArgs.deadEntity);
        }
    }
}