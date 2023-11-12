using System.Collections.Generic;
namespace Observer
{
    public sealed class Subject<T> : ISubject<T>
    {
        private List<IObserver<T>> _observers = new();

        public void RegisterObserver(IObserver<T> observer)
        {
            if (observer is null || _observers.Contains(observer))
                return;
            _observers.Add(observer);
            ((Observer<T>)observer).Subject = this;
        }
        
        public void UnregisterObserver(IObserver<T> observer)
        {
            if (observer is null)
                return;
            ((Observer<T>)observer).Subject = null;
            _observers.Remove(observer);
        }

        public void NotifyObservers(T data)
        {
            _observers.ForEach(observer => observer.Notify(data));
        }
        
        public void Dispose()
        {
            _observers.ForEach(UnregisterObserver);
        }
    }
}