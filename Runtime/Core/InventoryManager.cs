using com.workes.inventory.stacking;

namespace com.workes.inventory.core
{
    public class InventoryManager<TKey>
    {
        public IStackResolver<TKey> DefaultStackResolver { get; set; }

        private readonly ItemRegistry<TKey> _registry = new();

        public ItemRegistry<TKey> Registry => _registry;

        public Inventory<TKey> CreateInventory(int capacity)
        {
            return new Inventory<TKey>(DefaultStackResolver, null, null);
        }
    }
}
