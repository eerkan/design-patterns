using Factory;
using ObjectPool;
namespace Entity.Tree
{
    public sealed class TreeObjectPool : GenericObjectPool<Tree>
    {
        public TreeObjectPool(IObjectFactory<Tree> factory, int minCapacity = 0, int maxCapacity = int.MaxValue) : base(factory, minCapacity, maxCapacity)
        {
        }
    }
}