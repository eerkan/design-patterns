using System.Collections.Generic;
using Entity.Tree;
using UnityEngine;
using Tree = Entity.Tree.Tree;
namespace Strategy
{
    public sealed class TreeFactorySpawnStrategy : ITreeSpawnStrategy
    {
        private readonly TreeFactory _treeFactory;
        private readonly List<Tree> _trees = new();
        
        public TreeFactorySpawnStrategy(TreeFactory treeFactory)
        {
            _treeFactory = treeFactory;
        }
        
        public Tree Spawn()
        {
            var tree = _treeFactory.CreateNew().Component;
            tree.SpawnStrategy = this;
            tree.Reset();
            _trees.Add(tree);
            return tree;
        }

        public void Destroy(Tree tree)
        {
            _trees.Remove(tree);
            Object.Destroy(tree.gameObject);
        }
        
        public Tree[] GetAllTrees()
        {
            return _trees.ToArray();
        }
    }
}