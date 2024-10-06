namespace Nidavellir.GameEventBus.Events
{
    public class BurrowMateGroupAddedEvent : IEvent
    {
        public int GroupSize { get; }

        public BurrowMateGroupAddedEvent(int groupSize)
        {
            this.GroupSize = groupSize;
        }
    }
}