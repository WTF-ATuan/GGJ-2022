using System;

namespace Extra
{
    public class DomainEvent
    {
        private Action _domainEvent;

        public void RegisterEvent(Action action)
        {
            _domainEvent += action;
        }

        public void RemoveEvent(Action action)
        {
            _domainEvent -= action;
        }

        public void InvokeEvent()
        {
            _domainEvent?.Invoke();
        }
    }
}