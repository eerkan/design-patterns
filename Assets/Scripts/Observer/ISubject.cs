namespace Observer
{
    public interface ISubject<T> : ISubjectBase<T>
    {
        void RegisterObserver(IObserver<T> observer);
        void NotifyObservers(T data);
    }
}