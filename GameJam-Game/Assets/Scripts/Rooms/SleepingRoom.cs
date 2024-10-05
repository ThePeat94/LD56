using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.Events;
using UnityEngine;

namespace Nidavellir.Rooms
{
    public class SleepingRoom : Room
    {
        public override void PlaceRoom()
        {
            GameEventBus<SleepingRoomPlacedEvent>.Invoke(this, new(4));
        }
    }
}