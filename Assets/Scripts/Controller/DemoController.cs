using System.Collections;
using Entity.Tree;
using Spawner;
using Strategy;
using UnityEngine;
namespace Controller
{
    public sealed class DemoController : MonoBehaviour
    {
        [SerializeField] private TreeSpawner _treeSpawner;
        [SerializeField] private bool _enableObjectPoolStrategy;
        
        private TreeObjectPool _treeObjectPool;
        private ITreeSpawnStrategy _treeSpawnStrategy;
        
        private void Awake()
        {
            var treeFactory = GetComponent<TreeFactory>();
            _treeObjectPool = new TreeObjectPool(treeFactory);
            
            var treeFactorySpawnStrategy = new TreeFactorySpawnStrategy(treeFactory);
            var treeObjectPoolSpawnStrategy = new TreeObjectPoolSpawnStrategy(_treeObjectPool);
            _treeSpawnStrategy = _enableObjectPoolStrategy ? treeObjectPoolSpawnStrategy : treeFactorySpawnStrategy;
            _treeSpawner.TreeSpawnStrategy = _treeSpawnStrategy;

            StartCoroutine(DamageAllTrees());
        }

        private IEnumerator DamageAllTrees()
        {
            var wait = new WaitForSeconds(0.01f);
            while (true)
            {
                yield return wait;
                foreach (var tree in _treeSpawnStrategy.GetAllTrees())
                    tree.Component.Damage(Random.Range(0.03f, 0.05f));
            }
        }
    }
}