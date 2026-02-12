using com.workes.inventory.core;

namespace com.workes.inventory.capacity
{
    public interface ICapacityPolicy<TKey>
    {
        bool CanAdd(Inventory<TKey> inventory, ItemInstance<TKey> instance, out string? error);
    }
}
