using System.Collections.Generic;
using UnityEngine;
using UI;

namespace CreationSystem
{
    public abstract class Creator : MonoBehaviour
    {
        [SerializeField] protected GameplayView m_gameplayView;

        public abstract IPlaceable Create();

        protected ISlot GetUnoccupiedSlot(ISlot[] slots) 
        {
            List<ISlot> unoccupiedSlots = new List<ISlot>();

            foreach (ISlot slot in slots)
            {
                if (!slot.isOccupied)
                {
                    unoccupiedSlots.Add(slot);
                }
            }

            return unoccupiedSlots.Count == 0 
                ? null 
                : unoccupiedSlots[Random.Range(0, unoccupiedSlots.Count)];
        }
    }
}
