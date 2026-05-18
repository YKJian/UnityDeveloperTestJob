using Infrastructure.ObjectPool;
using Infrastructure;
using UnityEngine;
using Weapons;
using Towers;

namespace CreationSystem.Impls
{
    public sealed class WeaponCreator : Creator
    {
        private const float m_weaponPositionY = 3.42f;

        [SerializeField] private TowerCreator m_towerCreator;

        private CustomObjectPool<Weapon> m_weaponsPool;
        private Weapon[] m_weaponsPrefabs;

        private void Awake()
        {
            m_weaponsPrefabs = Resources.LoadAll<Weapon>(Paths.Weapons);

            m_weaponsPool = new CustomObjectPool<Weapon>();
        }

        private void OnEnable()
        {
            m_gameplayView.CreateWeaponClicked += OnCreateWeapon;
        }

        private void OnDisable()
        {
            m_gameplayView.CreateWeaponClicked -= OnCreateWeapon;
        }

        private void OnCreateWeapon()
        {
            Create();
        }

        public override IPlaceable Create()
        {
            Tower tower = GetSlot(m_towerCreator.GetActiveTowers()) as Tower;

            if (tower == null)
            {
                return null;
            }

            tower.isOccupied = true;

            Vector3 weaponPosition =
                tower.transform.position + new Vector3(0f, m_weaponPositionY, 0f);
            Quaternion weaponRotation =
                tower.transform.rotation;

            m_weaponsPool.Initialize(
                GetWeaponPrefab(), 
                weaponPosition, 
                weaponRotation,
                tower.transform);

            Weapon weapon = m_weaponsPool.Get();
            weapon.Initialize(tower);

            return weapon;
        }

        private Weapon GetWeaponPrefab() =>
            m_weaponsPrefabs[Random.Range(0, m_weaponsPrefabs.Length)];
    }
}
