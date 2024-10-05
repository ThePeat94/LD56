using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.Events;
using UnityEngine;

namespace Nidavellir.Rooms
{
    public class SleepingRoom : MonoBehaviour
    {
        public void PlaceRoom()
        {
            GameEventBus<SleepingRoomPlacedEvent>.Invoke(this, new(4));
        }
    }
}