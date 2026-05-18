using Infrastructure.ObjectPool;
using Infrastructure;
using UnityEngine;
using Towers;

namespace CreationSystem.Impls
{
    public sealed class TowerCreator : Creator
    {
        [SerializeField] private PlacementSlot[] m_towerSlots;

        private CustomObjectPool<Tower> m_towersPool; 
        private Tower[] m_towerPrefabs;

        private void Awake()
        {
            m_towerPrefabs = Resources.LoadAll<Tower>(Paths.Towers);

            m_towersPool = new CustomObjectPool<Tower>();
        }

        private void OnEnable()
        {
            m_gameplayView.CreateTowerClicked += OnCreateTower;
        }

        private void OnDisable()
        {
            m_gameplayView.CreateTowerClicked -= OnCreateTower;
        }

        public Tower[] GetActiveTowers() =>
            m_towersPool.activeObjects.ToArray();

        public override IPlaceable Create()
        {
            PlacementSlot slot = GetSlot(m_towerSlots) as PlacementSlot;

            if (slot == null)
            {
                return null;
            }

            slot.isOccupied = true;

            m_towersPool.Initialize(
                GetTowerPrefab(),
                slot.transform);

            Tower tower = m_towersPool.Get();

            return tower;
        }

        private void OnCreateTower()
        {
            Create();
        }

        private Tower GetTowerPrefab() =>
            m_towerPrefabs[Random.Range(0, m_towerPrefabs.Length)];
    }
}
