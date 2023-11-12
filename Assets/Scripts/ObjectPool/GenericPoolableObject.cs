using UnityEngine;
namespace ObjectPool
{
    public abstract class GenericPoolableObject<T> : MonoBehaviour, IPoolableObject<T> where T : MonoBehaviour
    {
        private T _component;
        private GameObject _gameObject;
        private IObjectPool<T> _pool;
        
        public virtual void OnGetPool(IObjectPool<T> pool)
        {
            _pool ??= pool;
            gameObject.SetActive(true);
        }

        public virtual void OnReleasePool()
        {
            gameObject.SetActive(false);
        }

        public void ReleaseToPool()
        {
            _pool.ReleaseObject(this);
        }
        
        public T Component => _component ??= GameObject.GetComponent<T>();
        public GameObject GameObject => _gameObject ??= gameObject;
    }
}