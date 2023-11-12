using System;
namespace Observer
{
    public interface IObserver<T> : IDisposable
    {
        void Notify(T state);
    }
}