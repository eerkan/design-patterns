using System;
namespace Observer
{
    public interface IReactiveProperty<T> : ISubjectBase<T>
    {
        void RegisterObserver(Predicate<T> condition, IObserver<T> observer);
    }
}