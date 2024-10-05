using Nidavellir.Scriptables;
using UnityEngine;

namespace Nidavellir.Rooms
{
    public abstract class Room : MonoBehaviour
    {
        [SerializeField] protected RoomData m_roomData;
        public abstract void PlaceRoom();
    }
}