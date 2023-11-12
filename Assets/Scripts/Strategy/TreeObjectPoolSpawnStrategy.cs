using System.Linq;
using Entity.Tree;
using Tree = Entity.Tree.Tree;
namespace Strategy
{
    public sealed class TreeObjectPoolSpawnStrategy : ITreeSpawnStrategy
    {
        private readonly TreeObjectPool _treeObjectPool;
        
        public TreeObjectPoolSpawnStrategy(TreeObjectPool treeObjectPool)
        {
            _treeObjectPool = treeObjectPool;
        }
        
        public Tree Spawn()
        {
            var tree = _treeObjectPool.GetObject().Component;
            tree.SpawnStrategy = this;
            // tree.Reset();
            return tree;
        }
        
        public void Destroy(Tree tree)
        {
            _treeObjectPool.ReleaseObject(tree);
        }
        
        public Tree[] GetAllTrees()
        {
            return _treeObjectPool.ActiveObjects.Select(x => x.Component).ToArray();
        }
    }
}