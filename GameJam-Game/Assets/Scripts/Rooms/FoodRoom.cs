using Nidavellir.GameEventBus;
using Nidavellir.GameEventBus.Events;
using UnityEngine;

namespace Nidavellir.Rooms
{
    public class FoodRoom : Room
    {
        public override void PlaceRoom()
        {
            GameEventBus<FoodRoomPlacedEvent>.Invoke(this, new(50));
        }
    }
}