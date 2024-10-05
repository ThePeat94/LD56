namespace Nidavellir.GameEventBus.Events
{
    public class SleepingRoomPlacedEvent : IEvent
    {
        public int SpaceAmount { get; }
        
        public SleepingRoomPlacedEvent(int spaceAmount)
        {
            this.SpaceAmount = spaceAmount;
        }
    }
}