using System.Collections.Generic;
using Nidavellir.GameEventBus.EventBinding;
using Nidavellir.GameEventBus.Events;

namespace Nidavellir.GameEventBus
{
    public static class GameEventBus<T> where T : IEvent
    {
        private static readonly HashSet<IEventBinding<T>> s_eventBindings = new();
    
        public static void Register(IEventBinding<T> eventBinding)
        {
            s_eventBindings.Add(eventBinding);
        }
    
        public static void Unregister(IEventBinding<T> eventBinding)
        {
            s_eventBindings.Remove(eventBinding);
        }
    
        public static void Invoke(object sender, T args)
        {
            foreach (var eventBinding in s_eventBindings)
            {
                eventBinding.Invoke(sender);
                eventBinding.Invoke(sender, args);
            }
        }
    }
}