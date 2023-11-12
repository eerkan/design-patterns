using System;
using Factory;
using ObjectPool;
using UnityEngine;
namespace Entity.Tree
{
    public sealed class TreeFactory : GenericFactory<Tree>, IObjectFactory<Tree>
    {
        [SerializeField] private GameObject[] _treePrefabs;

        private void Awake()
        {
            if (_treePrefabs is null || _treePrefabs.Length == 0)
                throw new ArgumentException(nameof(_treePrefabs));
        }
        
        public IPoolableObject<Tree> CreateNew()
        {
            return GetNewInstanceByRandom(_treePrefabs);
        }
    }
}