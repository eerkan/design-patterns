using ObjectPool;
using UnityEngine;
namespace Factory
{
    public interface IObjectFactory<T> where T : MonoBehaviour, IPoolableObject<T>
    {
        IPoolableObject<T> CreateNew();
    }
}