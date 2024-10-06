namespace Nidavellir.GameEventBus.Events
{
    public class FiveIngameMinutesPassedEvent : IEvent
    {
        public int Hours { get; }
        public int Minutes { get; }
        public int CurrentDay { get; }
        
        public FiveIngameMinutesPassedEvent(int hours, int minutes, int currentDay)
        {
            this.Hours = hours;
            this.Minutes = minutes;
            this.CurrentDay = currentDay;
        }
    }
}