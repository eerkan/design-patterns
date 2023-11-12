using UnityEngine;
namespace ObjectPool
{
    public interface IPoolableObject<T> where T : MonoBehaviour
    {
        void OnGetPool(IObjectPool<T> pool);
        void OnReleasePool();
        void ReleaseToPool();
        T Component { get; }
        GameObject GameObject { get; }
    }
}