using com.workes.inventory.core;

namespace com.workes.inventory.capacity
{
    public class UnlimitedCapacityPolicy<TKey> : ICapacityPolicy<TKey>
    {
        public bool CanApply(Inventory<TKey> inventory, NormalizedInventoryTransaction<TKey> normalizedTransaction, out string? error)
        {
            error = null;
            return true;
        }

        public bool CanAdd(Inventory<TKey> inventory, ItemInstance<TKey> instance, out string? error)
        {
            error = null;
            return true;
        }
    }
}
