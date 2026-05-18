using System.Linq;
using UnityEngine;
using UI;

namespace CreationSystem
{
    public abstract class Creator : MonoBehaviour
    {
        [SerializeField] protected GameplayView m_gameplayView;

        public abstract IPlaceable Create();

        protected ISlot GetSlot(ISlot[] collection)
        {
            ISlot[] unoccupiedSlots = GetUnoccupiedSlots(collection);
            int length = unoccupiedSlots.Length;

            if (length == 0)
            {
                return null;
            }
            else
            {
                return unoccupiedSlots[Random.Range(0, length)];
            }
        }

        private ISlot[] GetUnoccupiedSlots(ISlot[] objects)
        {
            return objects
            .Where(x => !x.isOccupied)
            .ToArray();
        }
    }
}
