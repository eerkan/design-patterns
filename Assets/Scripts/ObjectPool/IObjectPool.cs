using System;
using System.Collections.Generic;
using UnityEngine;
namespace ObjectPool
{
    public interface IObjectPool<T> : IDisposable where T : MonoBehaviour
    {
        bool IsFull { get; }
        int Length { get; }
        IReadOnlyCollection<IPoolableObject<T>> ActiveObjects { get; }
        
        IPoolableObject<T> GetObject();
        void ReleaseObject(IPoolableObject<T> poolableObject);
    }
}