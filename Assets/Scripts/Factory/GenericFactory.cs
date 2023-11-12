using UnityEngine;
namespace Factory
{
    public abstract class GenericFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected GameObject Prefab;

        private T _GetNewInstance(GameObject prefab)
        {
            return Instantiate(prefab)?.GetComponent<T>();
        }

        public T GetNewInstance()
        {
            return _GetNewInstance(Prefab);
        }

        protected T GetNewInstanceByRandom(GameObject[] prefabs)
        {
            return _GetNewInstance(prefabs[Random.Range(0, prefabs.Length)]);
        }
    }
}