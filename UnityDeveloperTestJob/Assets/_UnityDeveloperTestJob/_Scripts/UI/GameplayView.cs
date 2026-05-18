using UnityEngine.UI;
using UnityEngine;
using System;

namespace UI
{
    public class GameplayView : MonoBehaviour
    {
        public event Action CreateTowerClicked;
        public event Action CreateWeaponClicked;

        [SerializeField] private Button m_createTowerButton;
        [SerializeField] private Button m_createWeaponButton;

        private void OnEnable()
        {
            m_createTowerButton.onClick.AddListener(OnTowerClicked);
            m_createWeaponButton.onClick.AddListener(OnWeaponClicked);
        }

        private void OnDisable()
        {
            m_createTowerButton.onClick.AddListener(OnTowerClicked);
            m_createWeaponButton.onClick.AddListener(OnWeaponClicked);
        }

        private void OnTowerClicked() =>
            CreateTowerClicked?.Invoke();

        private void OnWeaponClicked() =>
            CreateWeaponClicked?.Invoke();
    }
}
