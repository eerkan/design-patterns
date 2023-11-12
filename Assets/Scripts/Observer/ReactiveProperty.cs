using System;
using System.Collections.Generic;
namespace Observer
{
    public sealed class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private List<IObserver<T>> _observers = new();
        private Dictionary<IObserver<T>, Predicate<T>> _predicates = new();
        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifyObservers(Value);
            }
        }

        public void RegisterObserver(Predicate<T> condition, IObserver<T> observer)
        {
            if (observer is null || _observers.Contains(observer))
                return;
            _predicates[observer] = condition;
            _observers.Add(observer);
            ((Observer<T>)observer).Subject = this;
        }
        
        public void UnregisterObserver(IObserver<T> observer)
        {
            if (observer is null)
                return;
            ((Observer<T>)observer).Subject = null;
            _predicates.Remove(observer);
            _observers.Remove(observer);
        }

        private void NotifyObservers(T data)
        {
            _observers.ForEach(observer =>
            {
                _predicates.TryGetValue(observer, out var predicate);
                if(predicate?.Invoke(data) ?? false)
                    observer.Notify(data);
            });
        }
        
        public void Dispose()
        {
            _observers.ForEach(UnregisterObserver);
        }
    }
}