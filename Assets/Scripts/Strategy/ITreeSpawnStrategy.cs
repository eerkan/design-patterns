using Entity.Tree;
namespace Strategy
{
    public interface ITreeSpawnStrategy
    {
        Tree Spawn();
        void Destroy(Tree tree);
        Tree[] GetAllTrees();
    }
}