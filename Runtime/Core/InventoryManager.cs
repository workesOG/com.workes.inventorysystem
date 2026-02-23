using com.workes.inventory.stacking;
using com.workes.inventory.layout;
using com.workes.inventory.capacity;

namespace com.workes.inventory.core
{
    public class InventoryManager<TKey>
    {
        public IStackResolver<TKey> DefaultStackResolver { get; set; }
        public ICapacityPolicy<TKey> DefaultCapacityPolicy { get; set; }
        public IInventoryLayout<TKey> DefaultLayout { get; set; }

        private readonly ItemRegistry<TKey> _registry = new();

        public ItemRegistry<TKey> Registry => _registry;

        /// <summary>
        /// Creates an inventory using the manager's default stack resolver, capacity policy, and layout.
        /// </summary>
        public Inventory<TKey> CreateInventory()
        {
            return new Inventory<TKey>(
                this,
                DefaultStackResolver,
                DefaultCapacityPolicy,
                DefaultLayout);
        }

        /// <summary>
        /// Creates an inventory with the given layout and capacity policy.
        /// </summary>
        public Inventory<TKey> CreateInventory(
            IStackResolver<TKey> stackResolver,
            IInventoryLayout<TKey> layout,
            ICapacityPolicy<TKey> capacityPolicy)
        {
            return new Inventory<TKey>(
                this,
                stackResolver,
                capacityPolicy,
                layout);
        }
    }
}
