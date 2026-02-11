using com.workes.inventory.core;

namespace com.workes.inventory.stacking
{
    public interface IStackResolver<TKey>
    {
        int ResolveMaxStack(
            ItemDefinition<TKey> definition,
            Inventory<TKey> inventory);
    }
}
