using System;
namespace Observer
{
    public interface ISubjectBase<T> : IDisposable
    {
        void UnregisterObserver(IObserver<T> observer);
    }
}