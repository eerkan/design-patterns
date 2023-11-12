using System;
namespace Observer
{
    public sealed class Observer<T> : IObserver<T>
    {
        private Action<T> _notifyAction;
        public ISubjectBase<T> Subject { get; set; }

        public Observer(Action<T> notifyAction)
        {
            _notifyAction = notifyAction;
        }

        public void Notify(T state)
        {
            _notifyAction?.Invoke(state);
        }
        
        public void Dispose()
        {
            Subject?.UnregisterObserver(this);
        }
    }
}