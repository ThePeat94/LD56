namespace Nidavellir.GameEventBus.Events
{
    public class FoodRoomPlacedEvent : IEvent
    {
        public int FoodAmount { get; }
        
        public FoodRoomPlacedEvent(int foodAmount)
        {
            this.FoodAmount = foodAmount;
        }
    }
}