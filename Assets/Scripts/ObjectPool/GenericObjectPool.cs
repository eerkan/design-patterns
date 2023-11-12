using System;
using System.Collections.Generic;
using Exception;
using Factory;
using UnityEngine;
namespace ObjectPool
{
    public abstract class GenericObjectPool<T>: IObjectPool<T> where T : MonoBehaviour, IPoolableObject<T>
    {
        private readonly int _maxCapacity;
        private readonly IObjectFactory<T> _factory;
        private readonly List<IPoolableObject<T>> _objects = new();
        private readonly Queue<IPoolableObject<T>> _poolObjects = new();

        public int Length => _poolObjects.Count + _objects.Count;
        public bool IsFull => Length == _maxCapacity;
        public IReadOnlyCollection<IPoolableObject<T>> ActiveObjects => _objects.AsReadOnly();

        protected GenericObjectPool(IObjectFactory<T> factory, int minCapacity = 0, int maxCapacity = int.MaxValue)
        {
            if (minCapacity < 0)
                throw new ArgumentException(nameof(minCapacity));

            if (maxCapacity < minCapacity)
                throw new ArgumentException(nameof(maxCapacity));
            
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _maxCapacity = maxCapacity;
            
            _PoolObjects(minCapacity);
        }

        private void _PoolObjects(int size)
        {
            var pooledObjects = new Queue<IPoolableObject<T>>();
            for (var i = 0; i < size; i++)
                pooledObjects.Enqueue(GetObject());
            while(pooledObjects.TryDequeue(out var pooledObject))
                ReleaseObject(pooledObject);
        }
        
        public void Dispose()
        {
            _objects.Clear();
            _poolObjects.Clear();
            GC.SuppressFinalize(_objects);
            GC.SuppressFinalize(_poolObjects);
            GC.SuppressFinalize(this);
        }

        public IPoolableObject<T> GetObject()
        {
            _poolObjects.TryDequeue(out var poolObject);

            if (poolObject is null)
            {
                if (Length == _maxCapacity)
                    throw new ObjectPoolFullException();
                
                poolObject = _factory.CreateNew();
            }
            
            _objects.Add(poolObject);
            poolObject.OnGetPool(this);
            
            return poolObject;
        }
        
        public void ReleaseObject(IPoolableObject<T> poolableObject)
        {
            poolableObject.OnReleasePool();
            _objects.Remove(poolableObject);
            _poolObjects.Enqueue(poolableObject);
        }
    }
}