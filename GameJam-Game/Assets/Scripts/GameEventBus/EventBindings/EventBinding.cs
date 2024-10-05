using System;
using Nidavellir.GameEventBus.Events;

namespace Nidavellir.GameEventBus.EventBinding
{
    public class EventBinding<T> : IEventBinding<T> where T : IEvent
    {
        private EventHandler m_noArgsHandler;
        private EventHandler<T> m_handler;

        public event EventHandler NoArgsHandler
        {
            add => this.m_noArgsHandler += value;
            remove => this.m_noArgsHandler -= value;

        }
        public event EventHandler<T> Handler
        {
            add => this.m_handler += value;
            remove => this.m_handler -= value;
        }

        public EventBinding(EventHandler onEvent)
        {
            this.m_noArgsHandler += onEvent;
        }

        public EventBinding(EventHandler<T> onEvent)
        {
            this.m_handler += onEvent;
        }

        public void Invoke(object sender)
        {
            this.m_noArgsHandler?.Invoke(sender, System.EventArgs.Empty);
        }

        public void Invoke(object sender, T args)
        {
            this.m_handler?.Invoke(sender, args);
        }
    }
}