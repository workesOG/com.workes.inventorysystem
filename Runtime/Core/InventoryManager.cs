using com.workes.inventory.stacking;
using com.workes.inventory.layout;
using com.workes.inventory.capacity;

namespace com.workes.inventory.core
{
    public class InventoryManager<TKey>
    {
        public IStackResolver<TKey> DefaultStackResolver { get; set; }

        private readonly ItemRegistry<TKey> _registry = new();

        public ItemRegistry<TKey> Registry => _registry;

        public Inventory<TKey> CreateInventory(
            IInventoryLayout<TKey> layout,
            ICapacityPolicy<TKey> capacityPolicy)
        {
            return new Inventory<TKey>(
                this,
                DefaultStackResolver,
                capacityPolicy,
                layout);
        }
    }
}
