using com.workes.inventory.core;

namespace com.workes.inventory.stacking
{
    public interface IStackResolver<TKey>
    {
        int ResolveMaxStackSize(Inventory<TKey> inventory, ItemInstance<TKey> instance);
    }
}
