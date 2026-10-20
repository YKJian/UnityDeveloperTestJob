using UnityEngine;

namespace CreationSystem
{
    public class PlacementSlot : MonoBehaviour, ISlot
    {
        public bool isOccupied { get; set; } = false;
    }
}
