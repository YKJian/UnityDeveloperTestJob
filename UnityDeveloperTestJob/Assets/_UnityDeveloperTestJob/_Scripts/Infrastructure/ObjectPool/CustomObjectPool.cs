using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEngine;

namespace Infrastructure.ObjectPool
{
    public class CustomObjectPool<T> 
        where T : MonoBehaviour
    {
        public List<T> activeObjects => m_activeObjects;

        private ObjectPool<T> m_pool;
        private List<T> m_activeObjects;

        private T m_prefab;
        private Vector3 m_spawnPosition;
        private Quaternion m_spawnRotation;
        private Transform m_parent;

        public CustomObjectPool()
        {
            m_pool = new ObjectPool<T>(
                createFunc: () => OnCreate(), 
                actionOnGet: OnGet, 
                actionOnRelease: OnRelease, 
                actionOnDestroy: OnDestroy);

            m_activeObjects = new List<T>();
        }

        public void Initialize(T prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            m_prefab = prefab;
            m_spawnPosition = position;
            m_spawnRotation = rotation;
            m_parent = parent;
        }

        public void Initialize(T prefab, Vector3 position, Quaternion rotation)
        {
            m_prefab = prefab;
            m_spawnPosition = position;
            m_spawnRotation = rotation;
            m_parent = null;
        }

        public void Initialize(T prefab, Transform parent)
        {
            m_prefab = prefab;
            m_spawnPosition = parent.position;
            m_spawnRotation = parent.rotation;
            m_parent = parent;
        }

        public T Get() => 
            m_pool.Get();

        public void Release(T obj) => 
            m_pool.Release(obj);

        private T OnCreate() =>
            Object.Instantiate(m_prefab, m_spawnPosition, m_spawnRotation, m_parent);

        private void OnGet(T obj)
        {
            obj.transform.SetPositionAndRotation(m_spawnPosition, m_spawnRotation);
            obj.gameObject.SetActive(true);

            m_activeObjects.Add(obj);
        }

        private void OnRelease(T obj)
        {
            obj.gameObject.SetActive(false);

            m_activeObjects.Remove(obj);
        }

        private void OnDestroy(T obj) =>
            Object.Destroy(obj);
    }
}
